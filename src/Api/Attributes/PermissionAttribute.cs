
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Steve.ManagerHero.UserService.Application.Service;

namespace Steve.ManagerHero.UserService.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
public class HasPermissionAttribute(
    params string[] permissions
    ) : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly string[] _permission = permissions;

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            throw new UnauthorizedException();
        }

        var permissionService = context.HttpContext.RequestServices.GetService<IPermissionService>();

        if (permissionService == null)
        {
            return;
        }

        var userGuid = Guid.Parse(userId);

        var hasPermission = await permissionService.HasPermissionAsync(userGuid, _permission);

        if (!hasPermission)
        {
            throw new ForbiddenException();
        }
    }
}
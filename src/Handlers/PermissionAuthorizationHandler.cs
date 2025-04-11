/*
* Author: Steve Bang
* History:
* - [2024-04-11] - Created by mrsteve.bang@gmail.com
*/


using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Steve.ManagerHero.UserService.Requirements;

namespace Steve.ManagerHero.UserService.Handlers;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IPermissionService _permissionService;

    public PermissionAuthorizationHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userId = Guid.Parse(context.User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (await _permissionService.UserHasPermissionAsync(userId, requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
    }
}
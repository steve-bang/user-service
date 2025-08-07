
using Steve.ManagerHero.UserService.Application.Interfaces.Caching;

namespace Steve.ManagerHero.UserService.Application.Service;

public interface IPermissionService
{
    Task<bool> HasPermissionAsync(Guid userId, params string[] permissionsRequest);

}

public class PermissionService(
    IUnitOfWork _unitOfWork,
    IPermissionCache _permissionCache
) : IPermissionService
{
    public async Task<bool> HasPermissionAsync(Guid userId, params string[] permissionsRequest)
    {

        bool isCached = _permissionCache.GetPermissionsByUserId(userId, out string[]? permissionCodes);

        if (!isCached || permissionCodes == null)
        {
            var permissions = await _unitOfWork.Permissions.GetPermissionsByUserIdAsync(userId);

            permissionCodes = permissions.Select(p => p.Code).ToArray();

            // Set cache
            _permissionCache.SetPermissionsByUserId(userId, permissionCodes);
        }

        return permissionCodes != null && permissionCodes.Any(
            code => permissionsRequest.Any(p => p == code)
        );

    }
}
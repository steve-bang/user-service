using Steve.ManagerHero.UserService.Domain.AggregatesModel;

public interface IPermissionService
{
    Task<bool> UserHasPermissionAsync(Guid userId, string permissionName);
    Task<IEnumerable<Permission>> GetAllPermissionsAsync();
    Task AssignPermissionToRoleAsync(Guid roleId, Guid permissionId);
    Task RevokePermissionFromRoleAsync(Guid roleId, Guid permissionId);
}
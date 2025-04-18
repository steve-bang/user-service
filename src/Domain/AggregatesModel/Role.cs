/*
* Author: Steve Bang
* History:
* - [2024-04-11] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Common;

namespace Steve.ManagerHero.UserService.Domain.AggregatesModel;

public class Role : AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsDefault { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Navigation properties
    private readonly List<RolePermission> _rolePermissions = new();
    public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

    private readonly List<UserRole> _userRoles = new();
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    public Role() {}

    public Role(string name, string description, bool isDefault = false)
    {
        Name = name;
        Description = description ?? string.Empty;
        IsDefault = isDefault;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description ?? string.Empty;
    }

    public void MarkAsDefault()
    {
        IsDefault = true;
    }

    public void AddPermission(Permission permission)
    {
        if (!_rolePermissions.Any(rp => rp.PermissionId == permission.Id))
        {
            _rolePermissions.Add(new RolePermission(this, permission));
        }
    }

    public void RemovePermission(Permission permission)
    {
        var rolePermission = _rolePermissions.FirstOrDefault(rp => rp.PermissionId == permission.Id);
        if (rolePermission != null)
        {
            _rolePermissions.Remove(rolePermission);
        }
    }

    public bool HasPermission(string permissionName)
    {
        return _rolePermissions.Any(rp =>
            string.Equals(rp.Permission.Name, permissionName, StringComparison.OrdinalIgnoreCase));
    }
}
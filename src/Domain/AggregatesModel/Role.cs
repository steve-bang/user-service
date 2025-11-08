/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.AggregatesModel;

public class Role : AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Navigation properties
    public ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();

    private readonly List<UserRole> _userRoles = new();
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    public Role() { }

    public Role(string name, string description)
    {
        Name = name;
        Description = description ?? string.Empty;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description ?? string.Empty;
    }

    public void AddPermissions(IEnumerable<Permission> permissions)
    {
        foreach (var permission in permissions)
        {
            if (!RolePermissions.Any(rp => rp.PermissionId == permission.Id))
            {
                RolePermissions.Add(new RolePermission(Id, permission.Id));
            }
        }
    }

    public void RemovePermissions(IEnumerable<Permission> permissions)
    {
        foreach (var permission in permissions)
        {
            var rolePermission = RolePermissions.FirstOrDefault(rp => rp.PermissionId == permission.Id);
            if (rolePermission != null)
            {
                RolePermissions.Remove(rolePermission);
            }
        }
    }
}
/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Common;

namespace Steve.ManagerHero.UserService.Domain.AggregatesModel;

public class RolePermission : AggregateRoot
{
    public Guid RoleId { get; private set; }
    public Role Role { get; private set; }
    
    public Guid PermissionId { get; private set; }
    public Permission Permission { get; private set; }
    
    public DateTime AssignedAt { get; private set; }

    // Private constructor for EF Core
    private RolePermission() { }

    public RolePermission(Role role, Permission permission)
    {
        Role = role ?? throw new ArgumentNullException(nameof(role));
        Permission = permission ?? throw new ArgumentNullException(nameof(permission));
        AssignedAt = DateTime.UtcNow;
    }
}
/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.AggregatesModel;

public class UserRole : AggregateRoot
{
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public Guid RoleId { get; private set; }
    public Role Role { get; private set; }

    public DateTime AssignedAt { get; private set; }
    public Guid? AssignedBy { get; private set; }

    // Private constructor for EF Core
    private UserRole() { }

    public UserRole(User user, Role role, Guid? assignedBy = null)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        Role = role ?? throw new ArgumentNullException(nameof(role));
        AssignedBy = assignedBy;
        AssignedAt = DateTime.UtcNow;
    }
}
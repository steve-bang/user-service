/*
* Author: Steve Bang
* History:
* - [2024-04-11] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Common;

namespace Steve.ManagerHero.UserService.Domain.AggregatesModel;

public class Permission : AggregateRoot
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    // Navigation property
    private readonly List<RolePermission> _rolePermissions = new();
    public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

    // Private constructor for EF Core
    private Permission() { }

    public Permission(string code, string name, string description)
    {
        Code = code;
        Name = name;
        Description = description ?? string.Empty;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateDescription(string description)
    {
        Description = description ?? string.Empty;
    }

}
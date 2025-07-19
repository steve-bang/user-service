/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/


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

    public void Update(string code, string name, string descriptionn)
    {
        Code = code;
        Name = name;
        Description = descriptionn ?? string.Empty;
    }

}
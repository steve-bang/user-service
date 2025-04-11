
using Microsoft.AspNetCore.Authorization;

namespace Steve.ManagerHero.UserService.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
public class PermissionAttribute : AuthorizeAttribute
{
    public PermissionAttribute(string permission) 
        : base(policy: permission) { }
}
/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/


using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Steve.ManagerHero.UserService.Application.Service;
using Steve.ManagerHero.UserService.Requirements;

namespace Steve.ManagerHero.UserService.Handlers;

[Obsolete]
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

    }
}
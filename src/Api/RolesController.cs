/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steve.ManagerHero.Api.Models;
using Steve.ManagerHero.Application.Features.Permissions.Commands;
using Steve.ManagerHero.Application.Features.Roles.Commands;
using Steve.ManagerHero.Application.Features.Roles.Queries;

[Route("api/v1/roles")]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] RoleCommandRequest request)
    {
        var result = await _mediator.Send(new CreateRoleCommand(
            Name: request.Name,
            Description: request.Description
        ));

        return ApiResponseSuccess<RoleDto>.BuildCreatedObjectResult(result);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetRoleByIdQuery(id));

        return ApiResponseSuccess<RoleDto>.BuildOKObjectResult(result);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateById(Guid id, [FromBody] RoleCommandRequest request)
    {
        var result = await _mediator.Send(new UpdateRoleCommand(id, request.Name, request.Description));

        return ApiResponseSuccess<RoleDto>.BuildOKObjectResult(result);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        await _mediator.Send(new DeleteRoleCommand(id));
        return new NoContentResult();
    }

    [HttpPost("{roleId}/users/{userId}")]
    [Authorize]
    public async Task<IActionResult> AssignUserToRole(Guid roleId, Guid userId)
    {
        var result = await _mediator.Send(new AssignUserToRoleCommand(roleId, userId));
        return ApiResponseSuccess<bool>.BuildOKObjectResult(result);
    }

    /// <summary>
    /// Assigns permissions to a role.
    /// This endpoint allows you to assign multiple permissions to a specific role.
    /// </summary>
    /// <param name="roleId">The id of the role to which permissions will be assigned.</param>
    /// <param name="request">The request body containing the list of permission IDs to be assigned.</param>
    /// <returns></returns>
    [HttpPost("{roleId}/permissions")]
    [Authorize]
    public async Task<IActionResult> AssignPermissionsToRole(
        Guid roleId,
        [FromBody] AssignPermissionsToRoleRequest request
    )
    {
        var result = await _mediator.Send(new AssignPermissionToRoleCommand(roleId, request.PermissionIds));
        return ApiResponseSuccess<bool>.BuildOKObjectResult(result);
    }

}
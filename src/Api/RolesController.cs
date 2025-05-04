/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steve.ManagerHero.Api.Models;
using Steve.ManagerHero.Application.Features.Permissions.Commands;
using Steve.ManagerHero.Application.Features.Permissions.Queries;
using Steve.ManagerHero.Application.Features.Roles.Commands;
using Steve.ManagerHero.Application.Features.Roles.Queries;
using Steve.ManagerHero.Application.Features.Users.Queries;
using Steve.ManagerHero.UserService.Domain.Constants;

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
    [Authorize(Roles = RoleNames.Admin)]
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

    [HttpGet("{id}/users")]
    [Authorize]
    public async Task<IActionResult> GetUsersByRole(
        Guid id,
        [FromQuery] int pageNumber = PaginationConstant.PageNumberDefault,
        [FromQuery] int pageSize = PaginationConstant.PageSizeDefault
    )
    {
        var users = await _mediator.Send(new GetUsersByRoleIdQuery()
        {
            RoleId = id,
            PageNumber = pageNumber,
            PageSize = pageSize
        });

        return ApiResponseSuccess<PaginatedList<UserDto>>.BuildOKObjectResult(users);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetRoles(
        [FromQuery] string? filter = null,
        [FromQuery] int pageNumber = PaginationConstant.PageNumberDefault,
        [FromQuery] int pageSize = PaginationConstant.PageSizeDefault
    )
    {
        var roles = await _mediator.Send(new GetRolesQuery()
        {
            Filter = filter,
            PageNumber = pageNumber,
            PageSize = pageSize
        });

        return ApiResponseSuccess<PaginatedList<RoleDto>>.BuildOKObjectResult(roles);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> UpdateById(Guid id, [FromBody] RoleCommandRequest request)
    {
        var result = await _mediator.Send(new UpdateRoleCommand(id, request.Name, request.Description));

        return ApiResponseSuccess<RoleDto>.BuildOKObjectResult(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        await _mediator.Send(new DeleteRoleCommand(id));
        return new NoContentResult();
    }

    [HttpPost("{roleId}/users/{userId}")]
    [Authorize(Roles = RoleNames.Admin)]
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
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> AssignPermissionsToRole(
        Guid roleId,
        [FromBody] AssignPermissionsToRoleRequest request
    )
    {
        var result = await _mediator.Send(new AssignPermissionToRoleCommand(roleId, request.PermissionIds));
        return ApiResponseSuccess<bool>.BuildOKObjectResult(result);
    }

    /// <summary>
    /// Get permissions assigned to a specific role.
    /// This endpoint retrieves a paginated list of permissions associated with the specified role.
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="pagination"></param>
    /// <returns></returns>
    [HttpGet("{roleId}/permissions")]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> GetPermissionsByRole(Guid roleId, [FromQuery] PaginationQuery pagination)
    {
        var result = await _mediator.Send(new GetPermissionsByRoleQuery()
        {
            RoleId = roleId,
            PageNumber = pagination.PageNumber,
            PageSize = pagination.PageSize
        });

        return ApiResponseSuccess<PaginatedList<PermissionDto>>.BuildOKObjectResult(result);
    }

    /// <summary>
    /// Assigns permissions to a role.
    /// This endpoint allows you to assign multiple permissions to a specific role.
    /// </summary>
    /// <param name="roleId">The id of the role to which permissions will be assigned.</param>
    /// <param name="request">The request body containing the list of permission IDs to be assigned.</param>
    /// <returns></returns>
    [HttpDelete("{roleId}/permissions")]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> RemovePermissionsFromRole(
        Guid roleId,
        [FromBody] AssignPermissionsToRoleRequest request
    )
    {
        await _mediator.Send(new RemovePermissionsFromRoleCommand(roleId, request.PermissionIds));
        return new NoContentResult();
    }

}
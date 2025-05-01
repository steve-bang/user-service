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
using Steve.ManagerHero.Application.Features.Roles.Queries;

[Route("api/v1/permissions")]
public class PermissionController : ControllerBase
{
    private readonly IMediator _mediator;

    public PermissionController(
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PermissionCommandRequest request)
    {
        var result = await _mediator.Send(new CreatePermissionCommand(
            Code: request.Code,
            Name: request.Name,
            Description: request.Description
        ));

        return ApiResponseSuccess<PermissionDto>.BuildCreatedObjectResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetPermissionByIdQuery(id));

        return ApiResponseSuccess<PermissionDto>.BuildOKObjectResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] PermissionCommandRequest request)
    {
        var result = await _mediator.Send(new UpdatePermissionCommand(
            Id: id,
            Code: request.Code,
            Name: request.Name,
            Description: request.Description
        ));

        return ApiResponseSuccess<PermissionDto>.BuildOKObjectResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById([FromRoute] Guid id)
    {
        await _mediator.Send(new DeletePermissionCommand(id));

        return new NoContentResult();
    }

    [HttpGet]
    public async Task<IActionResult> GetPermissions(
        [FromQuery] string? filter = null,
        [FromQuery] int pageNumber = PaginationConstant.PageNumberDefault,
        [FromQuery] int pageSize = PaginationConstant.PageSizeDefault
    )
    {
        var permissions = await _mediator.Send(new GetPermissionsQuery()
        {
            Filter = filter,
            PageNumber = pageNumber,
            PageSize = pageSize
        });

        return ApiResponseSuccess<PaginatedList<PermissionDto>>.BuildOKObjectResult(permissions);
    }


    [HttpGet("{id}/roles")]
    public async Task<IActionResult> GetRolesByPermissionId([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetRolesByPermissionIdQuery(id));

        return ApiResponseSuccess<IEnumerable<RoleDto>>.BuildOKObjectResult(result);
    }
}
/*
* Author: Steve Bang
* History:
* - [2025-08-03] - Created by mrsteve.bang@gmail.com
*/

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steve.ManagerHero.Api.Models;
using Steve.ManagerHero.Application.Features.Sessions.Commands;
using Steve.ManagerHero.Application.Features.Sessions.Queries;
using Steve.ManagerHero.UserService.Application.DTOs;


namespace Steve.ManagerHero.UserService.Api;

[Route("api/v1/sessions")]
public class SessionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SessionsController(
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById([Required] Guid id)
    {
        var result = await _mediator.Send(new GetSessionByIdQuery(
            Id: id
        ));

        return ApiResponseSuccess<SessionDto>.BuildOKObjectResult(result);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteById([Required] Guid id)
    {
        await _mediator.Send(new DeleteSessionByIdCommand(
            Id: id
        ));

        return new NoContentResult();
    }

    [HttpDelete("{id}/revoke")]
    [Authorize]
    public async Task<IActionResult> RevokeSessionById([Required] Guid id)
    {
        await _mediator.Send(new RevokeSessionByIdCommand(
            Id: id
        ));

        return new NoContentResult();
    }
}
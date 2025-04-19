/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steve.ManagerHero.Api.Models;
using Steve.ManagerHero.Application.Features.Users.Commands;
using Steve.ManagerHero.Application.Features.Users.Queries;
using Steve.ManagerHero.UserService.Application.Auth;
using Steve.ManagerHero.UserService.Application.DTO;

[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly IIdentityService _identityService;

    public AuthController(
        IMediator mediator,
        IIdentityService identityService
    )
    {
        _mediator = mediator;
        _identityService = identityService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterNewUser([FromBody] RegisterUserRequest request)
    {
        var result = await _mediator.Send(new RegisterUserCommand(
            EmailAddress: request.EmailAddress,
            Password: request.Password,
            ConfirmPassword: request.ConfirmPassword,
            FirstName: request.FirstName,
            LastName: request.LastName
        ));

        return ApiResponseSuccess<UserDto>.BuildCreatedObjectResult(result);
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> AuthenticatePassword([FromBody] LoginPasswordRequest request)
    {
        var result = await _mediator.Send(new LoginPasswordQuery(
            EmailAddress: request.EmailAddress,
            Password: request.Password
        ));

        return ApiResponseSuccess<AuthenticationResponseDto>.BuildOKObjectResult(result);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> LogoutUser()
    {
        var result = await _mediator.Send(new LogoutUserCommand(_identityService.GetAccessTokenRequest()));

        return ApiResponseSuccess<bool>.BuildOKObjectResult(result);
    }
}
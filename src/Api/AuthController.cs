

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Steve.ManagerHero.Api.Models;
using Steve.ManagerHero.Application.Features.Users.Commands;
using Steve.ManagerHero.Application.Features.Users.Queries;
using Steve.ManagerHero.UserService.Application.DTO;

// [ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
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
}
/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steve.ManagerHero.Api.Models;
using Steve.ManagerHero.Application.Features.Users.Commands;
using Steve.ManagerHero.Application.Features.Users.Queries;
using Steve.ManagerHero.UserService.Application.Auth;
using Steve.ManagerHero.UserService.Helpers;

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

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        await _mediator.Send(new ForgotPasswordCommand(request.EmailAddress));
        return ApiResponseSuccess<string>.BuildOKObjectResult("If an account with that email exists, a password reset link has been sent.");
    }

    [HttpGet("reset-password/validate")]
    public async Task<IActionResult> ValidateTokenResetPassword([FromQuery] string token)
    {
        var result = await _mediator.Send(new ValidateTokenQuery(token, EncryptionPurpose.ResetPassword));

        return ApiResponseSuccess<TokenValidateDto>.BuildOKObjectResult(result);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var result = await _mediator.Send(new ResetPasswordCommand(
            Token: request.Token,
            NewPassword: request.NewPassword,
            ConfirmPassword: request.ConfirmPassword
        ));

        return ApiResponseSuccess<bool>.BuildOKObjectResult(result);
    }

    [HttpPost("verification-email-address")]
    public async Task<IActionResult> VerificationEmailAddress([FromBody] VerificationEmailAddressRequest request)
    {
        var result = await _mediator.Send(new VerificationEmailAddressCommand(
            Token: request.Token
        ));

        return ApiResponseSuccess<bool>.BuildOKObjectResult(result);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> LogoutUser()
    {
        var result = await _mediator.Send(new LogoutUserCommand(_identityService.GetAccessTokenRequest()));

        return ApiResponseSuccess<bool>.BuildOKObjectResult(result);
    }
}
/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steve.ManagerHero.Api.Models;
using Steve.ManagerHero.Application.Features.Roles.Commands;
using Steve.ManagerHero.Application.Features.Roles.Queries;
using Steve.ManagerHero.Application.Features.Users.Commands;
using Steve.ManagerHero.Application.Features.Users.Queries;
using Steve.ManagerHero.UserService.Application.Auth;

[Route("api/v1/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly IIdentityService _identityService;

    public UsersController(
        IMediator mediator,
        IIdentityService identityService
    )
    {
        _mediator = mediator;
        _identityService = identityService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([Required] string id)
    {
        // If the id request is 'me', we get the current user for get
        if (id == Steve.ManagerHero.UserService.Domain.AggregatesModel.User.CurrentUserKey)
        {
            id = _identityService.GetUserIdRequest().ToString();
        }

        var result = await _mediator.Send(new GetUserByIdQuery(Guid.Parse(id)));

        return ApiResponseSuccess<UserDto>.BuildOKObjectResult(result);
    }

    [HttpPut("{id}/change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangePasswordRequest request)
    {

        var result = await _mediator.Send(new ChangePasswordCommand(
            UserId: id,
            CurrentPassword: request.CurrentPassword,
            NewPassword: request.NewPassword,
            ConfirmPassword: request.ConfirmPassword
        ));

        return ApiResponseSuccess<bool>.BuildOKObjectResult(result);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateById(Guid id, [FromBody] UserUpdateRequest request)
    {

        var result = await _mediator.Send(new UpdateUserCommand(
            Id: id,
            EmailAddress: request.EmailAddress,
            SecondaryEmailAddress: request.SecondaryEmailAddress,
            FirstName: request.FirstName,
            LastName: request.LastName,
            DisplayName: request.DisplayName,
            PhoneNumber: request.PhoneNumber,
            Address: request.Address
        ));

        return ApiResponseSuccess<UserDto>.BuildOKObjectResult(result);
    }

    [HttpPost("{id}/send-verification-email")]
    [Authorize]
    public async Task<IActionResult> SendVerificationEmail(Guid id)
    {
        await _mediator.Send(new SendVerificationEmailLinkCommand(id));
        return ApiResponseSuccess<EmailVerificationSendDto>.BuildOKObjectResult(new());
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        await _mediator.Send(new DeleteUserCommand(id));
        return new NoContentResult();
    }

    [HttpGet]
    //[Authorize]
    public async Task<IActionResult> GetUsers(
        [FromQuery] string? filter = null,
        [FromQuery] int pageNumber = PaginationConstant.PageNumberDefault,
        [FromQuery] int pageSize = PaginationConstant.PageSizeDefault
    )
    {
        var users = await _mediator.Send(new GetUsersQuery()
        {
            Filter = filter,
            PageNumber = pageNumber,
            PageSize = pageSize
        });

        return ApiResponseSuccess<PaginatedList<UserDto>>.BuildOKObjectResult(users);
    }

    [HttpGet("{id}/roles")]
    [Authorize]
    public async Task<IActionResult> GetRolesByUserId(Guid id)
    {
        var roles = await _mediator.Send(new GetRolesByUserIdQuery(id));
        return ApiResponseSuccess<IEnumerable<RoleDto>>.BuildOKObjectResult(roles);
    }

    [HttpPost("{userId}/roles/{roleId}")]
    [Authorize]
    public async Task<IActionResult> AssignRoleToUser(Guid userId, Guid roleId)
    {
        var roles = await _mediator.Send(new AssignUserToRoleCommand(
            UserId: userId,
            RoleId: roleId
        ));

        return ApiResponseSuccess<bool>.BuildOKObjectResult(roles);
    }

}
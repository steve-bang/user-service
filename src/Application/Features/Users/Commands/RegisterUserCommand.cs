/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using MediatR;
using Steve.ManagerHero.UserService.Application.DTO;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public record RegisterUserCommand(
    string EmailAddress,
    string Password,
    string ConfirmPassword,
    string FirstName,
    string LastName
) : IRequest<UserDto>;
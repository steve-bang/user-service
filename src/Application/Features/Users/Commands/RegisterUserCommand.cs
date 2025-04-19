/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public record RegisterUserCommand(
    string EmailAddress,
    string Password,
    string ConfirmPassword,
    string FirstName,
    string LastName
) : IRequest<UserDto>;
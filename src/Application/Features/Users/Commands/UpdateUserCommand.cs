/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.ValueObjects;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public record UpdateUserCommand(
    Guid Id,
    string EmailAddress,
    string FirstName,
    string LastName,
    string DisplayName,
    string? SecondaryEmailAddress,
    string? PhoneNumber, // E.164 format (e.g., +1234567890)
    Address? Address
) : IRequest<UserDto>;
/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.ValueObjects;

namespace Steve.ManagerHero.UserService.Application.DTO;

public record UserUpdateRequest(
    string EmailAddress,
    string FirstName,
    string LastName,
    string DisplayName,
    string? SecondaryEmailAddress,
    string? PhoneNumber,
    Address? Address
);
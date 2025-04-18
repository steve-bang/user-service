/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Application.DTO;

public record UserDto(
    Guid Id,
    string EmailAddress,
    string FirstName,
    string LastName
);
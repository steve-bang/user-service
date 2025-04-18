/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Application.DTO;

public record RoleDto(
    Guid Id,
    string Name,
    string Description,
    bool IsDefault,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    IEnumerable<string> Permissions);
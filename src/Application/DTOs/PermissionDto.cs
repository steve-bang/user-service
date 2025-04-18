/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Application.DTO;

public record PermissionDto(
    Guid Id,
    string Code,
    string Name,
    string Description,
    DateTime CreatedAt);
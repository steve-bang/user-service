/*
* Author: Steve Bang
* History:
* - [2025-05-01] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.UserService.Application.DTO;

public record PermissionCommandRequest(
    string Code,
    string Name,
    string Description
);
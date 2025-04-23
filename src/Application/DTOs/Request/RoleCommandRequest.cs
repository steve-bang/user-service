/*
* Author: Steve Bang
* History:
* - [2025-04-22] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.UserService.Application.DTO;

public record RoleCommandRequest(
    string Name,
    string Description
);
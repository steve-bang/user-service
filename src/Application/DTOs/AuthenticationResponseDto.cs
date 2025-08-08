/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Application.DTO;

public record AuthenticationResponseDto(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresIn
);

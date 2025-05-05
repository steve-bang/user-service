/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.UserService.Application.DTO;

public record ResetPasswordRequest(
    string Token,
    string NewPassword, 
    string ConfirmPassword
);
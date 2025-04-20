/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Application.DTO;

public class EmailVerificationSendDto
{
    public string Message { get; set; }

    public EmailVerificationSendDto()
    {
        Message = "Verification email sent successfully.";
    }
}

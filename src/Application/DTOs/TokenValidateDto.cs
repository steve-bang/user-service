/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Application.DTO;

public class TokenValidateDto
{
    public bool Valid { get; set; }

    public string Reason { get; set; }

    public TokenValidateDto(bool valid, string reason)
    {
        Valid = valid;
        Reason = reason;
    }

    public static TokenValidateDto ValidToken()
    {
        return new(true, "The token is valid.");
    }

    public static TokenValidateDto InValidToken()
    {
        return new(false, "Token expired or invalid.");
    }
}
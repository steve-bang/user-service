/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.SharedKernel.Application.Exception;

public class IpRestrictException : ForbiddenException
{
    public IpRestrictException() : base(
        code: "IpRestrict",
        message: "Access denied. Your IP is not allowed.")
    {

    }
}
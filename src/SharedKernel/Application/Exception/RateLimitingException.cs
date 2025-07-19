/*
* Author: Steve Bang
* History:
* - [2025-05-10] - Created by mrsteve.bang@gmail.com
*/

using System.Net;

namespace Steve.ManagerHero.SharedKernel.Application.Exception;

public class RateLimitingException : ManagerHeroException
{
    public RateLimitingException() : base("user.too_many_requests", "Rate limit exceeded. Please try again later.")
    {
        HttpCode = HttpStatusCode.TooManyRequests;
    }

}
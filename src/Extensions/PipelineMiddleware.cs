/*
* Author: Steve Bang
* History:
* - [2025-05-10] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.Middlewares;

namespace Steve.ManagerHero.UserService.Extensions;

public static class PipelineMiddleware
{
    public static WebApplication ConfigPipelineMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseMiddleware<IpRestrictionMiddleware>();

        return app;
    }
}
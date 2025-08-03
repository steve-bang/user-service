/*
* Author: Steve Bang
* History:
* - [2025-08-03] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.Application.Features.Sessions.Commands;

public record DeleteSessionByIdCommand(Guid Id) : IRequest;
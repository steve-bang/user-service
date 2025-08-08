/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/

using AutoMapper;
using Steve.ManagerHero.UserService.Application.DTOs;
using Steve.ManagerHero.UserService.Application.Interfaces.Caching;

namespace Steve.ManagerHero.Application.Features.Sessions.Queries;

public class GetSessionByIdQueryHandler(
    IUnitOfWork _unitOfWork,
    IMapper _mapper,
    ISessionCache _sessionCache
) : IRequestHandler<GetSessionByIdQuery, SessionDto>
{
    public async Task<SessionDto> Handle(GetSessionByIdQuery request, CancellationToken cancellationToken)
    {
        // Get user from cache
        if (_sessionCache.GetById(request.Id, out Session? cachedSession))
        {
            return _mapper.Map<SessionDto>(cachedSession);
        }

        // If session is not in cache, fetch from database
        Session session = await _unitOfWork.Sessions.GetByIdAsync(request.Id, cancellationToken)
                        ?? throw new SessionNotFoundException();

        // Set session to cache
        _sessionCache.SetSession(session);

        // Return the mapped SessionDto
        return _mapper.Map<SessionDto>(session);
    }
}
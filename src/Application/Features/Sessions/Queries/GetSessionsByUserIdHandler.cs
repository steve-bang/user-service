/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/

using AutoMapper;
using Steve.ManagerHero.UserService.Application.DTOs;

namespace Steve.ManagerHero.Application.Features.Sessions.Queries;

public class GetSessionsByByUserIdQueryHandler(
    IUnitOfWork _unitOfWork,
    IMapper _mapper
) : IRequestHandler<GetSessionsByByUserIdQuery, List<SessionDto>>
{
    public async Task<List<SessionDto>> Handle(GetSessionsByByUserIdQuery request, CancellationToken cancellationToken)
    {
        // List session from database
        List<Session> sessions = await _unitOfWork.Sessions.GetByUserIdAsync(request.UserId, cancellationToken);

        // Return the mapped SessionDto
        return sessions.Select(_mapper.Map<SessionDto>).ToList();
    }
}
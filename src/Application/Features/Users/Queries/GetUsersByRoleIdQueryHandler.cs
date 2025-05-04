/*
* Author: Steve Bang
* History:
* - [2025-05-04] - Created by mrsteve.bang@gmail.com
*/

using AutoMapper;

namespace Steve.ManagerHero.Application.Features.Users.Queries;

public class GetUsersByRoleIdQueryHandler : IRequestHandler<GetUsersByRoleIdQuery, PaginatedList<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUsersByRoleIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginatedList<UserDto>> Handle(
        GetUsersByRoleIdQuery request,
        CancellationToken cancellationToken)
    {

        var (users, totalCount) = await _unitOfWork.Users.GetUsersByRoleIdAsync(
            roleId: request.RoleId,
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        return new PaginatedList<UserDto>
        {
            Items = users.Select(_mapper.Map<UserDto>).ToList(),
            TotalCount = totalCount
        };
    }
}
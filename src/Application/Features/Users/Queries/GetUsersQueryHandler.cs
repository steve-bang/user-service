/*
* Author: Steve Bang
* History:
* - [2025-04-24] - Created by mrsteve.bang@gmail.com
*/

using System.Linq.Expressions;
using AutoMapper;
using Steve.ManagerHero.Application.Processors;

namespace Steve.ManagerHero.Application.Features.Users.Queries;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedList<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IScimFilterProcessor<User> _scimFilterProcessor;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(
        IUnitOfWork unitOfWork,
        IScimFilterProcessor<User> scimFilterProcessor,
        IMapper mapper
    )
    {
        _unitOfWork = unitOfWork;
        _scimFilterProcessor = scimFilterProcessor;
        _mapper = mapper;
    }

    public async Task<PaginatedList<UserDto>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        Expression<Func<User, bool>>? filter = null;

        if (!string.IsNullOrWhiteSpace(request.Filter))
        {
            filter = _scimFilterProcessor.ParseFilter(request.Filter);
        }

        var (users, totalCount) = await _unitOfWork.Users.GetUsersAsync(
            filter,
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
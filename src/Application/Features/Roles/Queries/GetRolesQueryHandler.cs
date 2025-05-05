/*
* Author: Steve Bang
* History:
* - [2025-05-04] - Created by mrsteve.bang@gmail.com
*/

using System.Linq.Expressions;
using AutoMapper;
using Steve.ManagerHero.Application.Processors;

namespace Steve.ManagerHero.Application.Features.Roles.Queries;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PaginatedList<RoleDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IScimFilterProcessor<Role> _scimFilterProcessor;
    private readonly IMapper _mapper;

    public GetRolesQueryHandler(
        IUnitOfWork unitOfWork,
        IScimFilterProcessor<Role> scimFilterProcessor,
        IMapper mapper
    )
    {
        _unitOfWork = unitOfWork;
        _scimFilterProcessor = scimFilterProcessor;
        _mapper = mapper;
    }

    public async Task<PaginatedList<RoleDto>> Handle(
        GetRolesQuery request,
        CancellationToken cancellationToken)
    {
        Expression<Func<Role, bool>>? filter = null;

        if (!string.IsNullOrWhiteSpace(request.Filter))
        {
            filter = _scimFilterProcessor.ParseFilter(request.Filter);
        }

        var (permissions, totalCount) = await _unitOfWork.Roles.GetRolesAsync(
            filter,
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        return new PaginatedList<RoleDto>
        {
            Items = permissions.Select(_mapper.Map<RoleDto>).ToList(),
            TotalCount = totalCount
        };
    }
}
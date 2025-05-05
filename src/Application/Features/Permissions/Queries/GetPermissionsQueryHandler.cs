/*
* Author: Steve Bang
* History:
* - [2025-04-24] - Created by mrsteve.bang@gmail.com
*/

using System.Linq.Expressions;
using AutoMapper;
using Steve.ManagerHero.Application.Processors;

namespace Steve.ManagerHero.Application.Features.Permissions.Queries;

public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, PaginatedList<PermissionDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IScimFilterProcessor<Permission> _scimFilterProcessor;
    private readonly IMapper _mapper;

    public GetPermissionsQueryHandler(
        IUnitOfWork unitOfWork,
        IScimFilterProcessor<Permission> scimFilterProcessor,
        IMapper mapper
    )
    {
        _unitOfWork = unitOfWork;
        _scimFilterProcessor = scimFilterProcessor;
        _mapper = mapper;
    }

    public async Task<PaginatedList<PermissionDto>> Handle(
        GetPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        Expression<Func<Permission, bool>>? filter = null;

        if (!string.IsNullOrWhiteSpace(request.Filter))
        {
            filter = _scimFilterProcessor.ParseFilter(request.Filter);
        }

        var (permissions, totalCount) = await _unitOfWork.Permissions.GetPermissionsAsync(
            filter,
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        return new PaginatedList<PermissionDto>
        {
            Items = permissions.Select(_mapper.Map<PermissionDto>).ToList(),
            TotalCount = totalCount
        };
    }
}
/*
* Author: Steve Bang
* History:
* - [2025-04-24] - Created by mrsteve.bang@gmail.com
*/

using System.Linq.Expressions;
using Steve.ManagerHero.Application.Processors;

namespace Steve.ManagerHero.Application.Features.Users.Queries;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedList<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IScimFilterProcessor<User> _scimFilterProcessor;

    public GetUsersQueryHandler(
        IUnitOfWork unitOfWork,
        IScimFilterProcessor<User> scimFilterProcessor)
    {
        _unitOfWork = unitOfWork;
        _scimFilterProcessor = scimFilterProcessor;
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
            Items = users.Select(user => new UserDto(
            Id: user.Id,
            EmailAddress: user.EmailAddress.Value,
            FirstName: user.FirstName,
            LastName: user.LastName,
            DisplayName: user.DisplayName,
            SecondaryEmailAddress: user.SecondaryEmailAddress != null ? user.SecondaryEmailAddress.Value : null,
            PhoneNumber: user.PhoneNumber != null ? user.PhoneNumber.Value : null,
            LastLogin: user.LastLoginDate,
            Address: user.Address,
            IsActive: user.IsActive,
            IsEmailVerified: user.IsEmailVerified,
            IsPhoneVerified: user.IsPhoneVerified
            )).ToList(),
            TotalCount = totalCount
        };
    }
}
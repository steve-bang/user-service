/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/

using AutoMapper;

namespace Steve.ManagerHero.Application.Features.Users.Queries;

public class GetUserByIdQueryHandler(
    IUnitOfWork _unitOfWork,
    IMapper _mapper
) : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        User user = await _unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken) ?? throw ExceptionProviders.User.NotFoundException;

        return _mapper.Map<UserDto>(user);
    }
}
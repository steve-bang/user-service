/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/

using AutoMapper;
using Steve.ManagerHero.UserService.Application.Interfaces.Caching;

namespace Steve.ManagerHero.Application.Features.Users.Queries;

public class GetUserByIdQueryHandler(
    IUnitOfWork _unitOfWork,
    IMapper _mapper,
    IUserCache _userCache
) : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        // Get user from cache
        if (_userCache.GetByUserId(request.Id, out User? cachedUser))
        {
            return _mapper.Map<UserDto>(cachedUser);
        }

        // If user is not in cache, fetch from database
        User user = await _unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken)
                        ?? throw new UserNotFoundException();

        // Save user to cache
        _userCache.SetUser(user);

        // Return the mapped UserDto
        return _mapper.Map<UserDto>(user);
    }
}
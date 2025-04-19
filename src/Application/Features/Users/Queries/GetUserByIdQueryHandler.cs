/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Users.Queries;

public class GetUserByIdQueryHandler(
    IUserRepository _userRepository
) : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetByIdAsync(request.Id) ?? throw ExceptionProviders.User.NotFoundException;

        return new UserDto(
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
        );
    }
}
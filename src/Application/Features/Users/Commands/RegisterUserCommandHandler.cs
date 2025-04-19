/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class RegisterUserCommandHandler(
    IUserRepository _userRepository
) : IRequestHandler<RegisterUserCommand, UserDto>
{
    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        bool isExistsEmail = await _userRepository.IsExistEmailAsync(request.EmailAddress, cancellationToken);

        if (isExistsEmail) throw ExceptionProviders.User.EmailAlreadyExistsException;


        User user = User.Create(
            firstName: request.FirstName,
            lastName: request.LastName,
            email: request.EmailAddress,
            password: request.Password
        );

        User userCreated = await _userRepository.CreateAsync(user, cancellationToken);

        await _userRepository.UnitOfWork.SaveEntitiesAsync();

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

/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class UpdateUserCommandHandler(
    IUnitOfWork _unitOfWork
) : IRequestHandler<UpdateUserCommand, UserDto>
{
    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken) ?? throw ExceptionProviders.User.NotFoundException;

        // Valid user if email update is new email
        if (user.EmailAddress.Value != request.EmailAddress)
        {
            bool isExsitsEmail = await _unitOfWork.Users.IsExistEmailAsync(request.EmailAddress, cancellationToken);
            if (isExsitsEmail)
                throw ExceptionProviders.User.EmailAlreadyExistsException;
        }

        // Update data
        user.Update(
            emailAddress: request.EmailAddress,
            secondaryEmailAddress: request.SecondaryEmailAddress,
            firstName: request.FirstName,
            lastName: request.LastName,
            displayName: request.DisplayName,
            phoneNumber: request.PhoneNumber,
            address: request.Address
        );

        _unitOfWork.Users.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

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
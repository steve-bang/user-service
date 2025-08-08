/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/


using AutoMapper;
using Steve.ManagerHero.UserService.Application.Interfaces.Caching;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class UpdateUserCommandHandler(
    IUnitOfWork _unitOfWork,
    IMapper _mapper,
    IUserCache _userCache
) : IRequestHandler<UpdateUserCommand, UserDto>
{
    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken) ?? throw new UserNotFoundException();

        // Valid user if email update is new email
        if (user.EmailAddress.Value != request.EmailAddress)
        {
            bool isExsitsEmail = await _unitOfWork.Users.IsExistEmailAsync(request.EmailAddress, cancellationToken);
            if (isExsitsEmail)
                throw new EmailAlreadyExistsException();
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

        // Clear user from cache
        _userCache.ClearUserById(user.Id);

        return _mapper.Map<UserDto>(user);
    }
}
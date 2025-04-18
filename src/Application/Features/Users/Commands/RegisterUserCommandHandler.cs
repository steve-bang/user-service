/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using MediatR;
using Steve.ManagerHero.UserService.Application.DTO;
using Steve.ManagerHero.UserService.Application.Interfaces.Repository;
using Steve.ManagerHero.UserService.Domain.AggregatesModel;
using Steve.ManagerHero.UserService.Domain.Exceptions;

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

        return new UserDto(userCreated.Id, userCreated.EmailAddress.Value, user.FirstName, user.LastName);
    }
}

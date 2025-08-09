/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using AutoMapper;
using Steve.ManagerHero.UserService.Domain.Constants;
using Steve.ManagerHero.UserService.Domain.Services;
using Steve.ManagerHero.UserService.Infrastructure.Security;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class RegisterUserCommandHandler(
    IUnitOfWork _unitOfWork,
    IMapper _mapper,
    IPasswordPolicy _passwordPolicy,
    IPasswordHasher _passwordHasher
) : IRequestHandler<RegisterUserCommand, UserDto>
{
    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        bool isExistsEmail = await _unitOfWork.Users.IsExistEmailAsync(request.EmailAddress, cancellationToken);

        if (isExistsEmail) throw new EmailAlreadyExistsException();

        var validPasswordResult = _passwordPolicy.Validate(request.Password);
        if (!validPasswordResult.IsValid)
            throw new PasswordIncorrectException(messsage: validPasswordResult.Message ?? UserErrorMessages.PasswordIncorrectMessage);

        (string passwordHash, string passwordSalt) = _passwordHasher.Hash(request.Password);

        User user = User.Register(
            firstName: request.FirstName,
            lastName: request.LastName,
            email: request.EmailAddress,
            passwordHash: passwordHash,
            passwordSalt: passwordSalt
        );

        User userCreated = await _unitOfWork.Users.CreateAsync(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UserDto>(userCreated);
    }
}

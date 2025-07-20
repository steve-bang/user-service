/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using AutoMapper;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class RegisterUserCommandHandler(
    IUnitOfWork _unitOfWork,
    IMapper _mapper
) : IRequestHandler<RegisterUserCommand, UserDto>
{
    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        bool isExistsEmail = await _unitOfWork.Users.IsExistEmailAsync(request.EmailAddress, cancellationToken);

        if (isExistsEmail) throw new EmailAlreadyExistsException();

        User user = User.Register(
            firstName: request.FirstName,
            lastName: request.LastName,
            email: request.EmailAddress,
            password: request.Password
        );

        User userCreated = await _unitOfWork.Users.CreateAsync(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UserDto>(userCreated);
    }
}

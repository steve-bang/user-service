/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.BuildingBlocks.Security.Jwt;

namespace Steve.ManagerHero.Application.Features.Users.Queries;

public class LoginPasswordQueryHandler(
    IUnitOfWork _unitOfWork,
    IPasswordHasher _passwordHasher,
    IHttpContextAccessor _httpContextAccessor,
    IJwtHandler _jwtHandler
) : IRequestHandler<LoginPasswordQuery, AuthenticationResponseDto>
{
    public async Task<AuthenticationResponseDto> Handle(LoginPasswordQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(request.EmailAddress, cancellationToken) ?? throw new InvalidCredentialException();

        // Get iidentity by Email
        var identity = user.Identities.FirstOrDefault(identity =>
            identity.Provider == UserService.Domain.Constants.IdentityProvider.Email
            ) ?? throw new InvalidCredentialException();

        if (!_passwordHasher.Verify(request.Password, user.PasswordHash, user.PasswordSalt))
            throw new InvalidCredentialException();

        // Login with password
        user.LoginPassword();

        // Intialize session object.
        var session = new Session(user);

        // Generate token
        (string accessToken, string refreshToken, DateTime expires) = _jwtHandler.GenerateToken(user.Id, session.Id);

        // Add session
        if (_httpContextAccessor.HttpContext != null)
        {
            session.Update(
                refreshToken,
                _httpContextAccessor.HttpContext,
                expires
            );
        }

        await _unitOfWork.Sessions.CreateAsync(session, cancellationToken);

        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthenticationResponseDto(
            AccessToken: accessToken,
            RefreshToken: refreshToken,
            ExpiresIn: expires
        );
    }
}
/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Application.Auth;

namespace Steve.ManagerHero.Application.Features.Users.Queries;

public class LoginPasswordQueryHandler(
    IUnitOfWork _unitOfWork,
    IHttpContextAccessor _httpContextAccessor,
    IJwtHandler _jwtHandler
) : IRequestHandler<LoginPasswordQuery, AuthenticationResponseDto>
{
    public async Task<AuthenticationResponseDto> Handle(LoginPasswordQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(request.EmailAddress, cancellationToken) ?? throw ExceptionProviders.User.LoginPasswordFailedException;

        // Login with password
        user.LoginPassword(request.Password);

        // Generate token
        _jwtHandler.GenerateToken(user, out string accessToken, out string refreshToken, out DateTime expriresIn);

        // Add session
        if (_httpContextAccessor.HttpContext != null)
        {
            var session = Session.Create(
                user,
                accessToken,
                refreshToken,
                _httpContextAccessor.HttpContext,
                expriresIn
            );

            _ = await _unitOfWork.Sessions.CreateAsync(session, cancellationToken);
        }

        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthenticationResponseDto(
            UserId: user.Id,
            AccessToken: accessToken,
            RefreshToken: refreshToken,
            ExpiresIn: expriresIn
        );
    }
}
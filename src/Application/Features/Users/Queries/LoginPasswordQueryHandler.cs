/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/

using MediatR;
using Steve.ManagerHero.UserService.Application.Auth;
using Steve.ManagerHero.UserService.Application.DTO;
using Steve.ManagerHero.UserService.Application.Interfaces.Repository;
using Steve.ManagerHero.UserService.Domain.AggregatesModel;
using Steve.ManagerHero.UserService.Domain.Exceptions;

namespace Steve.ManagerHero.Application.Features.Users.Queries;

public class LoginPasswordQueryHandler(
    IUserRepository _userRepository,
    ISessionRepository _sessionRepository,
    IHttpContextAccessor _httpContextAccessor,
    IJwtHandler _jwtHandler
) : IRequestHandler<LoginPasswordQuery, AuthenticationResponseDto>
{
    public async Task<AuthenticationResponseDto> Handle(LoginPasswordQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.EmailAddress) ?? throw ExceptionProviders.User.LoginPasswordFailed;

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

            _ = await _sessionRepository.CreateAsync(session, cancellationToken);
        }

        _ = await _userRepository.UnitOfWork.SaveEntitiesAsync();

        return new AuthenticationResponseDto(
            UserId: user.Id,
            AccessToken: accessToken,
            RefreshToken: refreshToken,
            ExpiresIn: expriresIn
        );
    }
}
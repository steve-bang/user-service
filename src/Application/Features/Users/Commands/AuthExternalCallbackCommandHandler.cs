/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.EntityFrameworkCore;
using Steve.ManagerHero.BuildingBlocks.Extensions;
using Steve.ManagerHero.UserService.Application.Auth;
using Steve.ManagerHero.UserService.Application.Service;
using Steve.ManagerHero.UserService.Domain.Constants;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class AuthExternalCallbackCommandHandler(
    IUnitOfWork _unitOfWork,
    ExternalAuthServiceFactory _externalAuthServiceFactory,
    IJwtHandler _jwtHandler,
    IHttpContextAccessor _httpContextAccessor,
    ILogger<AuthExternalCallbackCommandHandler> _logger
) : IRequestHandler<AuthExternalCallbackCommand, AuthenticationResponseDto>
{
    public async Task<AuthenticationResponseDto> Handle(AuthExternalCallbackCommand request, CancellationToken cancellationToken)
    {
        IdentityProvider provider = request.Provider;

        var factory = _externalAuthServiceFactory.GetService(provider);

        var oauthUserResult = await factory.VerifyTokenAsync(request.Code);

        var user = await _unitOfWork.Users.GetByEmailAsync(oauthUserResult.Email);

        string accessToken;
        string refreshToken;
        DateTime expriresIn;

        // The use exists in the system
        if (user == null)
        {
            user = User.RegisterFromExternal(
                displayName: oauthUserResult.Name,
                email: oauthUserResult.Email
            );

            await _unitOfWork.Users.CreateAsync(user, cancellationToken);
            _logger.LogInformation("Create a new user from {provider} with the providerId is {id}", provider, oauthUserResult.Id);
        }

        Session session = new Session(user);

        _jwtHandler.GenerateToken(user, session, out accessToken, out refreshToken, out expriresIn);

        // Add identity
        UserIdentity identity = new UserIdentity(
            provider: provider,
            user: user,
            providerId: oauthUserResult.Id,
            identityData: oauthUserResult.ToDictionary()
        );

        var identityExists = user.Identities.FirstOrDefault(i => i.Provider == identity.Provider && i.ProviderId == identity.ProviderId);
        if (identityExists == null)
        {
            user.AddIdentity(identity);

            // Make is new
            _unitOfWork.Context.Entry(identity).State = EntityState.Added;
        }

        if (!user.IsEmailVerified && oauthUserResult.VerifiedAccount)
            user.VerifyEmail();

        user.Login(provider);

        // Add session
        if (_httpContextAccessor.HttpContext != null)
        {
            session.Update(
                refreshToken,
                _httpContextAccessor.HttpContext,
                expriresIn
            );
        }

        _ = await _unitOfWork.Sessions.CreateAsync(session, cancellationToken);


        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthenticationResponseDto(
            AccessToken: accessToken,
            RefreshToken: refreshToken,
            ExpiresIn: expriresIn
        );
    }
}
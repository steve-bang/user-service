/*
* Author: Steve Bang
* History:
* - [2025-05-17] - Created by mrsteve.bang@gmail.com
*/

using MediatR;
using Steve.ManagerHero.UserService.Application.DTOs;

namespace Steve.ManagerHero.UserService.Application.Features.SocialProviders.Commands;

public record CreateSocialProviderCommand(CreateSocialProviderDto Dto) : IRequest<SocialProviderDto>;

public class CreateSocialProviderCommandHandler(
    IUnitOfWork _unitOfWork
) : IRequestHandler<CreateSocialProviderCommand, SocialProviderDto>
{

    public async Task<SocialProviderDto> Handle(CreateSocialProviderCommand request, CancellationToken cancellationToken)
    {
        var provider = SocialProvider.Create(
            request.Dto.Name,
            request.Dto.DisplayName,
            request.Dto.Type,
            request.Dto.ClientId,
            request.Dto.ClientSecret,
            request.Dto.Scopes,
            request.Dto.AuthorizationEndpoint,
            request.Dto.TokenEndpoint,
            request.Dto.UserInfoEndpoint,
            request.Dto.JwksUri,
            request.Dto.AdditionalParameters,
            request.Dto.IsActive,
            request.Dto.AutoLinkAccounts,
            request.Dto.AllowRegistration
        );

        await _unitOfWork.SocialProviders.AddAsync(provider);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new SocialProviderDto
        {
            Id = provider.Id,
            Name = provider.Name,
            DisplayName = provider.DisplayName,
            Type = provider.Type,
            ClientId = provider.Credentials.ClientId,
            ClientSecret = provider.Credentials.ClientSecret,
            Scopes = provider.Scopes,
            AuthorizationEndpoint = provider.Endpoints.AuthorizationEndpoint,
            TokenEndpoint = provider.Endpoints.TokenEndpoint,
            UserInfoEndpoint = provider.Endpoints.UserInfoEndpoint,
            JwksUri = provider.Endpoints.JwksUri,
            AdditionalParameters = provider.Endpoints.AdditionalParameters,
            IsActive = provider.IsActive,
            AutoLinkAccounts = provider.AutoLinkAccounts,
            AllowRegistration = provider.AllowRegistration,
            CreatedAt = provider.CreatedAt,
            UpdatedAt = provider.UpdatedAt
        };
    }
} 
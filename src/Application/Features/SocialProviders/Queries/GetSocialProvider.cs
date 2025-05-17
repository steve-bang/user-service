/*
* Author: Steve Bang
* History:
* - [2025-05-17] - Created by mrsteve.bang@gmail.com
*/

using MediatR;
using Steve.ManagerHero.UserService.Application.DTOs;
using Steve.ManagerHero.UserService.Domain.AggregatesModel;

namespace Steve.ManagerHero.UserService.Application.Features.SocialProviders.Queries;

public record GetSocialProviderQuery(Guid Id) : IRequest<SocialProviderDto>;

public class GetSocialProviderQueryHandler : IRequestHandler<GetSocialProviderQuery, SocialProviderDto>
{
    private readonly ISocialProviderRepository _repository;

    public GetSocialProviderQueryHandler(ISocialProviderRepository repository)
    {
        _repository = repository;
    }

    public async Task<SocialProviderDto> Handle(GetSocialProviderQuery request, CancellationToken cancellationToken)
    {
        var provider = await _repository.GetByIdAsync(request.Id);
        if (provider == null)
            throw new KeyNotFoundException($"Social provider with ID {request.Id} not found");

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
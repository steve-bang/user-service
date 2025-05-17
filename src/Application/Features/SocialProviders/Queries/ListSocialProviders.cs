/*
* Author: Steve Bang
* History:
* - [2025-05-17] - Created by mrsteve.bang@gmail.com
*/

using MediatR;
using Microsoft.EntityFrameworkCore;
using Steve.ManagerHero.UserService.Application.DTOs;
using Steve.ManagerHero.UserService.Domain.AggregatesModel;

namespace Steve.ManagerHero.UserService.Application.Features.SocialProviders.Queries;

public record ListSocialProvidersQuery : IRequest<List<SocialProviderDto>>;

public class ListSocialProvidersQueryHandler : IRequestHandler<ListSocialProvidersQuery, List<SocialProviderDto>>
{
    private readonly ISocialProviderRepository _repository;

    public ListSocialProvidersQueryHandler(ISocialProviderRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<SocialProviderDto>> Handle(ListSocialProvidersQuery request, CancellationToken cancellationToken)
    {
        var providers = await _repository.GetAll()
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        return providers.Select(provider => new SocialProviderDto
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
        }).ToList();
    }
} 
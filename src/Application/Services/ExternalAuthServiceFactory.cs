using Steve.ManagerHero.UserService.Application.Interfaces.Services;
using Steve.ManagerHero.UserService.Domain.Constants;

namespace Steve.ManagerHero.UserService.Application.Service;

public class ExternalAuthServiceFactory
{
    private readonly IServiceProvider _provider;

    public ExternalAuthServiceFactory(IServiceProvider provider)
    {
        _provider = provider;
    }

    public IExternalAuthService GetService(IdentityProvider provider)
    {
        return provider switch
        {
            IdentityProvider.Google => _provider.GetRequiredService<IGoogleAuthService>(),
            _ => throw new NotSupportedException($"Provider {provider} not supported")
        };
    }
}
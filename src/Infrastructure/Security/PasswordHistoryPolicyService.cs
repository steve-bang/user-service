using Steve.ManagerHero.UserService.Domain.Services;

namespace Steve.ManagerHero.UserService.Infrastructure.Security;

public class PasswordHistoryPolicyService : IPasswordHistoryPolicyService
{
    public int MaxHistoryCount { get; }
    private readonly IPasswordHasher _passwordHasher;

    private const int DefaultCount = 1;

    public PasswordHistoryPolicyService(IPasswordHasher passwordHasher, IConfiguration config)
    {
        MaxHistoryCount = config.GetValue<int>("Security:PasswordPolicy:PasswordHistoryCount", DefaultCount);
        _passwordHasher = passwordHasher;
    }

    public bool IsPasswordUsed(User user, string newPasswordInput)
    {
        // check current password too
        if (_passwordHasher.Verify(newPasswordInput, user.PasswordHash, user.PasswordSalt))
            return true;

        foreach (var entry in user.PasswordHistories.Take(MaxHistoryCount))
        {
            if (_passwordHasher.Verify(newPasswordInput, entry.PasswordHash, entry.Salt))
                return true;
        }
        return false;
    }
}

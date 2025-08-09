
namespace Steve.ManagerHero.UserService.Domain.Services;

public interface IPasswordHistoryPolicyService
{
    int MaxHistoryCount { get; }
    bool IsPasswordUsed(User user, string newPasswordInput);
}
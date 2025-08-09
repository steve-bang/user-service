
using Steve.ManagerHero.UserService.Infrastructure.Security;

namespace Steve.ManagerHero.UserService.Domain.Services;

public interface IPasswordPolicy
{
    /// <summary>
    /// Validates a password against all configured rules for the given user.
    /// Throws a PasswordPolicyException if validation fails.
    /// </summary>
    PasswordValidationResult Validate(string password);
}
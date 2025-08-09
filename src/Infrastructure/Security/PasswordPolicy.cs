
using PasswordTheBest.Validations;
using Steve.ManagerHero.UserService.Domain.Services;

namespace Steve.ManagerHero.UserService.Infrastructure.Security;

public record PasswordValidationResult(bool IsValid, string? Message);

public class PasswordPolicy : IPasswordPolicy
{
    private readonly int _minLength;
    private readonly bool _requireUpper;
    private readonly bool _requireLower;
    private readonly bool _requireDigit;
    private readonly bool _requireSymbol;

    private const int DefaultMinLength = 6;

    public PasswordPolicy(IConfiguration config)
    {
        _minLength = config.GetValue<int>("Security:PasswordPolicy:MinLength", DefaultMinLength);
        _requireUpper = config.GetValue<bool>("Security:PasswordPolicy:RequireUppercase", false);
        _requireLower = config.GetValue<bool>("Security:PasswordPolicy:RequireLowercase", false);
        _requireDigit = config.GetValue<bool>("Security:PasswordPolicy:RequireDigit", false);
        _requireSymbol = config.GetValue<bool>("Security:PasswordPolicy:equireSymbol", false);
    }

    public PasswordValidationResult Validate(string password)
    {

        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(password))
        {
            return new PasswordValidationResult(false, "Password cannot be empty.");
        }

        if (password.Length < _minLength)
            errors.Add($"Password must be at least {_minLength} characters long.");

        if (_requireUpper && !password.Any(char.IsUpper))
            errors.Add("Password must contain at least one uppercase letter.");

        if (_requireLower && !password.Any(char.IsLower))
            errors.Add("Password must contain at least one lowercase letter.");

        if (_requireDigit && !password.Any(char.IsDigit))
            errors.Add("Password must contain at least one digit.");

        if (_requireSymbol && password.All(char.IsLetterOrDigit))
            errors.Add("Password must contain at least one special character.");

        return new PasswordValidationResult(
            errors.Count == 0,
            errors.Count != 0 ? string.Join(" ", errors) : null
        );
    }
}
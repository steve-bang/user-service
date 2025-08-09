
namespace Steve.ManagerHero.UserService.Infrastructure.Security;

public interface IPasswordHasher
{
    (string hash, string salt) Hash(string plainPassword);
    bool Verify(string plainPassword, string storedHash, string storedSalt);
}
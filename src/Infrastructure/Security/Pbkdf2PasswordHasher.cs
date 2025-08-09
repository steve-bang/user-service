
using System.Security.Cryptography;
using PasswordTheBest;

namespace Steve.ManagerHero.UserService.Infrastructure.Security;

public class Pbkdf2PasswordHasher : IPasswordHasher
{
    public (string hash, string salt) Hash(string plainPassword)
    {
        var passwordHasher = PasswordTheBestFactory.Create(HashAlgorithmName.SHA256);
        var hash = passwordHasher.Hash(plainPassword, out string salt);

        return (hash, salt);
    }

    public bool Verify(string plainPassword, string storedHash, string storedSalt)
    {
        var passwordHasher = PasswordTheBestFactory.Create(HashAlgorithmName.SHA256);

        return passwordHasher.Verify(plainPassword, storedHash, storedSalt);
    }
}
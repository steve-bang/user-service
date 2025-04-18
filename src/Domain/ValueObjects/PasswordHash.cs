/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using System.Security.Cryptography;
using PasswordTheBest;
using Steve.ManagerHero.UserService.Domain.Common;
namespace Steve.ManagerHero.UserService.Domain.ValueObjects;

public sealed class PasswordHash : ValueObject
{
    public string Hash { get; }
    public string Salt { get; }

    public PasswordHash(string hash, string salt)
    {
        Hash = hash;
        Salt = salt;
    }

    public static PasswordHash Create(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            throw new InvalidOperationException("Password must be at least 8 characters");

        // Hash password
        var passwordHasher = PasswordTheBestFactory.Create(HashAlgorithmName.SHA256);
        var hash = passwordHasher.Hash(password, out string salt);

        return new PasswordHash(hash, salt);
    }

    public bool Verify(string passwordRequest)
    {
        var passwordHasher = PasswordTheBestFactory.Create(HashAlgorithmName.SHA256);

        return passwordHasher.Verify(passwordRequest, Hash, Salt);
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Hash;
        yield return Salt;
    }
}
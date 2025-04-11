/*
* Author: Steve Bang
* History:
* - [2024-04-11] - Created by mrsteve.bang@gmail.com
*/

using System.Security.Cryptography;

using Steve.ManagerHero.UserService.Domain.Common;
namespace Steve.ManagerHero.UserService.Domain.ValueObjects;

public sealed class PasswordHash : ValueObject
{
    public string Hash { get; }
    public string Salt { get; }
    
    private PasswordHash(string hash, string salt)
    {
        Hash = hash;
        Salt = salt;
    }

    public static PasswordHash Create(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            throw new InvalidOperationException("Password must be at least 8 characters");
            
        var salt = GenerateSalt();
        var hash = ComputeHash(password, salt);
        
        return new PasswordHash(hash, salt);
    }

    public bool Verify(string password)
    {
        return ComputeHash(password, Salt) == Hash;
    }

    private static string GenerateSalt()
    {
        byte[] saltBytes = new byte[128 / 8];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(saltBytes);
        return Convert.ToBase64String(saltBytes);
    }

    private static string ComputeHash(string password, string salt)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);
        
        using var pbkdf2 = new Rfc2898DeriveBytes(
            password, 
            saltBytes, 
            10000,
            HashAlgorithmName.SHA256);
            
        byte[] hash = pbkdf2.GetBytes(256 / 8);
        return Convert.ToBase64String(hash);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Hash;
        yield return Salt;
    }
}
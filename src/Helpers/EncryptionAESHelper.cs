/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Steve.ManagerHero.UserService.Helpers;

public static class EncryptionAESHelper
{
    private static readonly int KeySize = 256; // 256-bit key
    private static readonly int BlockSize = 128; // AES block size is always 128-bit
    private static readonly int Iterations = 10000;
    private const char Separator = '|';

    public static string EncryptObject<T>(T obj, string password, string purpose, int expiryMinutes = 1440)
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj));
        if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

        string serializedObj = JsonSerializer.Serialize(obj);
        return Encrypt(serializedObj, password, purpose, expiryMinutes);
    }

    public static T? DecryptObject<T>(string cipherText, string password, string expectedPurpose)
    {
        if (string.IsNullOrEmpty(cipherText)) throw new ArgumentNullException(nameof(cipherText));
        if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));
        if (string.IsNullOrEmpty(expectedPurpose)) throw new ArgumentNullException(nameof(expectedPurpose));

        string serializedObj = Decrypt(cipherText, password, expectedPurpose);
        return JsonSerializer.Deserialize<T>(serializedObj);
    }

    public static string Encrypt(string plainText, string password, string purpose, int expiryMinutes = 1440)
    {
        if (string.IsNullOrEmpty(plainText)) throw new ArgumentNullException(nameof(plainText));
        if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

        // Prepare payload with metadata
        string payload = $"{purpose}{Separator}{plainText}";
        if (expiryMinutes > 0)
        {
            var expiryTime = DateTime.UtcNow.AddMinutes(expiryMinutes).Ticks;
            payload = $"{expiryTime}{Separator}{payload}";
        }

        // Generate cryptographic components
        var salt = GenerateRandomBytes(32); // 256-bit salt
        var iv = GenerateRandomBytes(16);   // 128-bit IV (AES requirement)
        var plainTextBytes = Encoding.UTF8.GetBytes(payload);

        using (var passwordDerivation = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
        using (var aes = Aes.Create())
        {
            aes.KeySize = KeySize;
            aes.BlockSize = BlockSize;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            var key = passwordDerivation.GetBytes(KeySize / 8);
            using (var encryptor = aes.CreateEncryptor(key, iv))
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                }

                // Combine salt (32) + iv (16) + ciphertext
                var result = new byte[salt.Length + iv.Length + memoryStream.ToArray().Length];
                Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
                Buffer.BlockCopy(iv, 0, result, salt.Length, iv.Length);
                Buffer.BlockCopy(memoryStream.ToArray(), 0, result, salt.Length + iv.Length, memoryStream.ToArray().Length);

                return Convert.ToBase64String(result);
            }
        }
    }

    public static string Decrypt(string cipherText, string password, string expectedPurpose)
    {
        if (string.IsNullOrEmpty(cipherText)) throw new ArgumentNullException(nameof(cipherText));
        if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

        try
        {
            var cipherTextBytes = Convert.FromBase64String(cipherText);

            // Extract components (salt 32, iv 16, ciphertext)
            var salt = cipherTextBytes.Take(32).ToArray();
            var iv = cipherTextBytes.Skip(32).Take(16).ToArray();
            var cipherBytes = cipherTextBytes.Skip(32 + 16).ToArray();

            using (var passwordDerivation = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            using (var aes = Aes.Create())
            {
                aes.KeySize = KeySize;
                aes.BlockSize = BlockSize;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                var key = passwordDerivation.GetBytes(KeySize / 8);
                using (var decryptor = aes.CreateDecryptor(key, iv))
                using (var memoryStream = new MemoryStream(cipherBytes))
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                using (var streamReader = new StreamReader(cryptoStream, Encoding.UTF8))
                {
                    var decryptedPayload = streamReader.ReadToEnd();
                    var parts = decryptedPayload.Split(Separator);

                    // Validate payload
                    if (parts.Length == 3 && long.TryParse(parts[0], out var ticks))
                    {
                        if (new DateTime(ticks) < DateTime.UtcNow)
                            throw new SecurityException("Token has expired");
                        if (parts[1] != expectedPurpose)
                            throw new SecurityException("Purpose mismatch");
                        return parts[2];
                    }
                    else if (parts.Length == 2)
                    {
                        if (parts[0] != expectedPurpose)
                            throw new SecurityException("Purpose mismatch");
                        return parts[1];
                    }
                    throw new SecurityException("Invalid token format");
                }
            }
        }
        catch (Exception ex)
        {
            throw new SecurityException("Decryption failed", ex);
        }
    }

    private static byte[] GenerateRandomBytes(int length)
    {
        var bytes = new byte[length];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }
        return bytes;
    }
}

public enum EncryptionPurpose
{
    VerificationEmailAddress,
    ResetPassword
}

public class SecurityException : Exception
{
    public SecurityException(string message) : base(message) { }
    public SecurityException(string message, Exception inner) : base(message, inner) { }
}


public record UserPayloadEncrypt(
    Guid Id
);
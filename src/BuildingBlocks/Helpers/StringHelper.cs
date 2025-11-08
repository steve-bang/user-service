
using System.Security.Cryptography;
using System.Text;

namespace Steve.ManagerHero.BuildingBlocks.Helpers;

public class StringHelper
{
    /// <summary>
    /// Generate numberic otp
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string GenerateNumericOtp(int length)
    {
        var rng = RandomNumberGenerator.Create();
        var bytes = new byte[length];
        rng.GetBytes(bytes);
        var sb = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            sb.Append((bytes[i] % 10).ToString());
        }
        return sb.ToString();
    }

    public static bool SlowEquals(string a, string b)
    {
        var ba = Convert.FromBase64String(a);
        var bb = Convert.FromBase64String(b);
        if (ba.Length != bb.Length) return false;
        int diff = 0;
        for (int i = 0; i < ba.Length; i++) diff |= ba[i] ^ bb[i];
        return diff == 0;
    }
}
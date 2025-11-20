using System.Net;


namespace Steve.ManagerHero.BuildingBlocks.Utilities
{
    public static class IpAddressHelper
    {
        public static bool IsIpInRange(string ipAddress, string cidrNotation)
        {
            try
            {
                if (string.IsNullOrEmpty(ipAddress) || string.IsNullOrEmpty(cidrNotation))
                    return false;

                // Handle single IP address
                if (!cidrNotation.Contains('/'))
                {
                    return ipAddress == cidrNotation;
                }

                // Handle CIDR notation
                var parts = cidrNotation.Split('/');
                if (parts.Length != 2)
                    return false;

                var networkAddress = IPAddress.Parse(parts[0]);
                var subnetMask = int.Parse(parts[1]);

                var ip = IPAddress.Parse(ipAddress);

                // Check if both addresses are of the same address family
                if (ip.AddressFamily != networkAddress.AddressFamily)
                    return false;

                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    // IPv4
                    var ipBytes = ip.GetAddressBytes();
                    var networkBytes = networkAddress.GetAddressBytes();

                    if (subnetMask < 0 || subnetMask > 32)
                        return false;

                    var maskBytes = GetIPv4MaskBytes(subnetMask);

                    for (int i = 0; i < 4; i++)
                    {
                        if ((ipBytes[i] & maskBytes[i]) != (networkBytes[i] & maskBytes[i]))
                            return false;
                    }
                }
                else if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    // IPv6
                    var ipBytes = ip.GetAddressBytes();
                    var networkBytes = networkAddress.GetAddressBytes();

                    if (subnetMask < 0 || subnetMask > 128)
                        return false;

                    var fullBytes = subnetMask / 8;
                    var remainingBits = subnetMask % 8;

                    for (int i = 0; i < fullBytes; i++)
                    {
                        if (ipBytes[i] != networkBytes[i])
                            return false;
                    }

                    if (remainingBits > 0)
                    {
                        var mask = (byte)(0xFF << (8 - remainingBits));
                        if ((ipBytes[fullBytes] & mask) != (networkBytes[fullBytes] & mask))
                            return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static byte[] GetIPv4MaskBytes(int subnetMask)
        {
            var mask = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                if (subnetMask >= 8)
                {
                    mask[i] = 0xFF;
                    subnetMask -= 8;
                }
                else
                {
                    mask[i] = (byte)(0xFF << (8 - subnetMask));
                    subnetMask = 0;
                }
            }
            return mask;
        }

        public static bool IsValidIP(string ipAddress)
        {
            return IPAddress.TryParse(ipAddress, out _);
        }

        public static bool IsValidCIDR(string cidrNotation)
        {
            try
            {
                if (string.IsNullOrEmpty(cidrNotation))
                    return false;

                if (!cidrNotation.Contains('/'))
                    return IsValidIP(cidrNotation);

                var parts = cidrNotation.Split('/');
                if (parts.Length != 2)
                    return false;

                if (!IsValidIP(parts[0]))
                    return false;

                if (!int.TryParse(parts[1], out int subnetMask))
                    return false;

                var ip = IPAddress.Parse(parts[0]);
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return subnetMask >= 0 && subnetMask <= 32;
                else if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    return subnetMask >= 0 && subnetMask <= 128;

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
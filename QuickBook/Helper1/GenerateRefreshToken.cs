using System.Security.Cryptography;

namespace QuickBook.Helper1
{
    public class GenerateRefreshToken
    {
        public static string GenerateToken()
        {

            var randomByte = RandomNumberGenerator.GetBytes(64);

            return Convert.ToBase64String(randomByte).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }
    }
}

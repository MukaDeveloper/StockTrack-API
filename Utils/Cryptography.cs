using System.Security.Cryptography;
using System.Text;

namespace StockTrack_API.Utils
{
    public class Cryptography
    {
        public static void CryptographyHashSHA256(string input, out byte[] hash)
        {
            hash = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        }

        public static void CryptographyHashHmac(string input, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
        }

        public static bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
using System.Security.Cryptography;
using System.Text;

namespace StockTrack_API.Utils
{
    public class Cryptography
    {
        public static string HashToken(string token)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));

                // Converte o hash para uma string hexadecimal
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // "x2" para hexadecimal em minúsculas
                }
                return sb.ToString();
            }
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
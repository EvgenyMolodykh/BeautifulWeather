using System;
using System.Security.Cryptography;
using System.Text;

namespace WeatherAppWPF.Repository
{
    public class PBKDF2PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 20;
        private const int Iterations = 10000; 
        private static readonly HashAlgorithmName s_hashAlgorithm = HashAlgorithmName.SHA256;

        public string HashPassword(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, Iterations, s_hashAlgorithm))
            {
                byte[] salt = deriveBytes.Salt;

                byte[] hash = deriveBytes.GetBytes(HashSize);

                string saltBase64 = Convert.ToBase64String(salt);
                string hashBase64 = Convert.ToBase64String(hash);
                return $"{saltBase64}.{hashBase64}";
            }
        }

        public bool VerifyPassword(string password, string storedHash)
        {
            string[] parts = storedHash.Split('.');
            if (parts.Length != 2) return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] expectedHash = Convert.FromBase64String(parts[1]);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations, s_hashAlgorithm))
            {
                byte[] actualHash = deriveBytes.GetBytes(HashSize);
                return CryptographicOperations.FixedTimeEquals(expectedHash, actualHash);
            }
        }
    }
}
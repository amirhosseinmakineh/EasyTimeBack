using System.Security.Cryptography;

namespace EasyTime.Utilities.Convertor
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 10000;

        public static string HashPassword(string password, out string salt)
        {
            var saltBytes = RandomNumberGenerator.GetBytes(SaltSize);
            using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
            var key = pbkdf2.GetBytes(KeySize);
            salt = Convert.ToBase64String(saltBytes);
            return Convert.ToBase64String(key);
        }

        public static bool VerifyPassword(string password, string salt, string hash)
        {
            var saltBytes = Convert.FromBase64String(salt);
            using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
            var key = pbkdf2.GetBytes(KeySize);
            var computed = Convert.ToBase64String(key);
            return computed == hash;
        }
    }
}

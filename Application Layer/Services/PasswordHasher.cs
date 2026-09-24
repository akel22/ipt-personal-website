using System;
using System.Security.Cryptography;

namespace VelascoPersonalWebsite_IPT.Application.Services
{
    public class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;
        private const string Prefix = "PBKDF2";

        public bool Verify(string password, string storedHash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(storedHash)) return false;
            var parts = storedHash.Split('$');
            if (parts.Length != 4 || !string.Equals(parts[0], Prefix, StringComparison.Ordinal)) return false;
            int iterations; byte[] salt; byte[] expectedHash;
            if (!int.TryParse(parts[1], out iterations) || iterations <= 0 || !TryFromBase64(parts[2], out salt) || !TryFromBase64(parts[3], out expectedHash) || salt.Length != SaltSize || expectedHash.Length != HashSize) return false;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations)) return FixedTimeEquals(expectedHash, deriveBytes.GetBytes(HashSize));
        }

        public string Hash(string password)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("Password is required.", nameof(password));
            using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, Iterations))
                return string.Format("{0}${1}${2}${3}", Prefix, Iterations, Convert.ToBase64String(deriveBytes.Salt), Convert.ToBase64String(deriveBytes.GetBytes(HashSize)));
        }

        private static bool TryFromBase64(string value, out byte[] bytes)
        {
            try { bytes = Convert.FromBase64String(value); return true; }
            catch (FormatException) { bytes = null; return false; }
        }

        private static bool FixedTimeEquals(byte[] left, byte[] right)
        {
            if (left == null || right == null || left.Length != right.Length) return false;
            var result = 0;
            for (var index = 0; index < left.Length; index++) result |= left[index] ^ right[index];
            return result == 0;
        }
    }
}

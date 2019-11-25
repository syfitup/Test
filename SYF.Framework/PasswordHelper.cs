using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SYF.Framework
{
    public static class PasswordHelper
    {
        public static string EncryptPassword(string password, string salt, bool passwordBug = false)
        {
            // This is to cater for the password bug where the password was converted to lowercase
            if (passwordBug) password = password.ToLower();

            var passwordData = Encoding.Unicode.GetBytes(password);
            var saltData = Convert.FromBase64String(salt);
            var data = new byte[passwordData.Length + saltData.Length];
            Buffer.BlockCopy(passwordData, 0, data, 0, passwordData.Length);
            Buffer.BlockCopy(saltData, 0, data, passwordData.Length, saltData.Length);

            var hashProvider = SHA256.Create();
            var hashData = hashProvider.ComputeHash(data);

            return Convert.ToBase64String(hashData);
        }

        public static string GenerateSalt()
        {
            return Convert.ToBase64String(GenerateRandomValue(16));
        }

        public static string GenerateToken()
        {
            return Convert.ToBase64String(GenerateRandomValue(16));
        }

        public static byte[] GenerateApiKey()
        {
            return GenerateRandomValue(32);
        }

        public static string BuildApiToken(Guid userId, byte[] apiKey)
        {
            using (var stream = new MemoryStream())
            {
                var userIdValue = userId.ToByteArray();
                stream.Write(userIdValue, 0, userIdValue.Length);
                stream.Write(apiKey, 0, apiKey.Length);

                return Convert.ToBase64String(stream.ToArray());
            }
        }

        private static byte[] GenerateRandomValue(int size)
        {
            using (var generator = new RNGCryptoServiceProvider())
            {
                var data = new byte[size];
                generator.GetBytes(data);

                return data;
            }
        }
    }
}

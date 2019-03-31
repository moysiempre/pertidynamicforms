using System;
using System.Security.Cryptography;
using System.Text;

namespace FormsAdminGP.Common.Utilities
{
    public class Utils
    {
        public static string NewGuid => Guid.NewGuid().ToString().ToLower();
        private static readonly RandomNumberGenerator _rand = RandomNumberGenerator.Create();

        public static string GetSha256Hash(string input)
        {
            using (var hashAlgorithm = new SHA256CryptoServiceProvider())
            {
                var byteValue = Encoding.UTF8.GetBytes(input);
                var byteHash = hashAlgorithm.ComputeHash(byteValue);
                return Convert.ToBase64String(byteHash);
            }
        }

        public static Guid CreateCryptographicallySecureGuid()
        {
            var bytes = new byte[16];
            _rand.GetBytes(bytes);
            return new Guid(bytes);
        }
    }
}

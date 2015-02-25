using System;
using System.Security.Cryptography;
using System.Text;

namespace Clothing.Helpers.AccountHelpers
{
    public class Password
    {
        public string Hash { get; private set; }
        public string Salt { get; private set; }
        public string NewPassword { get; private set; }

        static readonly Random Random = new Random((int)DateTime.Now.Ticks);
        static string RandomPassword(int size)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();

        }

        public Password()
        {
            var saltBytes = new byte[32];
            using (var provider = new RNGCryptoServiceProvider())
                provider.GetNonZeroBytes(saltBytes);
            Salt = Convert.ToBase64String(saltBytes);
            NewPassword = RandomPassword(5);
            Hash = ComputeHash(Salt, NewPassword);
        }

        public Password(string password)
        {
            var saltBytes = new byte[32];
            using (var provider = new RNGCryptoServiceProvider())
                provider.GetNonZeroBytes(saltBytes);
            Salt = Convert.ToBase64String(saltBytes);
            Hash = ComputeHash(Salt, password);
        }

        static string ComputeHash(string salt, string password)
        {
            try
            {
                var saltBytes = Convert.FromBase64String(salt);
                using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 1000))
                    return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
            }
            catch
            {
                return "";
            }
        }

        public static bool Verify(string salt, string hash, string password)
        {
            return hash == ComputeHash(salt, password);
        }
    }
}

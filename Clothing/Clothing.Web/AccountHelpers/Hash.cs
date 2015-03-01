using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace Clothing.Web.AccountHelpers
{
    public static class Crypter
    {
        public static Guid CreateCryptoGuid()
        {
            var rng = new RNGCryptoServiceProvider();

            var data = new byte[16];

            rng.GetBytes(data);

            data[7] = (byte)((data[7] | 0x40) & 0x4f);
            data[8] = (byte)((data[8] | 0x80) & 0xbf);

            return new Guid(data);
        }

        public static string GenerateHash(int length)
        {
            var pwd = Membership.GeneratePassword(12, 1);

            return Regex.Replace(pwd, @"[^a-zA-Z0-9]", m => "3");
        }
    }
}

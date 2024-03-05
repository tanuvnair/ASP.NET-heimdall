using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ASP.NET_heimdall
{
    public class VerificationTokenGenerator
    {
        public static string GenerateToken(string email)
        {
            byte[] salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(salt);

            string combinedString = email + Convert.ToBase64String(salt) + DateTime.Now.Ticks;

            byte[] hashedBytes;
            using (SHA256 sha256 = SHA256.Create())
            {
                hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedString));
            }

            string token = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

            return token;
        }

    }
}
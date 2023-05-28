
using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace NextItemBuy.Services.Utils
{
    public static class EncriptionUtil
    {
        public static string secretKey = ConfigurationManager.AppSettings["secretKey"];

        public static string EncryptPassword(string password)
        {
            string passwordWithSecretKey = password + secretKey;

            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(passwordWithSecretKey);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
        public static bool VerifyMd5Hash(string input, string hash)
        {
            string hashOfInput = EncryptPassword(input);
            var comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }
    }
}

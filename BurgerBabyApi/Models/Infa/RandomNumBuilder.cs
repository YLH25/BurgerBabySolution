using System.Text;
using System;
using System.Security.Cryptography;
namespace BurgerBabyApi.Models.Infa
{
    public static class RandomNumBuilder
    {
        public static string CreateSha256Code() 
        {
            var guid = Guid.NewGuid().ToString();
            var code = "";
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] tokenBytes = Encoding.UTF8.GetBytes(guid);
                byte[] hashBytes = sha256.ComputeHash(tokenBytes);
                code = Convert.ToBase64String(hashBytes);
            }
            return code;  
        }
    }
}

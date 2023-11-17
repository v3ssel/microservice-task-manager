using System.Security.Cryptography;

namespace TaskManager.Autorization.Extensions
{
    public static class EncodeSHA512Extension
    {
        public static string EncodeSHA512(this string text)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(text);
            var hashed = SHA512.HashData(bytes);
            
            return BitConverter.ToString(hashed).Replace("-", "").ToLower();
        }
    }
}
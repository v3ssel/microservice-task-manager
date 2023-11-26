using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace TaskManager.Tasks.Models
{
    public class JWTVerifier
    {
        public static async Task<TokenValidationParameters> GetValidationParameters(string key_path)
        {
            var rsa = RSA.Create();
            rsa.FromXmlString(await File.ReadAllTextAsync(key_path)); 

            return new TokenValidationParameters()
            {
                RequireSignedTokens = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new RsaSecurityKey(rsa)
            };
        }
    }
}
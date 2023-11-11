using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Autorization.Models;

namespace TaskManager.Autorization.Services
{
    public class TokenService
    {
        private readonly AuthOptions _authOptions;

        public TokenService(AuthOptions authOptions)
        {
            Validator.ValidateObject(authOptions, new ValidationContext(authOptions), true);
            _authOptions = authOptions;
        }

        public async Task<SecurityKey> SignIssuerKey()
        {
            RSA rsa = RSA.Create();
            rsa.FromXmlString(await File.ReadAllTextAsync(_authOptions.PUBLIC_KEY_PATH!));

            return new RsaSecurityKey(rsa);
        }

        public SigningCredentials SignAudienceKey()
        {
            RSA rsa = RSA.Create();
            rsa.FromXmlString(File.ReadAllText(_authOptions.PRIVATE_KEY_PATH!));

            return new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);
        }
    }
}
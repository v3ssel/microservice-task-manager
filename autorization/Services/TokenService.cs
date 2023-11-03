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

        public TokenService(IOptions<AuthOptions> authOptions)
        {
            Validator.ValidateObject(authOptions.Value, new ValidationContext(authOptions.Value), true);
            _authOptions = authOptions.Value;
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
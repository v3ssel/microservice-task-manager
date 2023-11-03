using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Autorization.Models;
using TaskManager.Autorization.Services;

namespace TaskManager.Autorization.Extensions
{
    public static class AsymmetricAutheticationExtension
    {
        public static IServiceCollection AddAsymmetricAuthetication(this IServiceCollection services, IOptions<AuthOptions> auth, TokenService tokenService)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(
                                async (opt) => {
                                    opt.TokenValidationParameters = new TokenValidationParameters
                                    {
                                        ValidateIssuer = true,
                                        ValidIssuers = auth.Value.ISSUERS,
                                        
                                        ValidateAudience = true,
                                        ValidAudiences = auth.Value.AUDIENCE,
                                        
                                        ValidateLifetime = true,

                                        ValidateIssuerSigningKey = true,
                                        IssuerSigningKey = await tokenService.SignIssuerKey()
                                    };
                                }
                            );
            services.AddAuthorization();

            return services;
        }
    }
}
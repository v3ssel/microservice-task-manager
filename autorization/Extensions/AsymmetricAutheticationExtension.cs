using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Autorization.Models;
using TaskManager.Autorization.Services;

namespace TaskManager.Autorization.Extensions
{
    public static class AsymmetricAutheticationExtension
    {
        public static IServiceCollection AddAsymmetricAuthetication(this IServiceCollection services, AuthOptions auth)
        {
            var tokenService = new TokenService(auth);
            
            services.AddAuthorization(opt => {
                opt.AddPolicy("Administrator", 
                              new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                              .RequireRole("admin")
                                                              .Build());
                opt.AddPolicy("User", 
                              new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                              .Build());
                opt.DefaultPolicy = opt.GetPolicy("User")!;    
            });
 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(
                                async (opt) => {
                                    opt.TokenValidationParameters = new TokenValidationParameters
                                    {
                                        ValidateIssuer = true,
                                        ValidIssuers = auth.ISSUERS,
                                        
                                        ValidateAudience = true,
                                        ValidAudiences = auth.AUDIENCE,
                                        
                                        ValidateLifetime = true,

                                        ValidateIssuerSigningKey = true,
                                        IssuerSigningKey = await tokenService.SignIssuerKey()
                                    };
                                }
                            );

            return services;
        }
    }
}
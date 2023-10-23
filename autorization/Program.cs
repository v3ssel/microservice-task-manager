using System.Data.SqlTypes;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Autorization.Models;
using System.ComponentModel.DataAnnotations;
using MongoDB.Driver;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();

        var auth = builder.Configuration.GetSection("AuthOptions").Get<AuthOptions>()
                   ?? throw new ArgumentException("The AuthOptions is not correct.");
        Validator.ValidateObject(auth, new ValidationContext(auth), true);
        builder.Services.AddSingleton(auth);

        var conn_str = builder.Configuration.GetConnectionString("MongoDB") ?? throw new ArgumentException("The MongoDB connection string is not set.");
        builder.Services.AddSingleton(new MongoClient(conn_str));
        
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(
                            opt => {
                                opt.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidIssuer = auth.ISSUER,
                                    
                                    ValidateAudience = true,
                                    ValidAudience = auth.AUDIENCE,
                                    
                                    ValidateLifetime = true,

                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(auth.SECRET_KEY!))
                                };
                            }
                        );
        builder.Services.AddAuthorization();

        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession();

        var app = builder.Build();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();

        // app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}

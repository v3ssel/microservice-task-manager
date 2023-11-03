using System.Data.SqlTypes;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Autorization.Models;
using System.ComponentModel.DataAnnotations;
using MongoDB.Driver;
using System.Security.Cryptography;
using TaskManager.Autorization.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("AuthOptions"));
        builder.Services.AddTransient<TokenService>();
        builder.Services.AddControllers();

        var conn_str = builder.Configuration.GetConnectionString("MongoDB") ?? throw new ArgumentException("The MongoDB connection string is not set.");
        builder.Services.AddSingleton(new MongoClient(conn_str));
        
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

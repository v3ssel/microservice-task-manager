using TaskManager.Autorization.Models.DTO;
using MongoDB.Driver;
using TaskManager.Autorization.Services;
using TaskManager.Autorization.Extensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var authOpt = builder.Configuration.GetSection("AuthOptions").Get<AuthOptions>()
                        ?? throw new ArgumentException("AuthOptions is required.");

        builder.Services.AddSingleton(authOpt);
        builder.Services.AddTransient<TokenService>();
        builder.Services.AddControllers();

        var conn_str = builder.Configuration.GetConnectionString("MongoDB") ?? throw new ArgumentException("The MongoDB connection string is not set.");
        builder.Services.AddSingleton(new MongoClient(conn_str));

        builder.Services.AddAsymmetricAuthetication(authOpt);

        var app = builder.Build();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}

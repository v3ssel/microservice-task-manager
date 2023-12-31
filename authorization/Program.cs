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
        builder.Services.AddAsymmetricAuthetication(authOpt);

        var conn_str = builder.Configuration.GetConnectionString("MongoDB") ?? throw new ArgumentException("The MongoDB connection string is not set.");
        builder.Services.AddSingleton(new MongoClient(conn_str));
        builder.Services.AddTransient<TokenService>();
        
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

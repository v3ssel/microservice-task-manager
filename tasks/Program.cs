using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TaskManager.Tasks.Models;
using MongoDB.Driver;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var cstr = builder.Configuration.GetConnectionString("MongoDB");
        builder.Services.AddSingleton<IMongoClient>(new MongoClient(cstr));

        builder.Services.AddAuthentication(x => {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(async (x) => {
            x.SaveToken = true;
            x.TokenValidationParameters = await JWTVerifier.GetValidationParameters(builder.Configuration.GetValue<string>("PUBLIC_KEY_XML_PATH"));
        });


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

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
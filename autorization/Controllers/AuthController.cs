using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Autorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using TaskManager.Autorization.Services;

namespace TaskManager.Autorization.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly MongoClient _mongoClient;

        public AuthController(ILogger<AuthController> logger, MongoClient mongoClient)
        {
            _logger = logger;
            _mongoClient = mongoClient;
        }

        [HttpGet]
        [Route("welcome")]
        public string Home()
        {
            return $"Welcome !"; 
        }

        [HttpPost]
        [Route("register")]
        public async Task<JsonResult> Register(RegisterRequest data, TokenService tokenService)
        {
            var collection = _mongoClient.GetDatabase("task_manager_db").GetCollection<BsonDocument>("TM_Users");
            // var usr = collection.Find($"{{username: {data.Username}}}");
            
            // if ((await usr.CountDocumentsAsync()) > 0)
            // {
            //     return new JsonResult(new { error = true, message = "User already exists" });
            // }

            // collection.InsertOne(new BsonDocument
            // {
            //     { "username", data.Username },
            //     { "password", data.Password }
            // });

            var jwt = new JwtSecurityToken(
                issuer: data.Issuer,
                audience: data.Audience,
                claims: new List<Claim>() { new(ClaimTypes.Name, data.Username!) },
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: tokenService.SignAudienceKey()
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonResult(new { AuthToken = token });
        }

        // [HttpPost]
        // [Route("login")]
        // public JsonResult Login()
        // {
        //     return new JsonResult("Login");
        // }
    }
}

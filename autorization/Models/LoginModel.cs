using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TaskManager.Autorization.Extensions;
using TaskManager.Autorization.Models.DTO;
using TaskManager.Autorization.Services;

namespace TaskManager.Autorization.Models
{
    public class LoginModel
    {
        public static async Task<IActionResult> Login(LoginRequest login, TokenService tokenService, IMongoClient mongoClient)
        {
            using (var validation = ObjectsValidator.ValidateObject(login))
            {
                if (validation.IsError)
                {
                    return new JsonResult(validation)
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
            }

            var collection = mongoClient.GetDatabase("task_manager_db").GetCollection<BsonDocument>("TM_Users");
            var userBson = await 
                          (await collection.FindAsync($"{{ username: \"{login.Username}\", password: \"{login.Password!.EncodeSHA512()}\" }}"))
                                           .ToListAsync();

            if (!userBson.Any())
            {
                return new NotFoundResult();
            }

            var user = BsonSerializer.Deserialize<UserDTO>(userBson.First());

            var jwt = new JwtSecurityToken(
                issuer: login.Issuer,
                audience: login.Audience,
                claims: new List<Claim>()
                { 
                    new(ClaimTypes.Name, user.Username!),
                    new(ClaimTypes.Email, user.Email!),
                    new(ClaimTypes.Role, user.Role!)
                },
                expires: DateTime.UtcNow.AddHours(6),
                signingCredentials: tokenService.SignAudienceKey()
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new LoginResponse(user, token);

            return new JsonResult(response);
        }
    }
}
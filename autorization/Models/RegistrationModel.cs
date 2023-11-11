using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using TaskManager.Autorization.Extensions;
using TaskManager.Autorization.Models.DTO;

namespace TaskManager.Autorization.Models
{
    public class RegistrationModel
    {
        public static async Task<IActionResult> RegisterNewUser(RegisterRequest data, IMongoClient mongoClient)
        {
            using (var validation = ObjectsValidator.ValidateObject(data))
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
            var user = collection.Find($"{{ username: \"{data.Username}\" }}");
            
            if (await user.AnyAsync())
            {
                return new JsonResult(new ErrorResponse(true, "User already exists"))
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            collection.InsertOne(new BsonDocument
            {
                { "username", data.Username },
                { "password", data.Password!.EncodeSHA512() },
                { "email", data.Email },
                { "role", "user" }
            });

            return new CreatedResult(new Uri("login", UriKind.Relative), new { username = data.Username });
        }
    }
}
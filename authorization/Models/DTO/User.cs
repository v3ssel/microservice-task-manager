using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskManager.Autorization.Models.DTO
{
    [BsonIgnoreExtraElements]
    public class UserDTO
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("username")]
        public string? Username { get; set; }

        [BsonElement("email")]
        public string? Email { get; set; }

        [BsonElement("role")]
        public string? Role { get; set; }
    }
}

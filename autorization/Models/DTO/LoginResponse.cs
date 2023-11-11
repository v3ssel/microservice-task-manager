using System.ComponentModel.DataAnnotations;

namespace TaskManager.Autorization.Models.DTO
{
    public class LoginResponse : UserDTO
    {
        public string? Token { get; set; }

        public LoginResponse(UserDTO user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
            Role = user.Role;
            Token = token;
        }     
    }
}
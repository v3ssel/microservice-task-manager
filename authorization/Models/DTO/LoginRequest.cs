using System.ComponentModel.DataAnnotations;

namespace TaskManager.Autorization.Models.DTO
{
    public class LoginRequest
    {
        [Required]
        public string? Username { get; set; }
        
        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Issuer { get; set; }

        [Required]
        public string? Audience { get; set; }
        
    }
}
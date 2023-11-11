using System.ComponentModel.DataAnnotations;

namespace TaskManager.Autorization.Models.DTO
{
    public class RegisterRequest
    {
        [Required]
        public string? Username { get; set; }
        
        [Required]
        public string? Password { get; set; }

        [Required]
        // [RegularExpression("")]
        public string? Email { get; set; }
    }
}
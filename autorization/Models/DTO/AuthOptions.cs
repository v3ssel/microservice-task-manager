using System.ComponentModel.DataAnnotations;

namespace TaskManager.Autorization.Models.DTO
{
    public class AuthOptions
    {
        [Required]
        public IEnumerable<string>? ISSUERS { get; set; }
     
        [Required]
        public IEnumerable<string>? AUDIENCE { get; set; }
     
        [Required]
        public string? PRIVATE_KEY_PATH { get; set; }
        
        [Required]
        public string? PUBLIC_KEY_PATH { get; set; }
    }
}
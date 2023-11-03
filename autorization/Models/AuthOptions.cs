using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TaskManager.Autorization.Models
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
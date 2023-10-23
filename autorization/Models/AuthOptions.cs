using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Autorization.Models
{
    public class AuthOptions
    {
        [Required]
        public string? ISSUER { get; set; }
     
        [Required]
        public string? AUDIENCE { get; set; }
     
        [Required]
        public string? SECRET_KEY { get; set; }
    }
}
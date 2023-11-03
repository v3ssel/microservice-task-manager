namespace TaskManager.Autorization.Models
{
    public class RegisterRequest
    {
        public string? Issuer { get; set; }

        public string? Audience { get; set; }
        
        public string? Username { get; set; }
        
        public string? Password { get; set; }
    }
}
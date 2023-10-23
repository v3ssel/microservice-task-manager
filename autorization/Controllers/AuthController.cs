using Autorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Autorization.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("welcome")]
        public string Home(AuthOptions configuration)
        {
            return $"Welcome {configuration.SECRET_KEY}!"; 
        }

        [HttpPost]
        [Route("register")]
        public JsonResult Register(RegisterRequest data)
        {
            return new JsonResult("Login");
        }

        // [HttpPost]
        // [Route("login")]
        // public JsonResult Login()
        // {
        //     return new JsonResult("Login");
        // }
    }
}

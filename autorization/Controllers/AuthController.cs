using TaskManager.Autorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TaskManager.Autorization.Services;
using TaskManager.Autorization.Models.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace TaskManager.Autorization.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly MongoClient _mongoClient;

        public AuthController(ILogger<AuthController> logger, MongoClient mongoClient)
        {
            _logger = logger;
            _mongoClient = mongoClient;
        }

        [HttpGet]
        [Route("welcome")]
        public string Home()
        {
            return $"Welcome !"; 
        }

        [HttpGet]
        [Route("validate-token")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
        public IActionResult ValidateToken()
        {
            return Ok(HttpContext.User.Claims.Select(x => { return x.ValueType + " " + x.Value + " " + x.Type; })); 
        }

        [HttpGet]
        [Route("validate-admin-token")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public IActionResult ValidateAdminToken()
        {
            return Ok(HttpContext.User.Claims.Select(x => { return x.ValueType + " " + x.Value + " " + x.Type; })); 
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequest data)
        {
            try
            {
               return await RegistrationModel.RegisterNewUser(data, _mongoClient);
            }
            catch (Exception ex) 
            {
                return BadRequest(new ErrorResponse(true, ex.Message));
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest login, TokenService tokenService)
        {
            try
            {
               return await LoginModel.Login(login, tokenService, _mongoClient);
            }
            catch (Exception ex) 
            {
                return BadRequest(new ErrorResponse(true, ex.Message));
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System;
using TestApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using TestApplication.Services;
using TestApplication.Models;
using System.Linq;

namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly TokenService _tokenService;

        public UserController(DatabaseContext context)
        {
            _context = context;
            _tokenService = new TokenService("YourSecretKeyHere"); // Ensure this key matches the one used in Startup.cs
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginViewModel model)
        {
            var user = _context.Accounts.SingleOrDefault(u => u.Username.Equals(model.Username, StringComparison.OrdinalIgnoreCase));

            if (user == null)
                return Unauthorized();

            // Hash the password from the request to compare with stored hash
            using var md5 = MD5.Create();
            var passwordHash = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(model.Password)))
                .Replace("-", "").ToLower();

            if (user.PasswordHash != passwordHash)
                return Unauthorized();

            var token = _tokenService.GenerateToken(user.UserId, user.Username);
            return Ok(token);
        }
    }
}

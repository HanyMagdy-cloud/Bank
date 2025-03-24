using Bank.Repository.Entities;
using Bank.Repository.Entities.Services;
using Bank.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IBank _bank;

        public AuthController(UserService userService, IBank bank)
        {
            _userService = userService;
            _bank = bank;
        }



        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginRequest)
        {
            if (string.IsNullOrWhiteSpace(loginRequest.Name) || string.IsNullOrWhiteSpace(loginRequest.PasswordHash))
            {
                return BadRequest("Username and password are required.");
            }

            // Check if the credentials are valid using UserService
            var authenticatedUser = _userService.ValidateUser(loginRequest);

            if (authenticatedUser == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            // Generate JWT token
            var token = _userService.GenerateToken(authenticatedUser);

            return Ok(new
            {
                Token = token,
                Role = authenticatedUser.Role
            });
        }

    }
}

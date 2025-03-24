using Bank.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{

    [Authorize(Roles = "Admin")]  // Restrict access to users with the "Admin" role.
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository; // Dependency for accessing user data.

        // Constructor with dependency injection for the user repository.
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAllUsers() // Return IActionResult
        {
            var users = _userRepository.GetAllUsers();
            return Ok(users); // This now returns a single IActionResult (OkObjectResult)
        }

        [HttpGet("{id}")] // Example of getting a user by ID
        public IActionResult GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) // Check if the user exists.
            {
                return NotFound("User not found"); // Return 404 if user not found
            }
            return Ok(user);
        }


        // Endpoint to fetch a user by their username.

        [HttpGet("byusername/{username}")] // New endpoint
        public IActionResult GetUserByUsername(string username)
        {
            var user = _userRepository.GetUserByUsername(username); // Call the repository to fetch a user by username.

            if (user == null)
            {
                return NotFound("User not found!"); // Return 404 Not Found if the user doesn't exist
            }
            else
            {
                // Return user details
                var userDetails = new
                {
                    user.Id,
                    user.Name,
                    user.Role
                };
                return Ok(userDetails);
            }
        }
    }
}

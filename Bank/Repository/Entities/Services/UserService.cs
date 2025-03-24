using Bank.Repository.Interfaces;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bank.Repository.Entities.Services
{
    // This service provides methods for user authentication and token generation.
    public class UserService
    {
        // Private fields for repository and database access

        private readonly IUserRepository _userRepo;

        private readonly IBank _bank;


        // Constructor to inject dependencies for database and repository
        public UserService(IBank bank, IUserRepository userRepository)
        {
            _bank = bank; // Injects the IBank implementation for database access

            _userRepo = userRepository; // Injects the IUserRepository for user operations
        }

        // Method to validate user login credentials
        public bool Login(User user)
        {
            // Fetch the user from the database by username

            var dbUser = _userRepo.GetUserByUsername(user.Name);

            // Validate the provided credentials against the database
            if (dbUser != null && dbUser.PasswordHash == user.PasswordHash)
            {
                return true; // Login successful
            }

            return false; // Invalid credentials
        }

        // Method to validate user credentials directly using a query
        public User? ValidateUser(User loginRequest)
        {
            using var connection = _bank.GetConnection();    // Get a database connection
            var query = "SELECT * FROM Users WHERE Name = @Name AND PasswordHash = @PasswordHash";

            // Execute query to fetch the user with matching credentials

            return connection.QueryFirstOrDefault<User>(query, new
            {
                loginRequest.Name,
                loginRequest.PasswordHash
            });
        }


        // Method to generate a JWT for authenticated users
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),  // Adds the user's name
            new Claim(ClaimTypes.Role, user.Role)   // Adds the user's role
        };

            // Define a secret key for token encryption

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mykey1234567&%%485734579453%&//1255362"));

            // Specify the signing credentials for the token

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // Create the token configuration

            var tokenOptions = new JwtSecurityToken(
                issuer: "http://localhost:5290",
                audience: "http://localhost:5290",
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: signinCredentials
            );

            // Generate and return the JWT as a string

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}


using Bank.Repository;
using Bank.Repository.Entities;
using Bank.Repository.Interfaces;
using Microsoft.Extensions.Configuration;



namespace Bank.Tests
{
    public class UserRepoTests
    {
        private readonly UserRepo _userRepo;


        public UserRepoTests()
        {
            // Build configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Ensure this points to the correct path
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Initialize BankContext with configuration
            var bankContext = new BankContext(configuration);

            // Pass BankContext to UserRepo
            _userRepo = new UserRepo(bankContext);
        }

        [Fact]
        public void GetCustomerLoans_ValidCustomerId_ReturnsLoans()
        {
            // Arrange
            int validCustomerId = 2; //  a valid customer ID from database

            // Act
            var loans = _userRepo.GetCustomerLoans(validCustomerId);

            // Assert
            Assert.NotNull(loans);
            Assert.NotEmpty(loans); 
        }

        [Fact]
        public void GetCustomerLoans_InvalidCustomerId_ReturnsEmptyList()
        {
            // Arrange
            int invalidCustomerId = -3; //  an invalid customer ID

            // Act
            var loans = _userRepo.GetCustomerLoans(invalidCustomerId);

            // Assert
            Assert.NotNull(loans);
            Assert.Empty(loans); 
        }

        [Fact]
        public void GetCustomerAccounts_ValidCustomerId_ReturnsAccounts()
        {
            // Arrange
            int validCustomerId = 3; 

            // Act
            var accounts = _userRepo.GetCustomerAccounts(validCustomerId);

            // Assert
            Assert.NotNull(accounts); 
            Assert.NotEmpty(accounts); 

            
        }
    }
}
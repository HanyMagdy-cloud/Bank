using Bank.Repository.Entities;
using Bank.Repository.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

public class UserRepo : IUserRepository
{
    private readonly IBank _bank;  //Dependency for accessing database connections.

    // Constructor to inject the IBank dependency.
    public UserRepo(IBank bank)
    {
        _bank = bank; // Assigns the provided IBank instance to the private field
    }

    // Fetch all users from the database using a stored procedure.
    public List<User> GetAllUsers()
    {
        using var connection = _bank.GetConnection(); // Establish a database connection.
        return connection.Query<User>("GetAllUsers", commandType: CommandType.StoredProcedure).AsList();
    }

    // Fetch a single user by username using a stored procedure.
    public User? GetUserByUsername(string UserName)
    {
        using var connection = _bank.GetConnection();  // Establish a database connection.
        return connection.QueryFirstOrDefault<User>(
            "GetUserByUsername",
            new { Name = UserName },
            commandType: CommandType.StoredProcedure
        );
    }

    // Fetch a single user by their unique ID using a stored procedure.
    public User? GetUserById(int id)
    {
        using var connection = _bank.GetConnection();
        return connection.QueryFirstOrDefault<User>(
            "GetUserById",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    // Add a new user to the database using a stored procedure.
    //public bool CreateUser(User user)
    //{
    //    using var connection = _bank.GetConnection();
    //    var rowsAffected = connection.Execute(
    //        "CreateUser",
    //        new { user.Name, user.PasswordHash, user.Role },
    //        commandType: CommandType.StoredProcedure
    //    );
    //    return rowsAffected > 0;   // Returns true if at least one row is affected.
    //}

    // Validate user credentials using a stored procedure.
    public User? ValidateUser(User loginRequest)
    {
        using var connection = _bank.GetConnection();

        try
        {
            // Execute the stored procedure
            var user = connection.QueryFirstOrDefault<User>(
                "ValidateUser", // Name of the stored procedure
                new
                {
                    Name = loginRequest.Name, // Pass the username.
                    PasswordHash = loginRequest.PasswordHash  // Pass the hashed password.
                },
                commandType: CommandType.StoredProcedure // Indicate it’s a stored procedure
            );

            // If a user is found, return it; otherwise, return null
            return user;
        }
        catch (Exception ex)
        {
            // Log or handle exception as needed
            throw new Exception("Error occurred while validating the user.", ex);
        }
    }


    // Get all accounts for a customer
    public List<Account> GetCustomerAccounts(int customerId)
    {

        using var connection = _bank.GetConnection();

        try
        {
            // Execute the stored procedure using Dapper
            return connection.Query<Account>(
                "GetCustomerAccounts",
                new { CustomerId = customerId },
                commandType: CommandType.StoredProcedure
            ).ToList();
        }
        catch (Exception ex)
        {
            // Log or handle exception as needed
            throw new Exception("Error occurred while retrieving customer accounts.", ex);
        }

    }

    public List<Loan> GetCustomerLoans(int customerId)
    {
        using var connection = _bank.GetConnection();

        try
        {
            // Execute the stored procedure to fetch customer loans
            var loans = connection.Query<Loan>(
                "GetCustomerLoans", // Name of the stored procedure
                new { CustomerId = customerId },
                commandType: CommandType.StoredProcedure // Indicate it's a stored procedure
            ).ToList();

            return loans;
        }
        catch (Exception ex)
        {
            // Log or handle exception as needed
            throw new Exception("Error occurred while retrieving customer loans.", ex);
        }
        
    }

}

using Bank.Repository.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Bank.Repository.Entities
{
    public class CustomerService
    {
        private readonly UserRepo _userRepo;

        private readonly IBank _bank;

        // Constructor for dependency injection.
        public CustomerService(UserRepo userRepo, IBank bank)
        {
            _userRepo = userRepo;
            _bank = bank;
        }

        // Retrieves a list of accounts for a specific customer.
        public List<Account> GetCustomerAccounts(int CustomerId)
        {
            return _userRepo.GetCustomerAccounts(CustomerId);


        }
        // Retrieves a list of loans for a specific customer.
        public List<Loan> GetCustomerLoans(int customerId)
        {
            return _userRepo.GetCustomerLoans(customerId);
        }
        // Creates a new bank account for a customer.
        public string CreateNewBankAccount(int customerId, string accountType)
        {

            using var connection = _bank.GetConnection(); // Opens a connection using IBank.

            // Create parameters for the stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@CustomerId", customerId, DbType.Int32);
            parameters.Add("@Type", accountType, DbType.String);

            // Define an output parameter for the generated account number
            parameters.Add("@GeneratedAccountNumber", dbType: DbType.String, size: 20, direction: ParameterDirection.Output);

            try
            {
                // Execute the stored procedure
                connection.Execute(
                    "CreateCustomerAccount",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                // Retrieve the generated account number from the output parameter.
                string generatedAccountNumber = parameters.Get<string>("@GeneratedAccountNumber");

                return generatedAccountNumber;
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("Error occurred while creating a new bank account.", ex);
            }


        }

        // Deposits money into an account.
        public string DepositToAccount(string accountNumber, decimal depositAmount)
        {
            using var connection = _bank.GetConnection();

            // Create parameters for the stored procedure.

            var parameters = new DynamicParameters();
            parameters.Add("@AccountNumber", accountNumber, DbType.String);
            parameters.Add("@DepositAmount", depositAmount, DbType.Decimal);

            try
            {
                // Execute the stored procedure
                var result = connection.QuerySingleOrDefault<string>(
                    "Insert_Deposit",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                // Return result or a default error message if the result is null.

                return result ?? "Deposit failed. Please check the account number or amount.";
            }
            catch (Exception ex)
            {
                // Log exception as needed
                throw new Exception("An error occurred ! check the Account nuber.", ex);
            }
        }

        public bool ChangeCustomerPassword(int customerId, string oldPassword, string newPassword)
        {
            using var connection = _bank.GetConnection(); // Assuming `_bank` provides the DB connection.

            try
            {
                // Call the stored procedure to change the password
                var rowsAffected = connection.Execute(
                    "dbo.ChangeCustomerPassword", // Name of your stored procedure
                    new
                    {
                        CustomerId = customerId,
                        OldPassword = oldPassword,
                        NewPassword = newPassword
                    },
                    commandType: CommandType.StoredProcedure
                );

                return rowsAffected > 0; // Return true if the operation succeeded
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }


    }
}

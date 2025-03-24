using AutoMapper;
using Bank.Repository.DTOs;
using Bank.Repository.Interfaces;
using Dapper;
using System.Data;

namespace Bank.Repository.Entities.Services
{
    public class TransactionService
    {
        private readonly IBank _bank; // Dependency for managing database connections.
        private readonly IMapper _mapper;  // Mapper for converting between DTOs and entities.

        // Constructor for dependency injection.
        public TransactionService(IBank bank, IMapper mapper)
        {
            _bank = bank;
            _mapper = mapper;
        }

        // Handles transferring funds between accounts.
        public bool MakeTransfer(TransactionDto transactionDto)
        {
            using var connection = _bank.GetConnection(); // Establish a database connection.

            // Map DTO to parameters for stored procedure
            var parameters = new
            {
                CustomerId = transactionDto.CustomerId,
                Amount = transactionDto.Amount,
                SourceAccountNumber = transactionDto.SourceAccountNumber,
                DestinationAccountNumber = transactionDto.DestinationAccountNumber
            };

            try
            {
                // Execute stored procedure
                var rowsAffected = connection.Execute(
                    "Make_a_Transfer",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return rowsAffected > 0; // Return true if transfer was successful
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new Exception("Error occurred while making the transfer.", ex);
            }
        }

        // Fetches a list of transactions for a specific customer.
        public List<TransactionDto> GetTransactionsByCustomerId(int customerId)
        {
            using var connection = _bank.GetConnection();
            var parameters = new { CustomerId = customerId }; // Parameter for the stored procedure.

            var transactions = connection.Query<TransactionDto>(
                "GetTransactionByCustomerId",
                parameters,
                commandType: CommandType.StoredProcedure
            ).ToList();

            // Return null if all results are default/empty
            if (transactions.All(t => t.CustomerId == 0 && t.Amount == 0 && t.SourceAccountNumber == null))
            {
                return null; // Indicates no transactions found
            }

            return transactions;
        }

    }
}

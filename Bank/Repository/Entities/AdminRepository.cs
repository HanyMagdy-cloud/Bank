//using Bank.Repository.DTOs;
//using Bank.Repository.Interfaces;
//using Dapper;
//using System.Data;

//namespace Bank.Repository.Entities
//{
//    public class AdminRepository : IAdminRepository
//    {

//        private readonly IBank _bank;

//        public AdminRepository(IBank bank)
//        {
//            _bank = bank;
//        }

//        //Add a new customer with a bank account

//        public bool AddCustomerWithAccount(CustomerWithAccountDto customerDto)
//        {
//            // Open a connection using IBank
//            using (var connection = _bank.GetConnection())
//            {
//                // Call the stored procedure to add a customer and their account
//                var rowsAffected = connection.Execute(
//                    "AddCustomerWithAccount",
//                    new
//                    {
//                        Name = customerDto.Name,
//                        Email = customerDto.Email,
//                        PasswordHash = customerDto.PasswordHash,
//                        Role = customerDto.Role,
//                        AccountType = customerDto.AccountType,
//                        InitialBalance = customerDto.InitialBalance
//                    },
//                    commandType: CommandType.StoredProcedure
//                );

//                return rowsAffected > 0; // Return true if at least one row was affected
//            }
//        }



//        public bool AddLoanForCustomer(LoanDto loanDto)
//        {
//            using var connection = _bank.GetConnection();

//            // Execute the stored procedure using the LoanDto properties
//            var rowsAffected = connection.Execute(
//                "AddLoanForCustomer",
//                new
//                {
//                    CustomerId = loanDto.CustomerId,
//                    AccountNumber = loanDto.AccountNumber,
//                    LoanAmount = loanDto.LoanAmount,
//                    InterestRate = loanDto.InterestRate,
//                    LoanDate = loanDto.LoanDate
//                },
//                commandType: CommandType.StoredProcedure
//            );

//            return rowsAffected > 0; // Return true if at least one row was affected
//        }




//    }
//}


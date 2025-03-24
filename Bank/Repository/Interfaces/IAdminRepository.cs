using Bank.Repository.DTOs;

namespace Bank.Repository.Interfaces
{
    public interface IAdminRepository
    {
        public bool AddCustomerWithAccount(CustomerWithAccountDto customerDto);
        //bool AddCustomerWithAccount(string username, string email, string password, string role, string accountType, decimal initialBalance);
        //bool AddLoanForCustomer(int customerId, string accountnumber, decimal loanAmount, decimal interestRate, DateTime loanDate);
        public bool AddLoanForCustomer(LoanDto loanDto);

    }
}

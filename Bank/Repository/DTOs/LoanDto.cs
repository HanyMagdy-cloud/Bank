namespace Bank.Repository.DTOs
{
    public class LoanDto
    {
        public int CustomerId { get; set; }
        public string AccountNumber { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime LoanDate { get; set; }
    }
}

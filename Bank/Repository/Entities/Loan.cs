namespace Bank.Repository.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public int CustomerId {  get; set; }
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime LoanDate { get; set; }
        public string AccountNumber { get; set; }
    }
}

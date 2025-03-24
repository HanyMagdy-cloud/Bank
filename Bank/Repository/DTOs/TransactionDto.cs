namespace Bank.Repository.DTOs
{
    public class TransactionDto
    {
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string SourceAccountNumber { get; set; }
        public string DestinationAccountNumber { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}

namespace Bank.Repository.Entities
{
    public class Transaction
    {
        public int Id { get; set; } // Primary Key
        public int CustomerId { get; set; } 
        public decimal Amount { get; set; } // Transaction amount
        public string SourceAccountNumber { get; set; } // Nullable for deposits
        public string DestinationAccountNumber { get; set; } // Nullable for withdrawals
        public DateTime TransactionDate { get; set; } // Timestamp of the transaction
    }
}

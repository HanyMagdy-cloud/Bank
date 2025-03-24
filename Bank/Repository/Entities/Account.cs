namespace Bank.Repository.Entities
{
    public class Account
    {
       // public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string Type { get; set; } // Savings, Personal, etc.
        public decimal Balance { get; set; }
    }
}

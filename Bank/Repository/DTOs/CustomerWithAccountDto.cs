namespace Bank.Repository.DTOs
{
    public class CustomerWithAccountDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string AccountType { get; set; }
        public decimal InitialBalance { get; set; }
    }
}

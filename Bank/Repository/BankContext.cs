using Bank.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Bank.Repository
{

    // implementation of the IBank interface

    public class BankContext : IBank

    {

        // Private field to hold the connection string for the database.
        private readonly string _connString;

        public BankContext()
        {
            _connString = string.Empty; // Avoid null references

        }

        // Constructor to initialize the connection string using IConfiguration

        public BankContext(IConfiguration config)
        {
            // Get the connection string from the configuration and ensure it is valid

            _connString = config.GetConnectionString("Bank")
            ?? throw new InvalidOperationException("Connection string 'Bank' not found.");
        }

        public SqlConnection GetConnection()
        {    // Ensure the connection string is initialized before returning the connection
            if (string.IsNullOrEmpty(_connString))
            {
                throw new InvalidOperationException("The connection string has not been initialized.");
            }

            return new SqlConnection(_connString);
        }
    }
}

using Microsoft.Data.SqlClient;

namespace Bank.Repository.Interfaces
{

    // Interface to provide a method for retrieving a database connection
    public interface IBank
    { 
       // Method to get an open SQL connection
        public SqlConnection GetConnection();
    }
}

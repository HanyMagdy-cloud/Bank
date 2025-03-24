using Bank.Repository.Entities;

namespace Bank.Repository.Interfaces
{
    public interface IUserRepository
    {
        public List<User> GetAllUsers();
        public User? GetUserByUsername(string UserName);
        public User? GetUserById(int id);
        //public bool CreateUser(User user);
    }
}

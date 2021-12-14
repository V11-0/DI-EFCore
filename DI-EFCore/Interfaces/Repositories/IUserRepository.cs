using DI_EFCore.Entities;

namespace DI_EFCore.Interfaces.Repositories {
    public interface IUserRepository {
        Task<List<User>> GetAllUsers();
        Task<User?> GetUser(int id);
        Task<User> AddUser(User user);
        Task DeleteUser(int id);
    }
}
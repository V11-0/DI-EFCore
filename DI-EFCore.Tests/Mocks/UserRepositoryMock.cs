using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DI_EFCore.Entities;
using DI_EFCore.Interfaces.Repositories;

namespace DI_EFCore.Tests.Mocks
{
    public class UserRepositoryMock : IUserRepository {

        List<User> users = new List<User>();

        public UserRepositoryMock() {
            User user1 = new User("V11");
            user1.Id = 1;

            User user2 = new User("She");
            user2.Id = 2;

            users.Add(user1);
            users.Add(user2);
        }

        public Task<User> AddUser(User user) {
            throw new System.NotImplementedException();
        }

        public async Task<User?> GetUser(int id) {
            await Task.Delay(0);
            return users.Where(u => u.Id == id).SingleOrDefault();
        }

        public Task DeleteUser(User user) {
            throw new System.NotImplementedException();
        }

        public Task<List<User>> GetAllUsers() {
            throw new System.NotImplementedException();
        }
    }
}
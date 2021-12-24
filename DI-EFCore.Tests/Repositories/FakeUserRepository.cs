using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DI_EFCore.Entities;
using DI_EFCore.Interfaces.Repositories;

namespace DI_EFCore.Tests.Repositories
{
    public class FakeUserRepository : IUserRepository {

        List<User> users = new List<User>();

        public FakeUserRepository() {
            User user1 = new User("Name1");
            user1.Id = 0;

            User user2 = new User("Name2");
            user2.Id = 1;

            users.Add(user1);
            users.Add(user2);
        }

        public async Task<User> AddUser(User user) {
            await Task.Delay(0);

            users.Add(user);

            return user;
        }

        public async Task<User?> GetUser(int id) {
            await Task.Delay(0);
            return users.Where(u => u.Id == id).SingleOrDefault();
        }

        public async Task DeleteUser(User user) {
            await Task.Delay(0);
            users.Remove(user);
        }

        public Task<List<User>> GetAllUsers() {
            throw new System.NotImplementedException();
        }
    }
}
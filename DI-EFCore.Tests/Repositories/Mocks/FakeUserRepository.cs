using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DI_EFCore.Entities;
using DI_EFCore.Repositories.Interfaces;

namespace DI_EFCore.Tests.Repositories.Mocks
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

        public Task<User> AddUser(User user) {
            users.Add(user);
            return Task.FromResult(user);
        }

        public Task<User?> GetUser(int id) {
            var user = users.Where(u => u.Id == id).SingleOrDefault();
            return Task.FromResult(user);
        }

        public Task DeleteUser(User user) {
            users.Remove(user);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<User>> GetAllUsers() {
            return Task.FromResult(users.AsEnumerable());
        }
    }
}
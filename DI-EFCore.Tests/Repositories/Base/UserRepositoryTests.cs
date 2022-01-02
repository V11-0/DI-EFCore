using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DI_EFCore.Entities;
using DI_EFCore.Models;
using DI_EFCore.Repositories;

using DI_EFCore.Tests.Data;

namespace DI_EFCore.Tests.Repositories.Base
{

    public abstract class UserRepositoryTests {

        public readonly DbContextOptions<AppDbContext> _contextOptions;

        protected UserRepositoryTests(DbContextOptions<AppDbContext> contextOptions) {
            _contextOptions = contextOptions;

            var context = new AppDbContext(contextOptions);
            DataSeed.Seed(context);
        }

        private UserRepository _repository() {
            return new UserRepository(new AppDbContext(_contextOptions));
        }

        [TestMethod]
        public async Task GetAllUsers_ReturnsUsersEnumerable() {
            var users = await _repository().GetAllUsers();
            Assert.IsInstanceOfType(users, typeof(IEnumerable<User>));

            var userCount = users.Count();
            Assert.IsTrue(userCount > 0);
        }

        [TestMethod]
        [DataRow(1)]
        public async Task GetUser_ValidId_ReturnsUser(int userId) {
            var user = await _repository().GetUser(userId);

            Assert.IsNotNull(user);
            Assert.IsInstanceOfType(user, typeof(User));
        }

        [TestMethod]
        [DataRow(100)]
        public async Task GetUser_NonexistentId_ReturnsNull(int userId) {
            var user = await _repository().GetUser(userId);

            Assert.IsNull(user);
        }

        [TestMethod]
        public async Task AddUser_CheckCreatedId() {
            var user = new User("Name");
            await _repository().AddUser(user);

            var createdId = user.Id;

            Assert.IsNotNull(createdId);
        }

        [TestMethod]
        [DataRow(1)]
        public async Task DeleteUser_CheckNull(int userId) {
            var repo = _repository();

            var existentUser = await repo.GetUser(userId);
            Assert.IsNotNull(existentUser);

            await repo.DeleteUser(existentUser);

            var nonExistingUser = await repo.GetUser(userId);
            Assert.IsNull(nonExistingUser);
        }
    }
}
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;

using DI_EFCore.Controllers;
using DI_EFCore.Tests.Repositories;
using DI_EFCore.Entities;

namespace DI_EFCore.Tests.Controllers {

    [TestClass]
    public class UserControllerTests {
        private UserController _controller;

        public UserControllerTests() {
            _controller = new UserController(new FakeUserRepository());
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        public async Task GetUser_ValidId_ReturnsUser(int userId) {
            var actionResult = (await _controller.GetUser(userId)).Result;
            Assert.IsTrue(actionResult is OkObjectResult);

            var okResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okResult);

            var expectedUser = okResult.Value as User;
            Assert.IsNotNull(expectedUser);
        }

        [TestMethod]
        [DataRow(10)]
        [DataRow(20)]
        public async Task GetUser_NonExistentId_ReturnsNotFound(int userId) {
            var actionResult = (await _controller.GetUser(userId)).Result;

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PostUser_EmptyUsername_ReturnsBadRequest() {
            var invalidUser = new User("");

            var actionResult = (await _controller.PostUser(invalidUser)).Result;

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PostUser_ValidUser_ReturnsCreated() {
            var validUser = new User("Aname");

            var actionResult = (await _controller.PostUser(validUser)).Result;

            Assert.IsInstanceOfType(actionResult, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        public async Task DeleteUser_ValidId_ReturnsNoContent(int userId) {
            var actionResult = (await _controller.DeleteUser(userId));

            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
        }

        [TestMethod]
        [DataRow(10)]
        [DataRow(20)]
        public async Task DeleteUser_NonExistingId_ReturnsNotFound(int userId) {
            var actionResult = (await _controller.DeleteUser(userId));

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
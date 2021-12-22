using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;

using DI_EFCore.Controllers;
using DI_EFCore.Tests.Mocks;
using System.Threading.Tasks;
using DI_EFCore.Entities;

namespace DI_EFCore.Tests.Controllers;

[TestClass]
public class UserControllerTests {
    private UserController _controller;

    public UserControllerTests() {
        _controller = new UserController(new UserRepositoryMock());
    }

    [TestMethod]
    public async Task GetUserSuccess() {
        var actionResult = await _controller.GetUser(1);

        Assert.IsTrue(actionResult.Result is OkObjectResult);

        var okResult = actionResult.Result as OkObjectResult;
        var user = okResult.Value as User;

        Assert.IsNotNull(user);
        Assert.AreEqual("V11", user.Username);
    }

    [TestMethod]
    public async Task GetUserNotFound() {
        var actionResult = await _controller.GetUser(5);

        Assert.IsTrue(actionResult.Result is NotFoundResult);
    }
}
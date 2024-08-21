using Workspace.BLL.Logic.Contracts;
using Workspace.Entities;
using Workspace.Tests.DI;

namespace Workspace.BLL.Tests;

public class UserServiceTests : BaseFixture
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task CreateUser_PasswordNull_Shoud_NullReferenceException_Test()
    {
        //Arrange
        var userService = ResolveServices<IUserService>();
        var request = new WorkspaceUserRequest
        {
            Login = "test",
            Password = null,
            Name = "test",
            Surname = "test",
        };

        Assert.ThrowsAsync<NullReferenceException>(async () => await userService.CreateAsync(request));
    }
}

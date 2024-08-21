using Microsoft.Extensions.DependencyInjection;
using Moq;
using Microsoft.Extensions.Logging;
using Workspace.Auth;
using Workspace.BLL.Logic;
using Workspace.BLL.Logic.Contracts;
using Workspace.Auth;
using Workspace.DAL;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Workspace.Entities;
using Workspace.Entities.Contracts;

namespace Workspace.Tests.DI;

public abstract class BaseFixture : BaseFixtureBase
{
    private IServiceCollection _serviceCollection;

    protected Mock<T> RegisterMock<T>() where T : class
    {
        if (_serviceProvider is not null)
        {
            var service = _serviceProvider.GetService<T>();
            var mockService = Mock.Get(service);

            return mockService;
        }

        var mock = new Mock<T>();
        _serviceCollection.AddSingleton(mock.Object);

        return mock;
    }

    protected T ResolveServices<T>() where T : class
    {
        if (_serviceProvider is null)
        {
            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        return _serviceProvider.GetService<T>();
    }

    [OneTimeSetUp]
    public void Setup()
    {
        _serviceCollection = new ServiceCollection();
        _serviceCollection.AddSingleton<IUserService, UserService>();
        _serviceCollection.AddSingleton<IWorkspaceUser, WorkspaceUser>();
        _serviceCollection.AddSingleton(Mock.Of<IMapper>());
        _serviceCollection.AddSingleton(Mock.Of<IUserRepository>());
        _serviceCollection.AddSingleton(Mock.Of<IWorkspacePasswordHasher>());
        _serviceCollection.AddSingleton(Mock.Of<IJwtProvider>());
        _serviceCollection.AddSingleton(Mock.Of<ILogger<UserService>>());
        _serviceCollection.AddLogging();

        _serviceProvider = _serviceCollection.BuildServiceProvider();
    }
}

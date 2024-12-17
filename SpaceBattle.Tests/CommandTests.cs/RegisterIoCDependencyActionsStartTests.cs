namespace SpaceBattle.Tests;

using Hwdtech;
using Hwdtech.Ioc;
using SpaceBattle.Lib;
public class RegisterIoCDependencyActionsStartTests
{
    public RegisterIoCDependencyActionsStartTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set",
        IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void RegisterIoCDependencyActionsStart_Should_Register_Start_Command()
    {
        // Arrange
        var gameObject = new Dictionary<string, object>();

        // Act
        new RegisterIoCDependencyActionsStart().Execute();
        var command = IoC.Resolve<ICommand>("Actions.Start", gameObject, "Move");

        // Assert
        Assert.IsType<StartCommand>(command);
    }
}

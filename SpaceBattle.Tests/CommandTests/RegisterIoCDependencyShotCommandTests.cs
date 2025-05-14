using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Lib.Tests;

public class RegisterIoCDependencyShotCommandTests
{
    [Fact]
    public void RegisterIoCDependencyShotCommand_Successfully_Registers_Command()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<ICommand>("Scopes.Current.Set", scope).Execute();

        var registerCommand = new RegisterIoCDependencyShotCommand();

        registerCommand.Execute();

        var shootingObj = new Mock<IShooting>().Object;
        var resolvedCommand = IoC.Resolve<ICommand>("Commands.Shot", shootingObj);

        Assert.NotNull(resolvedCommand);
        Assert.IsType<ShotCommand>(resolvedCommand);
    }

    [Fact]
    public void RegisterIoCDependencyShotCommand_Throws_When_Executed_Multiple_Times()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<ICommand>("Scopes.Current.Set", scope).Execute();

        var registerCommand = new RegisterIoCDependencyShotCommand();

        registerCommand.Execute();

        Assert.Throws<Exception>(() => registerCommand.Execute());
    }
}

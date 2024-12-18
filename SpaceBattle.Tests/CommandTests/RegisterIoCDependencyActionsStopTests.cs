namespace SpaceBattle.Tests;

using Hwdtech;
using Hwdtech.Ioc;
using SpaceBattle.Lib;

public class RegisterIoCDependencyActionsStopTests
{
    public RegisterIoCDependencyActionsStopTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set",
        IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void RegisterIoCDependencyActionsStop_Should_Register_Stop_Command()
    {
        var gameObject = new Dictionary<string, object>();

        new RegisterIoCDependencyActionsStop().Execute();
        var stopcmd = IoC.Resolve<ICommand>("Actions.Stop", gameObject, "Move");

        Assert.IsType<StopCommand>(stopcmd);
    }
}

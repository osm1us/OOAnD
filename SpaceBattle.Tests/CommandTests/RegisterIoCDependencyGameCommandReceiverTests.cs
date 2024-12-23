using Hwdtech.Ioc;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyGameCommandReceiverTests
{
    public RegisterIoCDependencyGameCommandReceiverTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
        ).Execute();
    }

    [Fact]
    public void RegisterIoCDependencyGameCommandReceiver_Should_Register_Receiver()
    {
        var q = new Queue<ICommand>();
        IoC.Resolve<ICommand>("IoC.Register", "Game.Queue",
            (object[] args) => q).Execute();

        new RegisterIoCDependencyGameCommandReceiver().Execute();

        var receiver = IoC.Resolve<ICommandReceiver>("Game.CommandsReceiver");
        Assert.IsType<QueueCommandReceiver>(receiver);
    }
}

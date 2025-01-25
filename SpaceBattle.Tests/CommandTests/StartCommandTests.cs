namespace SpaceBattle.Tests;

using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

public class StartCommandTests
{
    public StartCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
        ).Execute();

        var cmd = new Mock<ICommand>();
        IoC.Resolve<ICommand>("IoC.Register", "Commands.Move", (object[] args) =>
            cmd.Object).Execute();

        var injectable = new Mock<ICommand>().As<ICommandInjectable>();

        IoC.Resolve<ICommand>("IoC.Register", "Commands.CommandInjectable",
            (object[] args) => injectable.Object).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Macro.Move",
            (object[] args) => cmd.Object).Execute();

        var receiver = new Mock<ICommandReceiver>();
        IoC.Resolve<ICommand>("IoC.Register", "Game.CommandsReceiver",
            (object[] args) => receiver.Object).Execute();

        new RegisterIoCDependencySendCommand().Execute();
    }

    [Fact]
    public void StartCommand_Should_Create_Repeatable_Command()
    {
        var gameObject = new Dictionary<string, object>();
        var startCommand = new StartCommand(gameObject, "Move");
        startCommand.Execute();

        Assert.True(gameObject.ContainsKey("repeatableMove"));
        var receiver = IoC.Resolve<ICommandReceiver>("Game.CommandsReceiver");
        Mock.Get(receiver).Verify(r => r.Receive(It.IsAny<ICommand>()), Times.Once());
    }
}

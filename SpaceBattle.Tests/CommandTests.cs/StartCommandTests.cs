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
        IoC.Resolve<ICommand>("IoC.Register", "Commands.CommandInjectable", (object[] args) =>
            injectable.Object).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Commands.Macro", (object[] args) =>
            cmd.Object).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Commands.Repeat", (object[] args) =>
            cmd.Object).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Specs.Move", (object[] args) =>
            new string[] { "Commands.Move", "Commands.Repeat" }).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Specs.Rotate", (object[] args) =>
            new string[] { "Commands.Rotate", "Commands.Repeat" }).Execute();

        new RegisterIoCDependencyMacroMoveRotate().Execute();
    }

    [Fact]
    public void StartCommand_Should_Create_Repeatable_Command()
    {
        var gameObject = new Dictionary<string, object>();
        var q = new Queue<ICommand>();
        var startCommand = new StartCommand(gameObject, q, "Move");

        startCommand.Execute();

        Assert.True(gameObject.ContainsKey("repeatableMove"));
    }
}

namespace SpaceBattle.Tests;

using System.Diagnostics;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

public class StopCommandTests
{
    public StopCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set",
        IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void StopCommand_Should_Execute_In_Constant_Time()
    {
        var injectable = new Mock<ICommandInjectable>();
        var gameObject = new Dictionary<string, object>
        {
            ["repeatableMove"] = injectable.Object
        };
        var stopCommand = new StopCommand(gameObject, "Move");

        var sw = Stopwatch.StartNew();
        stopCommand.Execute();
        sw.Stop();

        injectable.Verify(c => c.Inject(It.IsAny<EmptyCommand>()));
        Assert.True(sw.ElapsedMilliseconds < 100);
        Assert.False(gameObject.ContainsKey("repeatableMove"));
    }

    [Fact]
    public void StopCommand_Should_Throw_Exception_If_Thereisnostartfckimtiredreally()
    {
        var gameObject = new Dictionary<string, object>();
        var stopCommand = new StopCommand(gameObject, "Move");
        Assert.Throws<InvalidOperationException>(() => stopCommand.Execute());
    }
}

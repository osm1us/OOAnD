using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class QueueCommandReceiverTests
{
    public QueueCommandReceiverTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
        ).Execute();
    }

    [Fact]
    public void QueueCommandReceiver_Should_Add_Command_To_Queue()
    {
        var q = new Queue<ICommand>();
        var cmd = new Mock<ICommand>();

        IoC.Resolve<ICommand>("IoC.Register", "Game.Queue",
            (object[] args) => q).Execute();

        var receiver = new QueueCommandReceiver();
        receiver.Receive(cmd.Object);

        Assert.Single(q);
    }

    [Fact]
    public void QueueCommandReceiver_Should_Throw_When_Queue_Not_Found()
    {
        var cmd = new Mock<ICommand>();
        var receiver = new QueueCommandReceiver();

        Assert.Throws<ArgumentException>(() => receiver.Receive(cmd.Object));
    }
}

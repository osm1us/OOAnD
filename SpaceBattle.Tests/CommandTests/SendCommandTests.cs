using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class SendCommandTests
{
    private readonly Mock<ICommandReceiver> receiver;

    public SendCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
        ).Execute();

        var q = new Mock<Queue<ICommand>>();
        IoC.Resolve<ICommand>("IoC.Register", "Game.Queue",
            (object[] args) => q.Object).Execute();

        receiver = new Mock<ICommandReceiver>();
        IoC.Resolve<ICommand>("IoC.Register", "Game.CommandsReceiver",
            (object[] args) => receiver.Object).Execute();
    }
    [Fact]
    public void SendCommand_Should_Send_A_Command_To_The_Command_Receiver()
    {
        var cmd = new Mock<ICommand>();
        var sendCommand = new SendCommand(cmd.Object);

        sendCommand.Execute();

        receiver.Verify(x => x.Receive(cmd.Object));
    }
    [Fact]
    public void SendCommand_Should_Throw_When_Receiver_Fails()
    {
        var cmd = new Mock<ICommand>();
        receiver.Setup(r => r.Receive(cmd.Object)).Throws<Exception>();

        var sendCommand = new SendCommand(cmd.Object);

        Assert.Throws<Exception>(() => sendCommand.Execute());
    }
}

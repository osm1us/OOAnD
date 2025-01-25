using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class SendCommandTests
{
    [Fact]
    public void SendCommand_Should_Send_Command_To_Receiver()
    {
        var cmd = new Mock<ICommand>();
        var receiver = new Mock<ICommandReceiver>();
        var sendCommand = new SendCommand(cmd.Object, receiver.Object);

        sendCommand.Execute();

        receiver.Verify(r => r.Receive(cmd.Object), Times.Once());
    }

    [Fact]
    public void SendCommand_Should_Throw_When_Receiver_Fails()
    {
        var cmd = new Mock<ICommand>();
        var receiver = new Mock<ICommandReceiver>();
        receiver.Setup(r => r.Receive(cmd.Object)).Throws<Exception>();
        var sendCommand = new SendCommand(cmd.Object, receiver.Object);

        Assert.Throws<Exception>(() => sendCommand.Execute());
    }
}

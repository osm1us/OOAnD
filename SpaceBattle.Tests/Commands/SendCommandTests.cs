using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class SendCommandTests
{
    [Fact]
    public void SendCommand_Should_Send_A_Command_To_The_Command_Receiver()
    {
        var receiver = new Mock<ICommandReceiver>();
        var cmd = new Mock<ICommand>();
        var sendCommand = new SendCommand(cmd.Object, receiver.Object);

        sendCommand.Execute();

        receiver.Verify(x => x.Receive(cmd.Object));
    }
}

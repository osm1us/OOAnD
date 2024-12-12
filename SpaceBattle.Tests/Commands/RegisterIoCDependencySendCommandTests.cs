using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencySendCommandTests
    {
        public RegisterIoCDependencySendCommandTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
        }

        [Fact]
        public void SendCommand_Should_Send_A_Command_To_The_Command_Receiver()
        {
            var commandReceiverMock = new Mock<ICommandReceiver>();

            var command = new RegisterIoCDependencySendCommand();
            command.Execute();

            var sendCommand = IoC.Resolve<ICommand>("Commands.Send", gameObject);
            sendCommand.Execute();

            commandReceiveMock.Verify(r => r.Receive(cmd), Times.Once());
        }
    }
}
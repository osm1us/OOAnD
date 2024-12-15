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
            IoC.Resolve<ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", null)).Execute();
        }

        [Fact]
        public void SendCommand_Should_Send_A_Command_To_The_Command_Receiver()
        {
            var commandReceiverMock = new Mock<ICommandReceiver>();
            var cmdMock = new Mock<ICommand>();
            var args = new object[] { cmdMock.Object, commandReceiverMock.Object };

            var command = new RegisterIoCDependencySendCommand();
            command.Execute();

            var sendCommand = IoC.Resolve<ICommand>("Commands.Send", args);
            sendCommand.Execute();

            commandReceiverMock.Verify(r => r.Receive(cmdMock.Object), Times.Once());
        }
    }
}
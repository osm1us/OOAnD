using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyMacroMoveRotateTests
    {
        public RegisterIoCDependencyMacroMoveRotateTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
        }

        [Fact]
        public void MacroMoveRotate_Dependencies_Should_Be_Registered()
        {
            // Arrange
            var mockCommand1 = new Mock<ICommand>();
            var mockCommand2 = new Mock<ICommand>();

            IoC.Resolve<ICommand>("IoC.Register", "Specs.Move", 
                (object[] args) => new string[] { "Command1", "Command2" }).Execute();

            IoC.Resolve<ICommand>("IoC.Register", "Command1", 
                (object[] args) => mockCommand1.Object).Execute();

            IoC.Resolve<ICommand>("IoC.Register", "Command2", 
                (object[] args) => mockCommand2.Object).Execute();

            IoC.Resolve<ICommand>("IoC.Register", "Specs.Rotate", 
                (object[] args) => new string[] { "Command1", "Command2" }).Execute();

            new RegisterIoCDependencyMacroCommand().Execute();
            var command = new RegisterIoCDependencyMacroMoveRotate();

            // Act
            command.Execute();

            // Assert
            var moveCommand = IoC.Resolve<ICommand>("Macro.Move", new object[] { });
            var rotateCommand = IoC.Resolve<ICommand>("Macro.Rotate", new object[] { });

            moveCommand.Execute();
            rotateCommand.Execute();

            mockCommand1.Verify(c => c.Execute(), Times.Exactly(2));
            mockCommand2.Verify(c => c.Execute(), Times.Exactly(2));
        }
    }
}

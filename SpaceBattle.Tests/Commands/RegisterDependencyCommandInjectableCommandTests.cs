using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests;

public class RegisterDependencyCommandInjectableCommandTests
{
    public RegisterDependencyCommandInjectableCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
    }

    [Fact]
    public void RegisterDependencyCommandInjectableCommand_Success()
    {
        // Arrange
        var command = new RegisterDependencyCommandInjectableCommand();

        // Act
        command.Execute();

        // Assert
        var cmd1 = IoC.Resolve<ICommand>("Commands.CommandInjectable");
        var cmd2 = IoC.Resolve<ICommandInjectable>("Commands.CommandInjectable");
        var cmd3 = IoC.Resolve<InjectableCommand>("Commands.CommandInjectable");
        // Проверяем функциональность
        var mockCommand = new Mock<ICommand>();
        ((ICommandInjectable)cmd1).Inject(mockCommand.Object);
        cmd1.Execute();
        mockCommand.Verify(c => c.Execute(), Times.Once());
    }
}

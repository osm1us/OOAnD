using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyMacroCommandTests
    {
        public RegisterIoCDependencyMacroCommandTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
        }

        [Fact]
        public void Execute_Should_Register_Macro_Command_Dependency()
        {        
            var mock1 = new Mock<ICommand>();
            var mock2 = new Mock<ICommand>();

            var command = new RegisterIoCDependencyMacroCommand();
            command.Execute();

            var macroCommand = IoC.Resolve<ICommand>("Commands.Macro", new ICommand[] { mock1.Object, mock2.Object });
            macroCommand.Execute();

            mock1.Verify(m => m.Execute(), Times.Once());
            mock2.Verify(m => m.Execute(), Times.Once());
        }
    }
}
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyMacroCommandTests
    {
        public RegisterIoCDependencyMacroCommandTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
            IoC.Resolve<ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        }

        [Fact]
        public void Execute_Should_Register_Macro_Command_Dependency()
        {
            var cmd1 = new Mock<ICommand>();
            var cmd2 = new Mock<ICommand>();

            var cmd = new RegisterIoCDependencyMacroCommand();
            cmd.Execute();

            var macroCommand = IoC.Resolve<ICommand>("Commands.Macro", new ICommand[] { cmd1.Object, cmd2.Object });
            macroCommand.Execute();

            cmd1.Verify(m => m.Execute(), Times.Once());
            cmd2.Verify(m => m.Execute(), Times.Once());
        }
    }
}

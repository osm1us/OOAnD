using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests
{
    public class CreateMacroCommandStrategyTests
    {
        public CreateMacroCommandStrategyTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
            IoC.Resolve<ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", null)).Execute();
        }

        [Fact]
        public void SuccessfulMacroCommandCreation()
        {
            var cmd1 = new Mock<ICommand>();
            var cmd2 = new Mock<ICommand>();
            cmd1.Setup(c => c.Execute());
            cmd2.Setup(c => c.Execute());

            var command = new RegisterIoCDependencyMacroCommand();
            command.Execute();

            var macroCommand = IoC.Resolve<ICommand>("Commands.Macro", new ICommand[] { cmd1.Object, cmd2.Object });
            macroCommand.Execute();

            cmd1.Verify(c => c.Execute(), Times.Once());
            cmd2.Verify(c => c.Execute(), Times.Once());
        }

        [Fact]
        public void MacroCommandCreationFailsWhenCommandExecutionFails()
        {
            var cmd1 = new Mock<ICommand>();
            var cmd2 = new Mock<ICommand>();
            cmd2.Setup(c => c.Execute()).Throws<Exception>();

            var command = new RegisterIoCDependencyMacroCommand();
            command.Execute();

            var macroCommand = IoC.Resolve<ICommand>("Commands.Macro", new ICommand[] { cmd1.Object, cmd2.Object });
            Assert.Throws<Exception>(() => macroCommand.Execute());
        }
    }
}

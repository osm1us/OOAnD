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
            IoC.Resolve<ICommand>("Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
            ).Execute();
        }

        [Fact]
        public void Resolve_Should_Create_MacroCommand_When_Dependency_Exists()
        {
            var cmd1 = new Mock<ICommand>();
            var cmd2 = new Mock<ICommand>();

            IoC.Resolve<ICommand>("IoC.Register", "Specs.Test", (object[] args) => new string[] { "Command1", "Command2" }).Execute();

            IoC.Resolve<ICommand>("IoC.Register", "Command1", (object[] args) => cmd1.Object).Execute();

            IoC.Resolve<ICommand>("IoC.Register", "Command2", (object[] args) => cmd2.Object).Execute();

            var regMacroCommand = new RegisterIoCDependencyMacroCommand();
            regMacroCommand.Execute();

            var strategy = new CreateMacroCommandStrategy("Test");
            strategy.Resolve(new object[] { }).Execute();

            cmd1.Verify(m => m.Execute(), Times.Once());
            cmd2.Verify(m => m.Execute(), Times.Once());
        }

        [Fact]
        public void Resolve_Should_Throw_When_Command_Dependency_Not_Exists()
        {
            IoC.Resolve<ICommand>("IoC.Register", "Specs.Test", (object[] args) => new string[] { "Rofls" }).Execute();

            new RegisterIoCDependencyMacroCommand().Execute();

            var strategy = new CreateMacroCommandStrategy("Test");
            var args = new object[] { };

            Assert.Throws<ArgumentException>(() => strategy.Resolve(args));
        }
    }
}

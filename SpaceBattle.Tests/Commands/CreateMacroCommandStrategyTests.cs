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
        }

        [Fact]
        public void Resolve_Should_Create_MacroCommand_When_Dependency_Exists()
        {
            var mockCommand1 = new Mock<ICommand>();
            var mockCommand2 = new Mock<ICommand>();

            var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
            IoC.Resolve<ICommand>("Scopes.Current.Set", scope).Execute();

            IoC.Resolve<ICommand>("IoC.Register", "Specs.Test", (Func<object[], object>)(_ =>
                new string[] { "Command1", "Command2" })).Execute();

            IoC.Resolve<ICommand>("IoC.Register", "Command1", (Func<object[], object>)(_ =>
                mockCommand1.Object)).Execute();

            IoC.Resolve<ICommand>("IoC.Register", "Command2", (Func<object[], object>)(_ =>
                mockCommand2.Object)).Execute();

            var registerMacroCommand = new RegisterIoCDependencyMacroCommand();
            registerMacroCommand.Execute();

            var strategy = new CreateMacroCommandStrategy("Test");
            var args = new object[] { };

            var macroCommand = strategy.Resolve(args);
            macroCommand.Execute();

            mockCommand1.Verify(m => m.Execute(), Times.Once());
            mockCommand2.Verify(m => m.Execute(), Times.Once());
        }

        [Fact]
        public void Resolve_Should_Throw_When_Specs_Dependency_Not_Exists()
        {
            var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
            IoC.Resolve<ICommand>("Scopes.Current.Set", scope).Execute();

            var strategy = new CreateMacroCommandStrategy("NonExistentSpec");
            var args = new object[] { };

            Assert.Throws<ArgumentException>(() => strategy.Resolve(args));
        }

        [Fact]
        public void Resolve_Should_Throw_When_Command_Dependency_Not_Exists()
        {
            var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
            IoC.Resolve<ICommand>("Scopes.Current.Set", scope).Execute();

            IoC.Resolve<ICommand>("IoC.Register", "Specs.Test", (Func<object[], object>)(_ =>
                new string[] { "NonExistentCommand" })).Execute();

            var registerMacroCommand = new RegisterIoCDependencyMacroCommand();
            registerMacroCommand.Execute();

            var strategy = new CreateMacroCommandStrategy("Test");
            var args = new object[] { };

            Assert.Throws<ArgumentException>(() => strategy.Resolve(args));
        }
    }
}

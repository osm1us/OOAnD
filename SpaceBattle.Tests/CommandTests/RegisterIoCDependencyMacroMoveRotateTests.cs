using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyMacroMoveRotateTests
    {
        public RegisterIoCDependencyMacroMoveRotateTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
            IoC.Resolve<ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        }

        [Fact]
        public void MacroMoveRotate_Dependencies_Should_Be_Registered()
        {
            var cmd1 = new Mock<ICommand>();
            var cmd2 = new Mock<ICommand>();

            IoC.Resolve<ICommand>("IoC.Register", "Specs.Move",
                (object[] args) => new string[] { "Command1", "Command2" }).Execute();

            IoC.Resolve<ICommand>("IoC.Register", "Command1",
                (object[] args) => cmd1.Object).Execute();

            IoC.Resolve<ICommand>("IoC.Register", "Command2",
                (object[] args) => cmd2.Object).Execute();

            IoC.Resolve<ICommand>("IoC.Register", "Specs.Rotate",
                (object[] args) => new string[] { "Command1", "Command2" }).Execute();

            new RegisterIoCDependencyMacroCommand().Execute();
            new RegisterIoCDependencyMacroMoveRotate().Execute();
            IoC.Resolve<ICommand>("Macro.Move").Execute();

            cmd1.Verify(c => c.Execute());
            cmd2.Verify(c => c.Execute());
        }
    }
}

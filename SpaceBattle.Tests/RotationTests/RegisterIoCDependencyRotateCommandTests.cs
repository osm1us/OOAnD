using Hwdtech.Ioc;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyRotateCommandTests
    {
        public RegisterIoCDependencyRotateCommandTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
            IoC.Resolve<ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        }

        [Fact]
        public void Execute_Should_Register_Rotate_Command_Dependency()
        {
            var gameObject = new Dictionary<string, object>
            {
                { "AnglePos", new Angle(90, 8) },
                { "RotateVelocity", new Angle(45, 8) }
            };

            var cmd = new RegisterIoCDependencyRotateCommand();
            cmd.Execute();

            var rotateCommand = IoC.Resolve<ICommand>("Commands.Rotate", gameObject);
            rotateCommand.Execute();

            Assert.Equal(new Angle(135, 8), (Angle)gameObject["AnglePos"]);
        }
    }
}

using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyRotateCommandTests
    {
        public RegisterIoCDependencyRotateCommandTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
        }

        [Fact]
        public void Execute_Should_Register_Rotate_Command_Dependency()
        {        
           var gameObject = new Dictionary<string, object>
            {
                { "AnglePos", new Angle(90) },
                { "RotateVelocity", new Angle(45) }
            };

            var command = new RegisterIoCDependencyRotateCommand();
            command.Execute();

            var rotateCommand = IoC.Resolve<ICommand>("Commands.Rotate", gameObject);
            rotateCommand.Execute();

            Assert.Equal(new Angle(135), (Angle)gameObject["AnglePos"]);
        }
    }
}

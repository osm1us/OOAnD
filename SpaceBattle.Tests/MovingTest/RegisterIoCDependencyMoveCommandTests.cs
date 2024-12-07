using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyMoveCommandTests
    {
        public RegisterIoCDependencyMoveCommandTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
        }

        [Fact]
        public void Execute_Should_Register_Move_Command_Dependency()
        {        
            var gameObject = new Dictionary<string, object>
            {
                { "Position", new Vector(1, 1) },
                { "Velocity", new Vector(2, 2) }
            };

            var command = new RegisterIoCDependencyMoveCommand();
            command.Execute();

            var moveCommand = IoC.Resolve<ICommand>("Commands.Move", gameObject);
            moveCommand.Execute();

            Assert.Equal(new Vector(3, 3), (Vector)gameObject["Position"]);
        }
    }
}

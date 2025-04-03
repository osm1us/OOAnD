using Hwdtech.Ioc;

namespace SpaceBattle.Lib.Tests
{
    public class AuthCommandTests
    {
        public AuthCommandTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
            IoC.Resolve<ICommand>("Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New",
                    IoC.Resolve<object>("Scopes.Root"))).Execute();
        }

        [Fact]
        public void AuthCommand_Successfully_Authorizes()
        {
            var subjectId = "player1";
            var action = "Move";
            var objectId = "ship1";

            IoC.Resolve<ICommand>("IoC.Register",
                "Authorization.Check",
                new Func<object[], object>((object[] args) => (object)true)).Execute();

            var authCommand = new AuthCommand(subjectId, action, objectId);

            authCommand.Execute();
        }

        [Fact]
        public void AuthCommand_Throws_When_Unauthorized()
        {
            var subjectId = "player1";
            var action = "Move";
            var objectId = "ship1";

            IoC.Resolve<ICommand>("IoC.Register",
                "Authorization.Check",
                new Func<object[], object>((object[] args) => (object)false)).Execute();

            var authCommand = new AuthCommand(subjectId, action, objectId);

            var exception = Assert.Throws<UnauthorizedAccessException>(() => authCommand.Execute());
            Assert.Equal("Игрок не имеет прав совершать действие над этим обьектом", exception.Message);
        }

        [Fact]
        public void AuthCommand_Throws_When_AuthCheck_Not_Registered()
        {
            var subjectId = "player1";
            var action = "Move";
            var objectId = "ship1";

            var authCommand = new AuthCommand(subjectId, action, objectId);

            Assert.Throws<ArgumentException>(() => authCommand.Execute());
        }

        [Fact]
        public void AuthCommand_Passes_Correct_Parameters_To_AuthCheck()
        {
            var subjectId = "player1";
            var action = "Move";
            var objectId = "ship1";

            string? capturedSubjectId = null;
            string? capturedAction = null;
            string? capturedObjectId = null;

            IoC.Resolve<ICommand>("IoC.Register",
                "Authorization.Check",
                new Func<object[], object>((object[] args) =>
                {
                    capturedSubjectId = (string)args[0];
                    capturedAction = (string)args[1];
                    capturedObjectId = (string)args[2];
                    return (object)true;
                })).Execute();

            var authCommand = new AuthCommand(subjectId, action, objectId);

            authCommand.Execute();

            Assert.Equal(subjectId, capturedSubjectId);
            Assert.Equal(action, capturedAction);
            Assert.Equal(objectId, capturedObjectId);
        }
    }
}

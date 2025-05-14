using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests
{
    public class GameTests
    {
        private readonly object _scope;
        private readonly Mock<ICommand> _cmd1;
        private readonly Mock<ICommand> _cmd2;
        private readonly Mock<ICommand> _exCmd;
        private readonly Mock<ICommand> _exHandler;
        private readonly Queue<ICommand> _q;

        public GameTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
            _scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
            IoC.Resolve<ICommand>("Scopes.Current.Set", _scope).Execute();

            _cmd1 = new Mock<ICommand>();
            _cmd2 = new Mock<ICommand>();
            _exCmd = new Mock<ICommand>();
            _exCmd.Setup(x => x.Execute()).Throws<Exception>();
            _exHandler = new Mock<ICommand>();

            _q = new Queue<ICommand>();

            IoC.Resolve<ICommand>("IoC.Register", "Game.Queue.Take", (object[] args) => _q.Dequeue()).Execute();
            IoC.Resolve<ICommand>("IoC.Register", "ExceptionHandler", (object[] args) => _exHandler.Object).Execute();
            IoC.Resolve<ICommand>("IoC.Register", "Game.Queue.Count", (object[] args) => (object)(() => _q.Count)).Execute();
        }

        [Fact]
        public void AllCommandsInGameQueueAreExecuted()
        {
            _q.Enqueue(_cmd1.Object);
            _q.Enqueue(_cmd2.Object);

            IoC.Resolve<ICommand>("IoC.Register", "Command.Time", (object[] args) => (object)TimeSpan.FromMilliseconds(400)).Execute();

            new Game(_scope).Execute();

            _cmd1.Verify(x => x.Execute(), Times.Once());
            _cmd2.Verify(x => x.Execute(), Times.Once());
        }

        [Fact]
        public void NoCommandsAreExecutedWhenTimeIsUp()
        {
            _q.Enqueue(_cmd1.Object);

            IoC.Resolve<ICommand>("IoC.Register", "Command.Time", (object[] args) => (object)TimeSpan.FromMilliseconds(-1)).Execute();

            new Game(_scope).Execute();

            _cmd1.Verify(x => x.Execute(), Times.Never());
        }

        [Fact]
        public void ExceptionHandlerIsExecutedWhenCommandThrows()
        {
            _q.Enqueue(_exCmd.Object);

            IoC.Resolve<ICommand>("IoC.Register", "Command.Time", (object[] args) => (object)TimeSpan.FromMilliseconds(400)).Execute();

            new Game(_scope).Execute();

            _exHandler.Verify(x => x.Execute(), Times.Once());
        }
    }
}


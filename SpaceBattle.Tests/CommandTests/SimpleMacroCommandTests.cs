using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests
{
    public class SimpleMacroCommandTests
    {
        [Fact]
        public void Execute_Should_Stop_On_Exception()
        {
            var cmd1 = new Mock<ICommand>();
            var cmd2 = new Mock<ICommand>();
            var cmd3 = new Mock<ICommand>();

            cmd2.Setup(m => m.Execute()).Throws<Exception>();

            var macroCommand = new SimpleMacroCommand(cmd1.Object, cmd2.Object, cmd3.Object);

            Assert.Throws<Exception>(() => macroCommand.Execute());
            cmd1.Verify(m => m.Execute(), Times.Once());
            cmd2.Verify(m => m.Execute(), Times.Once());
            cmd3.Verify(m => m.Execute(), Times.Never());
        }
    }
}

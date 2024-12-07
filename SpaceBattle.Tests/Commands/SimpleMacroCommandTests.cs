using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests
{
    public class SimpleMacroCommandTests
    {
        [Fact]
        public void Execute_Should_Stop_On_Exception()
        {
            var mock1 = new Mock<ICommand>();
            var mock2 = new Mock<ICommand>();
            var mock3 = new Mock<ICommand>();

            mock2.Setup(m => m.Execute()).Throws<System.Exception>();

            var simpleMacroCommand = new SimpleMacroCommand(mock1.Object, mock2.Object, mock3.Object);

            Assert.Throws<System.Exception>(() => simpleMacroCommand.Execute());
            mock1.Verify(m => m.Execute(), Times.Once());
            mock2.Verify(m => m.Execute(), Times.Once());
            mock3.Verify(m => m.Execute(), Times.Never());
        }
    }
}

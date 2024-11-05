using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests
{
    public class MoveCommandTests
    {
        [Fact]
        public void UpdatesPositionCorrectlyTest()
        {
            var Moving = new Mock<IMoving>();
            Moving.SetupGet(m => m.Position).Returns(new Vector(12, 5));
            Moving.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));
            var command = new MoveCommand(Moving.Object);
            command.Execute();
            Moving.VerifySet(x => x.Position = It.Is<Vector>(x => x.EqualsCoordinates(new Vector(5, 8))));
        }

        [Fact]
        public void CannotReadPositionTest()
        {
            var Moving = new Mock<IMoving>();
            Moving.SetupGet(m => m.Position).Throws<Exception>();
            Moving.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));
            var command = new MoveCommand(Moving.Object);
            Assert.Throws<Exception>(() => command.Execute());
        }

        [Fact]
        public void CannotReadVelocityTest()
        {
            var Moving = new Mock<IMoving>();
            Moving.SetupGet(m => m.Position).Returns(new Vector(12, 5));
            Moving.SetupGet(m => m.Velocity).Throws<Exception>();
            var command = new MoveCommand(Moving.Object);
            Assert.Throws<Exception>(() => command.Execute());
        }

        [Fact]
        public void CannotSetPositionTest()
        {
            var Moving = new Mock<IMoving>();
            Moving.SetupGet(m => m.Position).Returns(new Vector(12, 5));
            Moving.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));
            Moving.SetupSet(m => m.Position = It.IsAny<Vector>()).Throws<Exception>();
            var command = new MoveCommand(Moving.Object);
            Assert.Throws<Exception>(() => command.Execute());
        }
    }
}

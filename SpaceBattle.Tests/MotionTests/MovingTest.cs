using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests
{
    public class MoveCommandTests
    {
        [Fact]
        public void UpdatesPositionCorrectlyTest()
        {
            var movingObj = new Mock<IMoving>();
            movingObj.SetupGet(m => m.Position).Returns(new Vector(12, 5));
            movingObj.SetupGet(m => m.Velocity).Returns(new Vector(-4, 1));
            var cmd = new MoveCommand(movingObj.Object);
            cmd.Execute();
            movingObj.VerifySet(x => x.Position = It.Is<Vector>(x => x.Equals(new Vector(8, 6))));
        }

        [Fact]
        public void CannotReadPositionTest()
        {
            var movingObj = new Mock<IMoving>();
            movingObj.SetupGet(m => m.Position).Throws<Exception>();
            movingObj.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));
            var cmd = new MoveCommand(movingObj.Object);
            Assert.Throws<Exception>(() => cmd.Execute());
        }

        [Fact]
        public void CannotReadVelocityTest()
        {
            var movingObj = new Mock<IMoving>();
            movingObj.SetupGet(m => m.Position).Returns(new Vector(12, 5));
            movingObj.SetupGet(m => m.Velocity).Throws<Exception>();
            var cmd = new MoveCommand(movingObj.Object);
            Assert.Throws<Exception>(() => cmd.Execute());
        }

        [Fact]
        public void CannotSetPositionTest()
        {
            var movingObj = new Mock<IMoving>();
            movingObj.SetupGet(m => m.Position).Returns(new Vector(12, 5));
            movingObj.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));
            movingObj.SetupSet(m => m.Position = It.IsAny<Vector>()).Throws<Exception>();
            var cmd = new MoveCommand(movingObj.Object);
            Assert.Throws<Exception>(() => cmd.Execute());
        }
    }
}

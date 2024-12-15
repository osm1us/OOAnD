using Moq;
using SpaceBattle.Lib;
namespace SpaceBattle.Tests;

public class RotateCommandTestTest
{
    [Fact]
    public void Rotating45DegreesWithVelocity90Test()
    {
        var Rotating = new Mock<IRotating>();
        Rotating.SetupGet(x => x.AnglePos).Returns(new Angle(45, 8));
        Rotating.SetupGet(x => x.RotateVelocity).Returns(new Angle(90, 8));
        var command = new RotateCommand(Rotating.Object);
        command.Execute();
        Rotating.VerifySet(x => x.AnglePos = new Angle(135, 8));
    }

    [Fact]
    public void NoAnglePosTest()
    {
        var Rotating = new Mock<IRotating>();
        Rotating.SetupGet(x => x.AnglePos).Throws<Exception>();
        Rotating.SetupGet(x => x.RotateVelocity).Returns(new Angle(90, 8));
        var command = new RotateCommand(Rotating.Object);
        Assert.Throws<Exception>(() => command.Execute());
    }

    [Fact]
    public void NoRotateVelocityTest()
    {
        var Rotating = new Mock<IRotating>();
        Rotating.SetupGet(x => x.AnglePos).Returns(new Angle(90, 8));
        Rotating.SetupGet(x => x.RotateVelocity).Throws<Exception>();
        var command = new RotateCommand(Rotating.Object);
        Assert.Throws<Exception>(() => command.Execute());
    }

    [Fact]
    public void CantChangeAnglePosTest()
    {
        var Rotating = new Mock<IRotating>();
        Rotating.SetupGet(x => x.AnglePos).Returns(new Angle(0, 8));
        Rotating.SetupSet(x => x.AnglePos = It.IsAny<Angle>()).Throws<Exception>();
        Rotating.SetupGet(x => x.RotateVelocity).Returns(new Angle(848, 8));
        var command = new RotateCommand(Rotating.Object);
        Assert.Throws<Exception>(() => command.Execute());
    }
}

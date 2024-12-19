using Moq;
using SpaceBattle.Lib;
namespace SpaceBattle.Tests;

public class RotateCommandTestTest
{
    [Fact]
    public void Rotating45DegreesWithVelocity90Test()
    {
        var rotatingObj = new Mock<IRotating>();
        rotatingObj.SetupGet(x => x.AnglePos).Returns(new Angle(45, 8));
        rotatingObj.SetupGet(x => x.RotateVelocity).Returns(new Angle(90, 8));
        var cmd = new RotateCommand(rotatingObj.Object);
        cmd.Execute();
        rotatingObj.VerifySet(x => x.AnglePos = new Angle(135, 8));
    }

    [Fact]
    public void NoAnglePosTest()
    {
        var rotatingObj = new Mock<IRotating>();
        rotatingObj.SetupGet(x => x.AnglePos).Throws<Exception>();
        rotatingObj.SetupGet(x => x.RotateVelocity).Returns(new Angle(90, 8));
        var cmd = new RotateCommand(rotatingObj.Object);
        Assert.Throws<Exception>(() => cmd.Execute());
    }

    [Fact]
    public void NoRotateVelocityTest()
    {
        var rotatingObj = new Mock<IRotating>();
        rotatingObj.SetupGet(x => x.AnglePos).Returns(new Angle(90, 8));
        rotatingObj.SetupGet(x => x.RotateVelocity).Throws<Exception>();
        var cmd = new RotateCommand(rotatingObj.Object);
        Assert.Throws<Exception>(() => cmd.Execute());
    }

    [Fact]
    public void CantChangeAnglePosTest()
    {
        var rotatingObj = new Mock<IRotating>();
        rotatingObj.SetupGet(x => x.AnglePos).Returns(new Angle(0, 8));
        rotatingObj.SetupSet(x => x.AnglePos = It.IsAny<Angle>()).Throws<Exception>();
        rotatingObj.SetupGet(x => x.RotateVelocity).Returns(new Angle(848, 8));
        var cmd = new RotateCommand(rotatingObj.Object);
        Assert.Throws<Exception>(() => cmd.Execute());
    }
}

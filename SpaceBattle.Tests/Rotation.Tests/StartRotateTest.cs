using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class StartRotateTest
{
    [Fact]
    public void StartRotateSuccessTest()
    {
        var gameObject = new Dictionary<string, object>
        {
            ["AnglePos"] = new Angle(45, 8),
            ["RotateVelocity"] = new Angle(90, 8)
        };
        var startRotate = new StartRotateCommand(gameObject);
        startRotate.Execute();
        Assert.True(gameObject.ContainsKey("repeatableRotate"));
    }

    [Fact]
    public void RotationIntegrationTest()
    {
        var gameObject = new Dictionary<string, object>
        {
            ["AnglePos"] = new Angle(0, 8),
            ["RotateVelocity"] = new Angle(10, 8)
        };
        var startRotate = new StartRotateCommand(gameObject);

        startRotate.Execute();
        var cmd = (ICommand)gameObject["repeatableRotate"];

        cmd.Execute();
        Assert.Equal(gameObject["AnglePos"], new Angle(10, 8));
    }
}
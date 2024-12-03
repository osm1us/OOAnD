using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class StartMoveTest
{
    [Fact]
    public void StartMoveSuccessTest()
    {
        var gameObject = new Dictionary<string, object>
        {
            ["Position"] = new Vector(12, 5),
            ["Velocity"] = new Vector(-7, 3)
        };
        var startMove = new StartMoveCommand(gameObject);
        startMove.Execute();
        Assert.True(gameObject.ContainsKey("repeatableMove"));
    }

    [Fact]
    public void RotationIntegrationTest()
    {
        var gameObject = new Dictionary<string, object>
        {
            ["Position"] = new Vector(12, 5),
            ["Velocity"] = new Vector(-7, 3)
        };
        var startMove = new StartMoveCommand(gameObject);

        startMove.Execute();
        var cmd = (ICommand)gameObject["repeatableMove"];

        cmd.Execute();
        Assert.Equal(gameObject["Position"], new Vector(5, 8));
    }
}

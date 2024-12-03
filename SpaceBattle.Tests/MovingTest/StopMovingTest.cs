using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class StopMvoingTest
{
    [Fact]
    public void StopMovingSuccessTest()
    {
        var gameObject = new Dictionary<string, object>();
        var injectable = new Mock<Injectable>();
        gameObject["repeatableMove"] = injectable.Object;
        var stopMoving = new StopMoveCommand(gameObject);

        stopMoving.Execute();

        injectable.Verify(x => x.Inject(It.IsAny<EmptyCommand>()));
    }

    [Fact]
    public void StopMovingWithoutStartTest()
    {
        var gameObject = new Dictionary<string, object>();
        var stopMoving = new StopMoveCommand(gameObject);

        Assert.Throws<InvalidOperationException>(() => stopMoving.Execute());
    }
}

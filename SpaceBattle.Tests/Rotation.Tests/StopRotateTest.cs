using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class StopRotateTest
{
    [Fact]
    public void StopRotateSuccessTest()
    {
        var gameObject = new Dictionary<string, object>();
        var injectable = new Mock<ICommandInjectable>();
        gameObject["repeatableRotate"] = injectable.Object;
        var stopRotate = new StopRotateCommand(gameObject);

        stopRotate.Execute();

        injectable.Verify(x => x.Inject(It.IsAny<EmptyCommand>()));
    }

    [Fact]
    public void StopRotateWithoutStartTest()
    {
        var gameObject = new Dictionary<string, object>();
        var stopRotate = new StopRotateCommand(gameObject);

        Assert.Throws<InvalidOperationException>(() => stopRotate.Execute());
    }
}

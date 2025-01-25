using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class InjectableCommandTest
{
    [Fact]
    public void InjectableCommand_ThrowsException_WhenNotInjected()
    {
        var injectable = new InjectableCommand();

        Assert.Throws<InvalidOperationException>(() => injectable.Execute());
    }

    [Fact]
    public void InjectableCommand_ExecutesInjectedCommand()
    {
        var injectable = new InjectableCommand();
        var cmd = new Mock<ICommand>();

        injectable.Inject(cmd.Object);
        injectable.Execute();

        cmd.Verify(x => x.Execute(), Times.Once());
    }
}

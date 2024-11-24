using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class InjectableCommandTest
{
    [Fact]
    public void InjectableCommand_DefaultConstructorKaifuet()
    {
        var injectable = new InjectableCommand();
        Assert.Null(Record.Exception(() => injectable.Execute()));
    }

    [Fact]
    public void InjectableCommand_Inject_ChangesCommand()
    {
        var injectable = new InjectableCommand();
        var cmd = new Mock<ICommand>();
        injectable.Inject(cmd.Object);
        injectable.Execute();

        cmd.Verify(x => x.Execute());
    }
}

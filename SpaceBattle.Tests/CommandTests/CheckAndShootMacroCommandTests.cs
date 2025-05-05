using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class CheckAndShootMacroCommandTests
{
    [Fact]
    public void Execute_PerformsAllStepsInSequence()
    {
        var log = new List<string>();

        var auth = new Mock<ICommand>();
        auth.Setup(c => c.Execute()).Callback(() => log.Add("auth"));

        var check = new Mock<ICommand>();
        check.Setup(c => c.Execute()).Callback(() => log.Add("check"));

        var remove = new Mock<ICommand>();
        remove.Setup(c => c.Execute()).Callback(() => log.Add("remove"));

        var shot = new Mock<ICommand>();
        shot.Setup(c => c.Execute()).Callback(() => log.Add("shot"));

        var macro = new SimpleMacroCommand(new List<ICommand>
        {
            auth.Object, check.Object, remove.Object, shot.Object
        });

        macro.Execute();

        Assert.Equal(new[] { "auth", "check", "remove", "shot" }, log);
    }
}

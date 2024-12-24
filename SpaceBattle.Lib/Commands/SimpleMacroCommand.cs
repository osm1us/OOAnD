namespace SpaceBattle.Lib;
public class SimpleMacroCommand : ICommand
{
    private readonly IEnumerable<ICommand> cmds;
    public SimpleMacroCommand(IEnumerable<ICommand> cmds)
    {
        this.cmds = cmds;
    }
    public void Execute()
    {
        cmds.ToList().ForEach(c => c.Execute());
    }
}

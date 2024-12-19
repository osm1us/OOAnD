namespace SpaceBattle.Lib;
public class SimpleMacroCommand : ICommand
{
    public ICommand[] cmds;
    public SimpleMacroCommand(params ICommand[] cmds)
    {
        this.cmds = cmds;
    }
    public void Execute()
    {
        foreach (var cmd in cmds)
        {
            cmd.Execute();
        }
    }
}

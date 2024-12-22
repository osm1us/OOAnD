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
        cmds.All(c =>
        {
            c.Execute();
            return true;
        });
    }
}

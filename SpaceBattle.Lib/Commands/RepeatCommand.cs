namespace SpaceBattle.Lib;
public class RepeatCommand : ICommand
{
    private readonly ICommand _cmd = new EmptyCommand();
    private readonly Queue<ICommand> _q = new Queue<ICommand>();
    public RepeatCommand(ICommand cmd, Queue<ICommand> q)
    {
        _cmd = cmd;
        _q = q;
    }
    public void Execute()
    {
        _q.Enqueue(_cmd);
    }
}

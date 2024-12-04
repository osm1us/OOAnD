namespace SpaceBattle.Lib;
public class InjectableCommand : ICommand, Injectable
{
    private ICommand _cmd = new EmptyCommand();

    public void Execute()
    {
        _cmd.Execute();
    }
    public void Inject(ICommand cmd)
    {
        _cmd = cmd;
    }
}

namespace SpaceBattle.Lib;

public class InjectableCommand : ICommand, ICommandInjectable
{
    private ICommand? _cmd;

    public void Execute()
    {
        if (_cmd == null)
        {
            throw new InvalidOperationException("No command injected.");
        }

        _cmd.Execute();
    }

    public void Inject(ICommand cmd)
    {
        _cmd = cmd;
    }
}

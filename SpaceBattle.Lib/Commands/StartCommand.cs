namespace SpaceBattle.Lib;

public class StartCommand : ICommand
{
    private readonly IDictionary<string, object> _gameObject;
    private readonly Queue<ICommand> _q;
    private readonly string _cmdType;

    public StartCommand(IDictionary<string, object> gameObject, Queue<ICommand> q, string cmdType)
    {
        _gameObject = gameObject;
        _q = q;
        _cmdType = cmdType;
    }

    public void Execute()
    {
        var cmd = IoC.Resolve<ICommand>($"Commands.{_cmdType}", _gameObject);
        var injectable = (ICommand)IoC.Resolve<ICommandInjectable>("Commands.CommandInjectable");
        var repeat = new RepeatCommand(injectable, _q);
        var repeatable = IoC.Resolve<ICommand>($"Macro.{_cmdType}", cmd, repeat);

        ((ICommandInjectable)injectable).Inject(repeatable);
        _gameObject[$"repeatable{_cmdType}"] = injectable;
        _q.Enqueue(repeatable);
    }
}

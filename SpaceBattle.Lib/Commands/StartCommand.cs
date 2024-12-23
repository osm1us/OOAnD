namespace SpaceBattle.Lib;

public class StartCommand : ICommand
{
    private readonly IDictionary<string, object> _gameObject;
    private readonly string _cmdType;

    public StartCommand(IDictionary<string, object> gameObject, string cmdType)
    {
        _gameObject = gameObject;
        _cmdType = cmdType;
    }

    public void Execute()
    {
        var cmd = IoC.Resolve<ICommand>($"Commands.{_cmdType}", _gameObject);
        var q = IoC.Resolve<Queue<ICommand>>("Game.Queue");
        var injectable = (ICommand)IoC.Resolve<ICommandInjectable>("Commands.CommandInjectable");
        var repeat = new RepeatCommand(injectable, q);
        var repeatable = IoC.Resolve<ICommand>($"Macro.{_cmdType}", cmd, repeat);
        ((ICommandInjectable)injectable).Inject(repeatable);
        _gameObject[$"repeatable{_cmdType}"] = injectable;

        var sendCommand = new SendCommand(repeatable);
        sendCommand.Execute();
    }
}

namespace SpaceBattle.Lib;

public class StartRotateCommand : ICommand
{
    private readonly Queue<ICommand> q = new Queue<ICommand>();
    private readonly IDictionary<string, object> _gameObject;

    public StartRotateCommand(IDictionary<string, object> gameObject)
    {
        _gameObject = gameObject;
    }

    public void Execute()
    {
        var rotateCommand = new RotateCommand(new RotatingAdapter(_gameObject));
        var injectable = new InjectableCommand();
        var repeat = new RepeatCommand(injectable, q);
        var repeatableRotate = new SimpleMacroCommand(rotateCommand, repeat);
        injectable.Inject(repeatableRotate);
        _gameObject["repeatableRotate"] = injectable;
        q.Enqueue(repeatableRotate);
    }
}

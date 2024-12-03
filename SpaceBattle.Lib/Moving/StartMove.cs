namespace SpaceBattle.Lib;

public class StartMoveCommand : ICommand
{
    private readonly Queue<ICommand> q = new Queue<ICommand>();
    private readonly IDictionary<string, object> _gameObject;

    public StartMoveCommand(IDictionary<string, object> gameObject)
    {
        _gameObject = gameObject;
    }

    public void Execute()
    {
        var moveCommand = new MoveCommand(new MovingAdapter(_gameObject));
        var injectable = new InjectableCommand();
        var repeat = new RepeatCommand(injectable, q);
        var repeatableMove = new SimpleMacroCommand(moveCommand, repeat);
        injectable.Inject(repeatableMove);
        _gameObject["repeatableMove"] = injectable;
        q.Enqueue(repeatableMove);
    }
}

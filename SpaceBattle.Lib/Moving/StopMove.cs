namespace SpaceBattle.Lib;

public class StopMoveCommand : ICommand
{
    private readonly IDictionary<string, object> _gameObject;

    public StopMoveCommand(IDictionary<string, object> gameObject)
    {
        _gameObject = gameObject;
    }

    public void Execute()
    {
        if (!_gameObject.ContainsKey("repeatableMove"))
        {
            throw new InvalidOperationException("Движение не был запущено");
        }

        var injectable = (Injectable)_gameObject["repeatableMove"];
        injectable.Inject(new EmptyCommand());
    }
}

namespace SpaceBattle.Lib;

public class StopRotateCommand : ICommand
{
    private readonly IDictionary<string, object> _gameObject;

    public StopRotateCommand(IDictionary<string, object> gameObject)
    {
        _gameObject = gameObject;
    }

    public void Execute()
    {
        if (!_gameObject.ContainsKey("repeatableRotate"))
        {
            throw new InvalidOperationException("Поворот не был запущен");
        }

        var injectable = (ICommandInjectable)_gameObject["repeatableRotate"];
        injectable.Inject(new EmptyCommand());
    }
}

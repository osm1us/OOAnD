namespace SpaceBattle.Lib;

public class GameItemAddCommand : ICommand
{
    private readonly Dictionary<string, Dictionary<string, object>> _gameItems;
    private readonly Dictionary<string, object> _gameObject;

    public GameItemAddCommand(Dictionary<string, Dictionary<string, object>> gameItems, Dictionary<string, object> gameObject)
    {
        _gameItems = gameItems;
        _gameObject = gameObject;
    }

    public void Execute()
    {
        if (!_gameObject.ContainsKey("Id"))
        {
            _gameObject["Id"] = Guid.NewGuid().ToString();
        }

        var key = (string)_gameObject["Id"];
        if (_gameItems.ContainsKey(key))
        {
            throw new Exception($"GameItem с ключом '{key}' уже существует.");
        }

        _gameItems[key] = _gameObject;
    }
}

namespace SpaceBattle.Lib;

public class GameItemRemoveCommand : ICommand
{
    private readonly Dictionary<string, Dictionary<string, object>> _gameItems;
    private readonly string _key;

    public GameItemRemoveCommand(Dictionary<string, Dictionary<string, object>> gameItems, string key)
    {
        _gameItems = gameItems;
        _key = key;
    }

    public void Execute()
    {
        if (!_gameItems.ContainsKey(_key))
        {
            throw new Exception($"GameItem с ключом '{_key}' не найден для удаления.");
        }

        _gameItems.Remove(_key);
    }
}

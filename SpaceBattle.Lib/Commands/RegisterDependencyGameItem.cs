namespace SpaceBattle.Lib;

public class RegisterDependencyGameItem : ICommand
{
    public void Execute()
    {
        var gameItems = new Dictionary<string, Dictionary<string, object>>();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "GameItem.Add",
            (object[] args) =>
            {
                var gameObject = (Dictionary<string, object>)args[0];
                return new GameItemAddCommand(gameItems, gameObject);
            }
        ).Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "GameItem.Remove",
            (object[] args) =>
            {
                var key = (string)args[0];
                return new GameItemRemoveCommand(gameItems, key);
            }
        ).Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "GameItem.Get",
            (object[] args) =>
            {
                var key = (string)args[0];
                if (!gameItems.TryGetValue(key, out var result))
                {
                    throw new Exception($"GameItem с ключом '{key}' не найден.");
                }

                return result;
            }
        ).Execute();
    }
}

namespace SpaceBattle.Tests;

using SpaceBattle.Lib;

public class GameItemRemoveCommandTests
{
    [Fact]
    public void Execute_ShouldRemoveGameItem_WhenGameItemExists()
    {
        var gameItems = new Dictionary<string, Dictionary<string, object>>();
        var id = "id";
        var gameObject = new Dictionary<string, object>
        {
            { "Id", id }
        };
        gameItems[id] = gameObject;
        var command = new GameItemRemoveCommand(gameItems, id);
        command.Execute();
        Assert.False(gameItems.ContainsKey(id));
    }

    [Fact]
    public void Execute_ShouldThrowException_WhenGameItemNotFoundForRemoval()
    {
        var repository = new Dictionary<string, Dictionary<string, object>>();
        var nonExistentId = "id";
        var command = new GameItemRemoveCommand(repository, nonExistentId);
        var ex = Assert.Throws<Exception>(() => command.Execute());
    }
}

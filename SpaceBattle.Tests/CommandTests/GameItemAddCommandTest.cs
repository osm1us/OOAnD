namespace SpaceBattle.Tests;

using SpaceBattle.Lib;

public class GameItemAddCommandTests
{
    [Fact]
    public void Execute_AssignsId_WhenNotPresent_AndAddsGameItem()
    {
        var gameItems = new Dictionary<string, Dictionary<string, object>>();
        var gameObject = new Dictionary<string, object>();
        var command = new GameItemAddCommand(gameItems, gameObject);
        command.Execute();
        Assert.True(gameObject.ContainsKey("Id"));
        var id = (string)gameObject["Id"];
        Assert.True(gameItems.ContainsKey(id));
        Assert.Same(gameObject, gameItems[id]);
    }

    [Fact]
    public void Execute_AddsGameItem_WhenIdPresentAndNotInRepository()
    {
        var repository = new Dictionary<string, Dictionary<string, object>>();
        var presetId = "id";
        var gameObject = new Dictionary<string, object>
        {
            { "Id", presetId }
        };
        var command = new GameItemAddCommand(repository, gameObject);
        command.Execute();
        Assert.True(repository.ContainsKey(presetId));
        Assert.Same(gameObject, repository[presetId]);
    }

    [Fact]
    public void Execute_ThrowsException_WhenGameItemWithSameIdAlreadyExists()
    {
        var repository = new Dictionary<string, Dictionary<string, object>>();
        var duplicateId = "id";
        var gameObject = new Dictionary<string, object>
        {
            { "Id", duplicateId }
        };
        repository[duplicateId] = gameObject;
        var command = new GameItemAddCommand(repository, gameObject);
        var ex = Assert.Throws<Exception>(() => command.Execute());
    }
}

using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RemoveAmmoCommandTests
{
    [Fact]
    public void Execute_RemovesAmmo()
    {
        var storage = new AmmoStorage();
        storage.Add(5);
        var command = new RemoveAmmoCommand(storage, 2);
        command.Execute();
        Assert.True(storage.HasAmmo());
    }

    [Fact]
    public void Execute_ThrowsIfNotEnoughAmmo()
    {
        var storage = new AmmoStorage();
        var command = new RemoveAmmoCommand(storage, 1);
        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }
}

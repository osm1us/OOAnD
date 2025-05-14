using Moq;

namespace SpaceBattle.Tests;

public class CheckAmmoCommandTests
{
    [Fact]
    public void Execute_DoesNotThrow_WhenAmmoIsAvailable()
    {
        var ammo = new Mock<IRemoveAmmo>();
        ammo.Setup(a => a.HasAmmo()).Returns(true);

        var command = new CheckAmmoCommand(ammo.Object);

        var ex = Record.Exception(() => command.Execute());

        Assert.Null(ex);
    }

    [Fact]
    public void Execute_Throws_WhenAmmoIsNotAvailable()
    {
        var ammo = new Mock<IRemoveAmmo>();
        ammo.Setup(a => a.HasAmmo()).Returns(false);

        var command = new CheckAmmoCommand(ammo.Object);

        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }
}

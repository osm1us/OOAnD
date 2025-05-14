using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterIoCDependencyCheckAndShootTests
{
    public RegisterIoCDependencyCheckAndShootTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void Execute_Should_Add_Ammo()
    {
        var mockAmmo = new Mock<IAddAmmo>();

        var amount = 5;
        var cmd = new AddAmmoCommand(mockAmmo.Object, amount);

        cmd.Execute();

        mockAmmo.Verify(m => m.Add(amount), Times.Once());
    }

    [Fact]
    public void RegisterIoCDependencyCheckAndShoot_Should_Register_All_Commands()
    {
        var addAmmo = new Mock<IAddAmmo>().Object;
        var removeAmmo = new Mock<IRemoveAmmo>().Object;

        var auth = new Mock<ICommand>();
        var check = new Mock<ICommand>();
        var remove = new Mock<ICommand>();
        var shot = new Mock<ICommand>();

        new RegisterIoCDependencyCheckAndShoot().Execute();

        var addAmmoCmd = IoC.Resolve<ICommand>("AddAmmo", addAmmo, 5);
        var removeAmmoCmd = IoC.Resolve<ICommand>("RemoveAmmo", removeAmmo, 3);
        var checkAmmoCmd = IoC.Resolve<ICommand>("CheckAmmo", removeAmmo);
        var checkAndShoot = IoC.Resolve<ICommand>("CheckAndShoot", auth.Object, check.Object, remove.Object, shot.Object);

        Assert.IsType<AddAmmoCommand>(addAmmoCmd);
        Assert.IsType<RemoveAmmoCommand>(removeAmmoCmd);
        Assert.IsType<CheckAmmoCommand>(checkAmmoCmd);
        Assert.IsType<SimpleMacroCommand>(checkAndShoot);

        checkAndShoot.Execute();
        auth.Verify(c => c.Execute(), Times.Once());
        check.Verify(c => c.Execute(), Times.Once());
        remove.Verify(c => c.Execute(), Times.Once());
        shot.Verify(c => c.Execute(), Times.Once());
    }
}

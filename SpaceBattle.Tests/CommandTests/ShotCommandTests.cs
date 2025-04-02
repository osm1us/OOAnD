using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Lib.Tests;

public class ShotCommandTests
{
    [Fact]
    public void Shot_Command_Successfully_Executes()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<ICommand>("Scopes.Current.Set", scope).Execute();

        var shootingObj = new Mock<IShooting>();
        var projectile = new Mock<IShooting>().Object;

        IoC.Resolve<ICommand>("IoC.Register", "Game.Projectile.Create",
            (object[] args) => projectile).Execute();

        var initProjectileCommand = new Mock<ICommand>();
        IoC.Resolve<ICommand>("IoC.Register", "Game.Commands.InitializeProjectile",
            (object[] args) => initProjectileCommand.Object).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Actions.Start",
            (object[] args) => new Mock<ICommand>().Object).Execute();

        var shotCommand = new ShotCommand(shootingObj.Object);

        shotCommand.Execute();

        initProjectileCommand.Verify(cmd => cmd.Execute(), Times.Once());
    }

    [Fact]
    public void Shot_Command_Throws_When_Projectile_Create_Not_Registered()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<ICommand>("Scopes.Current.Set", scope).Execute();

        var shootingObj = new Mock<IShooting>();
        var shotCommand = new ShotCommand(shootingObj.Object);

        Assert.Throws<ArgumentException>(() => shotCommand.Execute());
    }

    [Fact]
    public void Shot_Command_Throws_When_Initialize_Projectile_Not_Registered()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<ICommand>("Scopes.Current.Set", scope).Execute();

        var shootingObj = new Mock<IShooting>();
        var projectile = new Mock<IShooting>().Object;

        IoC.Resolve<ICommand>("IoC.Register", "Game.Projectile.Create",
            (object[] args) => projectile).Execute();

        var shotCommand = new ShotCommand(shootingObj.Object);

        Assert.Throws<ArgumentException>(() => shotCommand.Execute());
    }

    [Fact]
    public void Shot_Command_Throws_When_Start_Command_Not_Registered()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<ICommand>("Scopes.Current.Set", scope).Execute();

        var shootingObj = new Mock<IShooting>();
        var projectile = new Mock<IShooting>().Object;

        IoC.Resolve<ICommand>("IoC.Register", "Game.Projectile.Create",
            (object[] args) => projectile).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Game.Commands.InitializeProjectile",
            (object[] args) => new Mock<ICommand>().Object).Execute();

        var shotCommand = new ShotCommand(shootingObj.Object);

        Assert.Throws<ArgumentException>(() => shotCommand.Execute());
    }
}

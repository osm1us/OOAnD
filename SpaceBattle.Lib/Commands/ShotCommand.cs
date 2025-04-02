namespace SpaceBattle.Lib;

public class ShotCommand : ICommand
{
    private readonly IShooting _shootingObject;
    public ShotCommand(IShooting obj)
    {
        _shootingObject = obj;
    }
    public void Execute()
    {
        var projectile = IoC.Resolve<IShooting>("Game.Projectile.Create");
        IoC.Resolve<ICommand>("Game.Commands.InitializeProjectile", projectile, _shootingObject).Execute();
        IoC.Resolve<ICommand>("Actions.Start", projectile, "Move").Execute();
    }
}

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyShotCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Commands.Shot",
            (object[] args) =>
            {
                return new ShotCommand((IShooting)args[0]);
            }
        ).Execute();
    }
}

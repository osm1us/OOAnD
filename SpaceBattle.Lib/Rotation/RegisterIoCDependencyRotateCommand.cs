namespace SpaceBattle.Lib;

public class RegisterIoCDependencyRotateCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Commands.Rotate",
            (object[] args) => new RotateCommand(new RotatingAdapter((IDictionary<string, object>)args[0]))
        ).Execute();
    }
}

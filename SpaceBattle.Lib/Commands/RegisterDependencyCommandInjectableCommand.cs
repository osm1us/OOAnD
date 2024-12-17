namespace SpaceBattle.Lib;

using Hwdtech;

public class RegisterDependencyCommandInjectableCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Commands.CommandInjectable",
            (object[] args) => new InjectableCommand()
        ).Execute();
    }
}

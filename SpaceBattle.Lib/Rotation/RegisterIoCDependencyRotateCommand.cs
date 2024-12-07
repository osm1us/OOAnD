using Hwdtech;
using Hwdtech.Ioc;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyRotateCommand : ICommand
{
    public void Execute()
    {
        var registerDependencyCommand = IoC.Resolve<ICommand>(
            "IoC.Register",
            "Commands.Rotate",
            (object[] args) => new RotateCommand(new RotatingAdapter((IDictionary<string, object>)args[0]))
        );

        registerDependencyCommand.Execute();
    }
}

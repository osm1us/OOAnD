using Hwdtech;
using Hwdtech.Ioc;

namespace SpaceBattle.Lib;

public class InitScopeBasedIoCImplementationCommand : ICommand
{
    public void Execute()
    {
        new InitScopeBasedIoCImplementation().Execute();

        IoC.Resolve<ICommand>(
            "IoC.Register",
            "IoC.Register",
            (object[] args) => 
            {
                var scope = IoC.Resolve<object>("Scopes.Current");
                ((IScopeBasedIoCImplementation)scope).Register((string)args[0], (object)args[1]);
                return new EmptyCommand();
            }
        ).Execute();
    }
}

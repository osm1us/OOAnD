using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class RegisterDependencyCommandInjectableCommandTests
{
    public RegisterDependencyCommandInjectableCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set",
        IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void RegisterDependencyCommandInjectableCommand_Success()
    {
        new RegisterDependencyCommandInjectableCommand().Execute();

        var cmd1 = IoC.Resolve<ICommand>("Commands.CommandInjectable");
        var cmd2 = IoC.Resolve<ICommandInjectable>("Commands.CommandInjectable");
        var cmd3 = IoC.Resolve<InjectableCommand>("Commands.CommandInjectable");

        var cmd = new Mock<ICommand>();
        ((ICommandInjectable)cmd1).Inject(cmd.Object);
        cmd1.Execute();
        cmd.Verify(c => c.Execute(), Times.Once());
    }
}

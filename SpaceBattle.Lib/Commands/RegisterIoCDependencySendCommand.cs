public class RegisterIoCDependencySendCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
                "IoC.Register", 
                "Commands.Send",
                (object[] args) => (ICommand)new SendCommand((ICommand)x)).Execute();
    }
}
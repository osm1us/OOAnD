namespace SpaceBattle.Lib;

public class SendCommand(ICommand cmd) : ICommand
{
    public void Execute()
    {
        var receiver = IoC.Resolve<ICommandReceiver>("Game.CommandsReceiver");
        receiver.Receive(cmd);
    }
}

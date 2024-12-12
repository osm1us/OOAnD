using SpaceBattle.Lib;

class SendCommand(ICommand cmd, ICommandReceiver receiver) : ICommand
{
    public void Execute()
    {
    //var receiver = Ioc.Resolve<ICommandReceiver>("Game.CommandsReceiver");
    receiver.Receive(cmd);
    }
}
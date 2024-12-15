using SpaceBattle.Lib;

public interface ICommandReceiver
{
    void Receive(ICommand command);
}

public class SendCommand(ICommand cmd, ICommandReceiver receiver) : ICommand
{
    public void Execute()
    {
        receiver.Receive(cmd);
    }
}
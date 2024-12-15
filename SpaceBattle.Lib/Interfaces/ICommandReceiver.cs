namespace SpaceBattle.Lib;

public interface ICommandReceiver : ICommand
{
    void Receive(ICommand cmd);
}

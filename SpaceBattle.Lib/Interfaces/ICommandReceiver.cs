namespace SpaceBattle.Lib;

public interface ICommandReceiver : ICommand
{
    void Recieve(ICommand cmd);
}

namespace SpaceBattle.Lib;

public class QueueCommandReceiver : ICommandReceiver
{
    public void Receive(ICommand cmd)
    {
        var q = IoC.Resolve<Queue<ICommand>>("Game.Queue");
        q.Enqueue(cmd);
    }
}

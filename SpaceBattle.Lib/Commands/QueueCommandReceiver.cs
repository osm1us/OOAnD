namespace SpaceBattle.Lib;

public class QueueCommandReceiver : ICommandReceiver
{
    private readonly Queue<ICommand> _queue;

    public QueueCommandReceiver(Queue<ICommand> queue)
    {
        _queue = queue;
    }

    public void Receive(ICommand cmd)
    {
        _queue.Enqueue(cmd);
    }

    public void Execute() { }
}

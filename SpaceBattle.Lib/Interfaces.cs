public interface ICommand
{
    public void Execute();
}
public interface ISender
{
    void Add(ICommand cmd);
}
public interface IReceiver
{
    ICommand Take();
}

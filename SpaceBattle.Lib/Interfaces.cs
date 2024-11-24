namespace SpaceBattle.Lib;

public interface ICommand
{
    public void Execute();
}
public interface Injectable
{
    void Inject(ICommand cmd);
}

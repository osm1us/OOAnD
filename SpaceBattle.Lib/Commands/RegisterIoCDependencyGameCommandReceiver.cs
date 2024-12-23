namespace SpaceBattle.Lib;

public class RegisterIoCDependencyGameCommandReceiver : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>("IoC.Register", "Game.CommandsReceiver",
            (object[] args) => new QueueCommandReceiver()).Execute();
    }
}

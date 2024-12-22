namespace SpaceBattle.Lib
{
    public class RegisterIoCDependencyMacroCommand : ICommand
    {
        public void Execute()
        {
            IoC.Resolve<ICommand>(
                "IoC.Register",
                "Commands.Macro",
                (object[] args) => (ICommand)new SimpleMacroCommand(args.Select(x => (ICommand)x).ToArray())).Execute();
        }
    }
}

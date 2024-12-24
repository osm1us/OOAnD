﻿namespace SpaceBattle.Lib;
public class RegisterIoCDependencyMacroCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Commands.Macro",
            (object[] args) => new SimpleMacroCommand(args.OfType<ICommand>())).Execute();
    }
}

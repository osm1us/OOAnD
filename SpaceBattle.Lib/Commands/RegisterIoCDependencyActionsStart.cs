﻿namespace SpaceBattle.Lib;

public class RegisterIoCDependencyActionsStart : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ICommand>(
            "IoC.Register",
            "Actions.Start",
            (object[] args) =>
            {
                var gameObject = (IDictionary<string, object>)args[0];
                var cmdType = (string)args[1];

                return new StartCommand(gameObject, cmdType);
            }
        ).Execute();
    }
}

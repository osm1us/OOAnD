﻿namespace SpaceBattle.Lib;

public class StartCommand : ICommand
{
    private readonly IDictionary<string, object> _gameObject;
    private readonly string _cmdType;

    public StartCommand(IDictionary<string, object> gameObject, string cmdType)
    {
        _gameObject = gameObject;
        _cmdType = cmdType;
    }

    public void Execute()
    {
        var cmd = IoC.Resolve<ICommand>($"Commands.{_cmdType}", _gameObject);
        var injectable = (ICommand)IoC.Resolve<ICommandInjectable>("Commands.CommandInjectable");
        var receiver = IoC.Resolve<ICommandReceiver>("Game.CommandsReceiver");
        var send = new SendCommand(injectable, receiver);
        var repeatable = IoC.Resolve<ICommand>($"Macro.{_cmdType}", cmd, send);
        ((ICommandInjectable)injectable).Inject(repeatable);
        _gameObject[$"repeatable{_cmdType}"] = injectable;
        var sendCommand = new SendCommand(repeatable, receiver);
        sendCommand.Execute();
    }
}

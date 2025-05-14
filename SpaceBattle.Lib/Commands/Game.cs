using System.Diagnostics;

namespace SpaceBattle.Lib;

public class Game : ICommand
{
    private readonly object _scope;
    private readonly Stopwatch _stopwatch;

    public Game(object scope)
    {
        _scope = scope;
        _stopwatch = new Stopwatch();
    }

    public void Execute()
    {
        _stopwatch.Reset();
        IoC.Resolve<ICommand>("Scopes.Current.Set", _scope).Execute();
        var cmdsTime = IoC.Resolve<TimeSpan>("Command.Time");
        while (IoC.Resolve<Func<int>>("Game.Queue.Count")() > 0 && _stopwatch.Elapsed <= cmdsTime)
        {
            _stopwatch.Start();
            var cmd = IoC.Resolve<ICommand>("Game.Queue.Take");
            try
            {
                cmd.Execute();
            }
            catch (Exception exception)
            {
                IoC.Resolve<ICommand>("ExceptionHandler", exception, cmd).Execute();
            }

            _stopwatch.Stop();
        }
    }
}

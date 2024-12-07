using Hwdtech;
using System.Linq;

namespace SpaceBattle.Lib
{
    public class CreateMacroCommandStrategy
    {
        private readonly string commandSpec;

        public CreateMacroCommandStrategy(string commandSpec)
        {
            this.commandSpec = commandSpec;
        }

        public ICommand Resolve(object[] args)
        {
            var commandNames = IoC.Resolve<string[]>($"Specs.{commandSpec}");
            var commands = commandNames.Select(name => IoC.Resolve<ICommand>(name, args)).ToArray();
            return IoC.Resolve<ICommand>("Commands.Macro", commands.Cast<object>().ToArray());
        }
    }
}
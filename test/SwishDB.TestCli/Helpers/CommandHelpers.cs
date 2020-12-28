
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using CommandLine;
using SwishDB.TestCli.Commands;

namespace SwishDB.TestCli.Helpers
{
    public static class CommandHelpers
    {
        public static Type[] GetAllOptions()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.GetCustomAttribute<VerbAttribute>() != null)
                .ToArray();
        }


        public static async Task ExecuteCommandAsync(object options, CancellationToken cancellationToken)
        {
            // Figure out the name of the command type
            var commandName = options.GetType().Name.Replace("Options", "Command", StringComparison.Ordinal);

            // Get the matching type from the commands namespace
            var commandType = Type.GetType($"SwishDB.TestCli.Commands.{commandName}");

            // Instantiate the command object
            var command = (IAbstractCommand)Activator.CreateInstance(commandType);

            // Execute the command
            await command.ExecuteAsync(options, cancellationToken);
        }
    }
}

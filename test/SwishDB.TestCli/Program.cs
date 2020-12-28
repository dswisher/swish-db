
using System;
using System.Threading;
using System.Threading.Tasks;

using CommandLine;
using SwishDB.TestCli.Helpers;

namespace SwishDB.TestCli
{
    public sealed class Program
    {
        public static async Task<int> Main(string[] args)
        {
            try
            {
                var parsedArgs = Parser.Default.ParseArguments(args, CommandHelpers.GetAllOptions());

                // Run the command that matches the options that were parsed. This is a convention:
                //    FooOptions must have a matching FooCommand class.
                using (var tokenSource = new CancellationTokenSource())
                {
                    // Shut down semi-gracefully on ctrl+c...
                    Console.CancelKeyPress += (sender, eventArgs) =>
                    {
                        Console.WriteLine("*** Cancel event triggered ***");
                        tokenSource.Cancel();
                        eventArgs.Cancel = true;
                    };

                    await parsedArgs.WithParsedAsync(opt => CommandHelpers.ExecuteCommandAsync(opt, tokenSource.Token));
                }

                // It worked!
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unhandled exception:");
                Console.WriteLine(ex);

                return 1;
            }
        }
    }
}

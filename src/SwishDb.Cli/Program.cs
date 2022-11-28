using System;
using System.Threading.Tasks;

namespace SwishDb.Cli
{
    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            // Set up the DI container that will be used within the CLI
            // TODO

            // Set up the ctrl-c handler
            // TODO

            // Resolve the CLI from the container, and run
            await Task.CompletedTask;   // TODO - HACK!

            Console.WriteLine("Hello World!");

            return 0;
        }
    }
}


using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using SwishDB.TestCli.Options;

namespace SwishDB.TestCli.Commands
{
    public class CreateCommand : IAbstractCommand
    {
        public async Task ExecuteAsync(object opts, CancellationToken cancellationToken)
        {
            // TODO - wire up logging/tracing and use that instead of Console.WriteLine

            var options = (CreateOptions)opts;

            // This should start fresh: if the DB file already exists, delete it.
            if (File.Exists(options.DBPath))
            {
                Console.WriteLine("Deleting existing DB file...");
                File.Delete(options.DBPath);
            }

            // Create the database
            // TODO - configuration: page size, etc.
            using (var db = await SwishDB.DatabaseManager.OpenAsync(options.DBPath))
            {
            }

            // Print the status.
            if (File.Exists(options.DBPath))
            {
                Console.WriteLine("File created: {0}", options.DBPath);
            }
            else
            {
                Console.WriteLine("File NOT created: {0}", options.DBPath);
            }
        }
    }
}

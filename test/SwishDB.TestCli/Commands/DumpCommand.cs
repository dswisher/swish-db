
using System;
using System.Threading;
using System.Threading.Tasks;

using SwishDB.TestCli.Options;

namespace SwishDB.TestCli.Commands
{
    public class DumpCommand : IAbstractCommand
    {
        public async Task ExecuteAsync(object opts, CancellationToken cancellationToken)
        {
            var options = (DumpOptions)opts;

            // Use the internal PageFile class to open up the specified file.
            var pageFile = new PageFile();

            await pageFile.OpenAsync(options.DBFileName, cancellationToken);

            // TODO - dump all the pages
            Console.WriteLine("Dump is not yet implemented.");
        }
    }
}

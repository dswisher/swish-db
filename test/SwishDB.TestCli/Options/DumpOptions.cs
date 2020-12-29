
using CommandLine;

namespace SwishDB.TestCli.Options
{
    [Verb("dump", HelpText = "Dump the internals of a database file.")]
    public class DumpOptions
    {
        [Option("db-file", Default = "db.sdb", HelpText = "The name of the database file to create.")]
        public string DBFileName { get; set; }

        [Option("page", HelpText = "Dump the specified page.")]
        public int? PageNum { get; set; }
    }
}


using CommandLine;

namespace SwishDB.TestCli.Options
{
    [Verb("create", HelpText = "Create a new database.")]
    public class CreateOptions
    {
        [Option("db-file", Default = "db.sdb", HelpText = "The name of the database file to create.")]
        public string DBPath { get; set; }

        [Option("verbose", HelpText = "Enable debug-level logging.")]
        public bool Verbose { get; set; }
    }
}

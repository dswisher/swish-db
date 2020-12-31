
using System;
using System.Threading;
using System.Threading.Tasks;

using SwishDB.Pages;
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

            // If they want to dump a specific page, do so.
            if (options.PageNum.HasValue)
            {
                var page = await pageFile.ReadPageAsync(options.PageNum.Value, cancellationToken);

                DumpPage(options.PageNum.Value, page);
            }
            else
            {
                // Get the number of pages
                var numPages = pageFile.NumPages;

                Console.WriteLine("Num Pages: {0}", numPages);
                Console.WriteLine();
                Console.WriteLine("Page  Description");
                Console.WriteLine("----  -----------");

                // Iterate through all the pages, and print a summary of each
                for (var i = 0; i < numPages; i++)
                {
                    var page = await pageFile.ReadPageAsync(i, cancellationToken);

                    Console.WriteLine("{0,4}  {1}", i, page.ToString());
                }
            }
        }


        private static void DumpPage(int pageNum, Page page)
        {
            if (page is ZeroPage zero)
            {
                DumpZeroPage(pageNum, zero);
            }
            else
            {
                Console.WriteLine("Don't yet know how to dump a page of type {0} (pageNum={1}).", page.GetType().Name, pageNum);
            }
        }


        private static void DumpZeroPage(int pageNum, ZeroPage page)
        {
            Console.WriteLine("Page {0}, Zero Page:", pageNum);
            Console.WriteLine("   Page Size: {0}", page.PageSize);
        }
    }
}

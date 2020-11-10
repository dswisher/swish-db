// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using SwishDB;

namespace CreateWriteRead
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed.")]
    public class Program
    {
        private const string Filename = "example.db";

        public static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Opening {0}...", Filename);

                using (var db = await DatabaseManager.OpenAsync(Filename))
                {
                    // Get the bytes for a simple string
                    var encoding = Encoding.UTF8;

                    var lorem = encoding.GetBytes("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");

                    // Write the object
                    await db.WriteObjectAsync(1, lorem);

                    // Read the object back
                    var bytes = await db.ReadObjectAsync(1);

                    // And print it
                    Console.WriteLine(encoding.GetString(bytes));
                }

                // Clean up
                if (File.Exists(Filename))
                {
                    File.Delete(Filename);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

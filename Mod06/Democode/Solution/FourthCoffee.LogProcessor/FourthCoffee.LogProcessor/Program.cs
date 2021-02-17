using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourthCoffee.LogProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO uesr need to change directory 

            // OO: LogLocator håller en mapp och kan lista textfiler inuti

            string folder =  Path.Combine(FindRepoRoot(), @"Mod06\Democode\Data\Logs\");

            var logLocator = new LogLocator(folder);

            var logCombiner = new LogCombiner(logLocator);

            // OO: Kombinera alla textfiler i foldern (förutom CombinedLog.txt) och skriv till CombinedLog.txt
            logCombiner.CombineLogs(folder + "CombinedLog.txt");

        }

        private static string FindRepoRoot()
        {
            var folder = new DirectoryInfo(Directory.GetCurrentDirectory());

            while (true)
            {
                if (folder.Name == "Allfiles for code signature")
                    return folder.FullName;
                folder = folder.Parent;
            }
        }
    }
}

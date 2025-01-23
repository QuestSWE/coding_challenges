using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;

namespace qwc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Error: Invalid option. Use '-w' followed by a file name (e.g., program.exe -w file.txt");
                return;
            }
            else if (args[0] != "-w")
            {
                Console.WriteLine("Error: Invalid option. Use '-w' to specify the word count operation.");
                return;
            }
            else if (!File.Exists(args[1]))
            {
                Console.WriteLine($"Error: The specified file {args[1]} does not exist.");
                return;
            }
            else
            {
                CountWordsInFile(args[1], args);
            }


            static void CountWordsInFile(string filePath, string[] args)
            {
                // ***** WORD COUNT GOAL: 58164 (ACHIEVED) ****    <TODO: GET RID OF THIS COMMENT WHEN THE PROGRAM IS COMPLETED>
                IEnumerable<string> lines = File.ReadLines(filePath);
                int totalWord = 0;

                foreach (string line in lines)
                {
                    // The regex pattern @"\S+" matches every sequence of non-whitespace characters.
                    // This replicates the behavior of the Linux `wc` tool for counting words.
                    MatchCollection matches = Regex.Matches(line, @"\S+");
                    totalWord += matches.Count;
                    //Console.WriteLine($"Words: {string.Join(", ", matches.Select(m => m.Value))}");
                }
                Console.WriteLine($"{totalWord} {Path.GetFileName(args[1])}");
            }

            //Console.ReadKey();
            //THIS IS A COMMIT MESSAGE TEST
        }
    }

}


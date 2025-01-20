using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;

namespace qwc
{
    internal class Program
    {
        // Delimiters used to split lines into words (e.g., spaces, punctuation marks).
        // This static field is used in the CountWordsInFile method for splitting text.
        internal static readonly char[] separator = [' ', '.', ',', '!', '?', ':', ';'];

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
                CountWordsInFile(args[1]);
            }


            static void CountWordsInFile(string filePath)
            {
                IEnumerable<string> lines = File.ReadLines(filePath);
                int totalWord = 0;
                foreach (string line in lines)
                {
                    //string[] words = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    MatchCollection matches = Regex.Matches(line.Trim(), @"\b[A-Za-z0-9]+\b");
                    Console.WriteLine($"Line: {line}");
                    Console.WriteLine($"Words: {string.Join(", ", matches.Select(m => m.Value))}");
                    int wordCount = matches.Count;
                    totalWord += wordCount;
                }
                Console.WriteLine(totalWord);
            }

            Console.ReadKey();
        }
    }

}


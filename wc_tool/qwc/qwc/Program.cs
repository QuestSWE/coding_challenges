using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;

namespace qwc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (!ArgsValidation(args, out var errorMessage))
            {
                Console.WriteLine(errorMessage);
                return;
            }

            Dictionary<string, Action<string>> options = new()
            {
                { "-c", CountBytesInFile },
                { "-w", CountWordsInFile }
            };

            if (options.TryGetValue(args[0], out Action<string>? value))
            {
                value(args[1]);
            }

            Console.ReadKey();

        }

        static void CountBytesInFile(string filePath)
        {
            FileInfo fileInfo = new(filePath);
            long fileSizeInBytes = fileInfo.Length;
            Console.WriteLine($"{fileSizeInBytes} {Path.GetFileName(filePath)}");

        }

        static void CountWordsInFile(string filePath)
        {
            IEnumerable<string> lines = File.ReadLines(filePath);
            int totalWord = 0;

            foreach (string line in lines)
            {
                // The regex pattern @"\S+" matches every sequence of non-whitespace characters.
                // This replicates the behavior of the Linux `wc` tool for counting words.
                MatchCollection matches = Regex.Matches(line, @"\S+");
                totalWord += matches.Count;
            }
            Console.WriteLine($"{totalWord} {Path.GetFileName(filePath)}");
        }

        internal static readonly string[] sourceArray = ["-c", "-w"];
        private static bool ArgsValidation(string[] args, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (args.Length != 2)
            {
                errorMessage = "Error: Invalid input. Use  an option followed by a file name (e.g. -w file.txt).";
                return false;
            }
            else if (!sourceArray.Contains(args[0]))
            {
                errorMessage = "Error: Invalid option. Type -help to see options list.";
                return false;
            }
            else if (!File.Exists(args[1]))
            {
                errorMessage = $"Error: The specified file {args[1]} does not exist.";
                return false;
            }

            return true;
        }

    }
}

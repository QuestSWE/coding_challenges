using System.Text.RegularExpressions;

namespace qwc
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (!ArgsValidation(args, out var errorMessage))
            {
                Console.WriteLine(errorMessage);
                return;
            }

            Dictionary<string, Action<string>> options = new()
            {
                { "-c", CountBytesInFile },
                { "-w", CountWordsInFile },
                { "-l", CountLinesInFile },
                { "-m", CountCharInFile}

            };

            if (options.TryGetValue(args[0], out var value))
            {
                value(args[1]);
            }

            Console.ReadKey();

        }

        private static void CountBytesInFile(string filePath)
        {
            FileInfo fileInfo = new(filePath);
            var fileSizeInBytes = fileInfo.Length;
            Console.WriteLine($"{fileSizeInBytes} {Path.GetFileName(filePath)}");
        }

        private static void CountLinesInFile(string filePath)
        {
            var lines = File.ReadLines(filePath);
            var numLines = lines.Count();
            Console.WriteLine($"{numLines} {Path.GetFileName(filePath)}");
        }

        private static void CountWordsInFile(string filePath)
        {
            var lines = File.ReadLines(filePath);
            var totalWord = lines.Select(line => Regex.Matches(line, @"\S+")).Select(matches => matches.Count).Sum();

            Console.WriteLine($"{totalWord} {Path.GetFileName(filePath)}");
        }

        private static void CountCharInFile(string filePath)
        {
            var lines = File.ReadLines(filePath);
            var numChar = lines.Sum(line => line.Length);
            Console.WriteLine($"{numChar} {Path.GetFileName(filePath)}");
        }


        internal static readonly string[] SourceArray = ["-c", "-w", "-l", "-m"];

        private static bool ArgsValidation(string[] args, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (args.Length != 2)
            {
                errorMessage = "Error: Invalid input. Use  an option followed by a file name (e.g. -w file.txt).";
                return false;
            }

            if (!SourceArray.Contains(args[0]))
            {
                errorMessage = "Error: Invalid option. Type -help to see options list.";
                return false;
            }

            if (File.Exists(args[1])) return true;
            errorMessage = $"Error: The specified file {args[1]} does not exist.";
            return false;

        }

    }
}

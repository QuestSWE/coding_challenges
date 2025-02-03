using System.Text;
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

            if (args.Length == 1)
            {
                UseAllOptions(args[0], true);
                return;
            }

            Dictionary<string, Func<string, int>> options = new()
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

            //Console.ReadKey();

        }
        private static int CountBytesInFile(string filePath)
            => CountBytesInFile(filePath, true);
        private static int CountBytesInFile(string filePath, bool printResult)
        {
            FileInfo fileInfo = new(filePath);
            var fileSizeInBytes = fileInfo.Length;
            if (printResult)
                Console.WriteLine($"{fileSizeInBytes} {Path.GetFileName(filePath)}");

            return (int)fileSizeInBytes;
        }

        private static int CountLinesInFile(string filePath)
            => CountLinesInFile(filePath, true);
        private static int CountLinesInFile(string filePath, bool printResult)
        {
            var lines = File.ReadLines(filePath);
            var numLines = lines.Count();
            if (printResult)
                Console.WriteLine($"{numLines} {Path.GetFileName(filePath)}");

            return numLines;
        }
        private static int CountWordsInFile(string filePath)
            => CountWordsInFile(filePath, true);
        private static int CountWordsInFile(string filePath, bool printResult)
        {
            var lines = File.ReadLines(filePath);
            var totalWord = lines
                .Select(line => Regex.Matches(line, @"\S+"))
                .Select(matches => matches.Count)
                .Sum();
            if (printResult)
                Console.WriteLine($"{totalWord} {Path.GetFileName(filePath)}");

            return totalWord;
        }

        /// <summary>
        /// Counts the number of characters in a file while properly handling UTF-8 encoded files.
        /// </summary>
        /// <param name="filePath">The path to the file to be analyzed.</param>
        /// <remarks>
        /// - Reads the file as raw bytes and processes it using a StreamReader to correctly interpret UTF-8 encoding.
        /// - Uses a buffer to efficiently count characters in chunks, reducing memory usage for large files.
        /// - The 'encoderShouldEmitUTF8Identifier' flag is set to false to ensure BOM (Byte Order Mark) bytes are not included.
        /// - The 'detectEncodingFromByteOrderMarks' flag is disabled to prevent automatic encoding detection, ensuring manual control.
        /// - Outputs the total character count along with the file name.
        /// </remarks>
        /// TODO: handle UTF-16 LE, UTF-16 BE, UTF-32 LE, UTF-32 BE

        private static int CountCharInFile(string filePath)
            => CountCharInFile(filePath, true);
        private static int CountCharInFile(string filePath, bool printResult)
        {
            using var sr = new StreamReader(
                new MemoryStream(File.ReadAllBytes(filePath)),
                new UTF8Encoding(encoderShouldEmitUTF8Identifier: false),
                detectEncodingFromByteOrderMarks: false);

            var totalChar = 0;
            Span<char> buffer = new char[1024];
            while (sr.ReadBlock(buffer) is var count and > 0)
            {
                totalChar += count;
            }
            if (printResult)
                Console.WriteLine($"{totalChar} {Path.GetFileName(filePath)}");

            return totalChar;
        }

        private static void UseAllOptions(string filePath, bool printResult)
        {
            var lineCount = CountLinesInFile(filePath, false);
            var wordCount = CountWordsInFile(filePath, false);
            var byteCount = CountBytesInFile(filePath, false);

            if (printResult)
                Console.WriteLine($"{lineCount} {wordCount} {byteCount} {Path.GetFileName(filePath)}");

        }

        internal static readonly string[] SourceArray = ["-c", "-w", "-l", "-m"];

        private static bool ArgsValidation(string[] args, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (args.Length is < 1 or > 2)
            {
                errorMessage = "Error: Invalid input. Use  an option followed by a file name (e.g. -w file.txt). Using only the file name = -c, -l, -w";
                return false;
            }

            if (args.Length == 1)
            {
                if (File.Exists(args[0])) return true;
                errorMessage = $"Error: The specified file {args[0]} does not exist.";
                return false;
            }

            if (!SourceArray.Contains(args[0]))
            {
                errorMessage = "Error: Invalid option. Type -help to see options list."; // TODO add instruction of help option;
                return false;
            }


            if (File.Exists(args[1])) return true;

            errorMessage = $"Error: The specified file {args[1]} does not exist.";
            return false;
        }

    }
}

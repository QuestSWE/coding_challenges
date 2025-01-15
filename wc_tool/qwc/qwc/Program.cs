using Microsoft.VisualBasic.FileIO;


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
                Console.WriteLine("Error: Invalid option. Use '-c' followed by a file name (e.g., program.exe -c file.txt");
                return;
            }
            else if (args[0] != "-c")
            {
                Console.WriteLine("Error: Invalid option. Use '-c' to specify the word count operation.");
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

                foreach (string line in lines)
                {
                    string[] words = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                }

            }

        }
    }
}


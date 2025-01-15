using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;
using System.Linq;

namespace qwc
{
    internal class Program
    {
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
                CountWordInFile(args[1]);
            }

            static void CountWordInFile(string filePath)
            {

                string text = File.ReadAllText(filePath);
                string[] words = text.Split();
            }

        }
    }
}


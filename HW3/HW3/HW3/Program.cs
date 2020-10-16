using System;
using System.IO;

namespace HW3
{
    internal class Program
    {
        //Print the command line usage
        public static void PrintUsage()
        {
            Console.WriteLine("Usage is: \n " +
                "\n dotnet run c inputFile outputFile \n\n" +
                "Where: \n\n" +
                "\t C is the column number to fit to \n" +
                "\t inputFIle is the input text file \n" +
                "\t outputFile is the new output file base name containing wrapped text \n" +
                "\n e.g dotnet run 50 inputfile.txt outputFile.txt \n\n  ");
        }

        /// <summary>
        /// Main program getting I/O and setting items up for WrapSimply
        /// </summary>
        /// <param name="args">The command line arguments including an int, two strings</param>
        /// <returns>Status code for exit program</returns>
        private static int Main(string[] args)
        {
            int columnLength;
            string inputFileName;
            string outputFileName;
            try
            {
                inputFileName = args[1];
                outputFileName = args[2];
                columnLength = int.Parse(args[0]);
            }
            catch (Exception)
            {
                PrintUsage();
                return 1;
            }
            return SetupWrapSimply(columnLength, inputFileName, outputFileName);
        }

        /// <summary>
        ///  Used to initiate variables required for WrapSimply
        /// </summary>
        /// <param name="columnLength">Number of columns per row</param>
        /// <param name="inputFileName">File to take txt from</param>
        /// <param name="outputFileName">New file with different wrap</param>
        /// <returns>Status code to exit</returns>
        private static int SetupWrapSimply(int columnLength, string inputFileName, string outputFileName)
        {
            IQueue<string> words = new LinkedQueue<string>();
            StreamReader sr = ReadFile(inputFileName);
            if (sr == null)
            {
                PrintUsage();
                return 1;
            }
            while (sr.Peek() >= 0)
            {
                foreach (string word in sr.ReadLine().Split(" "))
                {
                    words.Push(word);
                }
            }
            int spacesRemaining = WrapSimply(words, columnLength, outputFileName);
            Console.WriteLine($"Total spaces remaining (Greedy): {spacesRemaining}");
            return 0;
        }

        /// <summary>
        /// Wrap row to desired width of columns
        /// </summary>
        /// <param name="words">Linked list of all the words from file</param>
        /// <param name="columnLength">Length of row</param>
        /// <param name="outputFileName">File to write words to</param>
        /// <returns>Spaces left</returns>
        private static int WrapSimply(IQueue<string> words, int columnLength, string outputFileName)
        {
            StreamWriter outt = OpenFile(outputFileName);
            int col = 1;
            int spacesRemaining = 0;
            while (!words.IsEmpty())
            {
                string str = words.Peek();
                int len = str.Length;
                if (col == 1)
                {
                    outt.Write(str);
                    col += len;
                    words.Pop();
                }
                else if ((col + len) >= columnLength)
                {
                    outt.WriteLine();
                    spacesRemaining += (columnLength - col) + 1;
                    col = 1;
                }
                else
                {
                    outt.Write(" ");
                    outt.Write(str);
                    col += (len + 1);
                    words.Pop();
                }
            }
            outt.WriteLine();
            outt.Flush();
            outt.Close();
            return spacesRemaining;
        }

        /// <summary>
        /// Create a StreamReader with error checking
        /// </summary>
        /// <param name="inputFileName">Name of file to create lock on</param>
        /// <returns>StreamReader to read from file</returns>
        private static StreamReader ReadFile(string inputFileName)
        {
            StreamReader sr;
            try
            {
                sr = new StreamReader(inputFileName);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Caouldn't find input file");
                return null;
            }
            return sr;
        }

        /// <summary>
        /// Try to open a file to write to.
        /// </summary>
        /// <param name="outputFileName">File that will be written to</param>
        /// <returns>StreamWriter to write from file</returns>
        private static StreamWriter OpenFile(string outputFileName)
        {
            StreamWriter outt;
            if (File.Exists(outputFileName))
            {
                outt = new StreamWriter(outputFileName);
            }
            else
            {
                Console.WriteLine($"Cannot create or open {outputFileName}" +
                $"for writing. Using standard output instead");
                outt = new StreamWriter(Console.OpenStandardOutput())
                {
                    AutoFlush = true
                };
            }
            return outt;
        }
    }
}
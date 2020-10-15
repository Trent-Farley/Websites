using System;
using System.IO;

namespace HW3
{
    internal class Program
    {
        public static void PrintUsage()
        {
            Console.WriteLine("Usage is: \n " +
                "\n FIXME ");
        }

        private static int Main(string[] args)
        {
            string inputFileName;
            string outputFileName;
            int c;
            StreamReader sr;
            IQueueInterface<string> words = new LinkedQueue<string>();
            if (args.Length != 3)
            {
                PrintUsage();
                return 1;
            }

            try
            {
                c = int.Parse(args[0]);
                inputFileName = args[1];
                outputFileName = args[2];
                sr = new StreamReader(inputFileName);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Caouldn't find input file");
                return 1;
            }
            catch (Exception)
            {
                Console.WriteLine("Something is wrong with input");
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
            int spacesRemaining = WrapSimply(words, c, outputFileName);
            Console.WriteLine($"Total spaces remaining (Greedy): {spacesRemaining}");
            return 0;
        }

        private static int WrapSimply(IQueueInterface<string> words, int c, string outputFileName)
        {
            StreamWriter outt;
            try
            {
                outt = new StreamWriter(outputFileName);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Cannot create or open {outputFileName}" +
                    $"for writing. Using standard output instead");
                outt = new StreamWriter(Console.OpenStandardOutput())
                {
                    AutoFlush = true
                };
            }
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
                else if ((col + len) > c)
                {
                    outt.WriteLine();
                    spacesRemaining += (c - col) + 1;
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
    }
}
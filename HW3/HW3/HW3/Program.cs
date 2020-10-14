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
            foreach (var arg in args)
            {
                Console.Write($"{arg} ");
            }
            //if (args.Length != 3)
            //{
            //    PrintUsage();
            //    return 1;
            //}
            try
            {
                //int c = int.Parse(args[0]);
                inputFileName = args[1];
                outputFileName = args[2];
                //IQueueInterface<string> words = new LinkedQueue<string>();

                using StreamReader sr = new StreamReader(inputFileName);
                while (sr.Peek() >= 0)
                {
                    foreach (string word in sr.ReadLine().Split(" "))
                    {
                        Console.WriteLine(word);
                        //words.Push(word);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Caouldn't find input file");
                return 1;
            }
            catch (Exception)
            {
                Console.WriteLine("Something wrong with input");
                PrintUsage();
                return 1;
            }

            return 0;
        }
    }
}
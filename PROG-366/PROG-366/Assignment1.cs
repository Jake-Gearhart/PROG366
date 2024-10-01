using System;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] data = { "one", "two", "three", "four" };
            constant(data);
            linear(data);
            exponential(data);
        }

        static void constant(string[] args)
        {
            // print first string in args
            Console.WriteLine(args[0]);
        }

        static void linear(string[] args)
        {
            // print each string in args separately
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine(args[i]);
            }
        }

        static void exponential(string[] args)
        {
            // print each string in args concatenated with each other string
            for (int i = 0; i < args.Length; i++)
            {
                string newString = args[i];
                for (int j = 0; j < args.Length; j++)
                {
                    if (j != i)
                    {
                        newString += args[j];
                    }
                }
                Console.WriteLine(newString);
            }
        }
    }
}
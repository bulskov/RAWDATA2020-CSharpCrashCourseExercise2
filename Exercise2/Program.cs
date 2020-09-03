using System;
using System.Globalization;

namespace Exercise2
{
    public class Program
    {
        // I'v made the reverse Polish calculator as a static class
        // Then you do not need to instantiate an object to use it.
        // It would also have been fine to implement it as an normal class.
        // Did the same with the shooting yard class

        static ConsoleColor defaultForgroundColor = Console.ForegroundColor;

        static void Main(string[] args)
        {
            // Just for fun - coloring the console text :-)
            WriteColorLine(ConsoleColor.Cyan, "Exercise_2_1_2\n");

            // Example from exercise
            WriteColor(ConsoleColor.Green, "5 1 2 + 4 * + 3 - ");
            Console.Write("= ");
            WriteColorLine(ConsoleColor.Red, ReversePolishCalculator.Compute("5 1 2 + 4 * + 3 -"));

            // Example from https://en.wikipedia.org/wiki/Shunting-yard_algorithm

            WriteColor(ConsoleColor.Green, "3 + 4 * 2 / (1 - 5) ^ 2 ^ 3 ");
            Console.Write("= ");
            WriteColorLine(ConsoleColor.Red, ReversePolishCalculator.Compute(ShuntingYard.Parse("3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3")));

            Console.WriteLine("\nThe correct answer if we had used real numbers (doubles instead of integers) would be:");
            WriteColor(ConsoleColor.Green, "3 + 4 * 2 / (1 - 5) ^ 2 ^ 3 ");
            Console.Write("= ");
            var result = 3.0 + ((4.0 * 2.0) / Math.Pow((1.0 - 5.0), Math.Pow(2.0, 3.0)));
            WriteColorLine(ConsoleColor.Red, result.ToString(CultureInfo.InvariantCulture));
            // the use of CultureInfo.InvariantCulture prevent formatting of numbers using the
            // systems format, ie. for me the Danish format with a comma instead of a dot: 3,0001220703125
        }

        // These two methods are examples on how to modify the colors in the console window
        static void WriteColorLine(ConsoleColor color, object output)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(output);
            Console.ForegroundColor = currentColor;
        }

        static void WriteColor(ConsoleColor color, object output)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(output);
            Console.ForegroundColor = currentColor;
        }
    }
}

using Fib;

namespace FibConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Welcome to fibonacci number generator! ===");
            Console.WriteLine("Press [q] to quit");

            do
            {
                try
                {
                    Console.Write("Which fibonacci number to print: ");
                    string? input = Console.ReadLine();
                    if (input == "q")
                        break;

                    int n = Convert.ToInt32(input);
                    Console.WriteLine(Fibonacci.GetNthNumber(n));
                }
                catch
                {
                    Console.WriteLine("Invalid input, try again.");
                    continue;
                }
            } while (true);
        }
    }
}
using Sudoku.src.Logic;

namespace Sudoku.src
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //catches ^C and Prints BYE! ;)
            Console.CancelKeyPress += (object? sender, ConsoleCancelEventArgs e) =>
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nBYE! ;)");
            };
            MainController.Run();
        }
    }
}

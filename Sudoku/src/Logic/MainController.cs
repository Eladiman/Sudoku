using Sudoku.src.Consts;
using Sudoku.src.Entities.Models;
using Sudoku.src.UI.InPut;
using Sudoku.src.UI.OutPut;
using System.Diagnostics;

namespace Sudoku.src.Logic
{
    /// <summary>
    /// The following class responsible for Run the program.
    /// Use of other classes for :
    /// read boards from user
    /// solve the boards
    /// show the answer to the user 
    /// </summary>
    public static class MainController
    {
        private static bool _run = true;


        /// <summary>
        /// The following function runs as long as the user hasn't pressed 3 (for exit)
        /// and constantly allows the user to input new boards and solve them.
        /// </summary>
        public static void Run()
        {
            while (_run)
            {
                try
                {
                    MainGameManager();
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        /// <summary>
        /// The following function receives the user's chosen option and operates accordingly.
        /// 1. gets board from cli
        /// 2. gets board from text file
        /// 3. exit the program
        /// </summary>
        private static void MainGameManager()
        {
            ShowMenu();
            string option = Console.ReadLine();

            if(option == null) { option = "3"; }//for ^C

            string expression = null;
            string filePath = null;
            switch (option)
            {
                case "1":
                    Console.WriteLine("\nEnter Sudoku: ");
                    expression = CliInPutHandler.GetInputFromUser();
                    break;

                case "2":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Enter file path: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    filePath = Console.ReadLine(); //get file path from user
                    expression = TextInPutHandler.GetInputFromUser(filePath);
                    break;

                case "3":
                    Console.WriteLine("BYE! ;)");
                    _run = false;
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Option is not valid! please enter 1,2 or 3!");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
            if (expression != null)
            {
                TrySolveBoard(expression, filePath); //attempted to solve the board
            }
        }

        /// <summary>
        /// The following function receives an expression, checks whether it is solvable,
        /// and prints whether it is solvable along with its solution, or if it is unsolvable.
        /// </summary>
        /// <param name="expression">the expression to solve</param>
        /// <param name="path">The path of the txt file. if path not null then put the result on the file</param>
        private static void TrySolveBoard(string expression,string path)
        {
            Stopwatch stopWatch = new Stopwatch();

            string str = expression;
            str = str.Replace('.', '0');

            Validator.CheckNumber(str);
            Validator.CheckLength(str);

            Board board = new Board(str);
            Console.WriteLine(board);
            stopWatch.Start();
            bool solved = BoardSolver.SolveBoard(board);
            if (!solved) Console.WriteLine(board.GetString());
            stopWatch.Stop();

            CliOutPutHandler.PrintInputForUser(board,stopWatch,solved);

            if(path!=null) TextOutPutHandler.PrintInputForUserInText(board, stopWatch,solved,path);

            //Console.WriteLine($"{BoardSolver.cnt}");
        }

        /// <summary>
        /// The following function displays a menu of different actions in the system to the user via the CLI.
        /// </summary>
        private static void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=== SUDOKU SOLVER ===\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. Input via Console");
            Console.WriteLine("2. Input via Text File");
            Console.WriteLine("3. Exit\n");

            Console.Write("Enter your choice: ");
        }
    }
}

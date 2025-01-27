using Sudoku.src.Consts;
using Sudoku.src.Entities.Exceptions;
using Sudoku.src.Entities.Models;
using Sudoku.src.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.Logic
{
    public static class MainController
    {
        private static bool  run = true;
        
        public static void Run()
        {
            while (run)
            {
                try
                {
                    string expression = GetExpression();
                    if (expression != null)
                    {
                        TrySolveBoard(expression);
                    }
                }
                catch (Exception e) when (e is SyntaxException || e is LogicalException || e is OutOfMemoryException || e is IOException || e is ArgumentOutOfRangeException)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static string GetExpression()
        {
            ShowMenu();
            String option = Console.ReadLine();
            String expression = null;
            switch (option)
            {
                case "1":
                    Console.WriteLine("\nEnter Sudoku: ");
                    expression = CliInPutHandler.GetInputFromUser();
                    break;

                case "2":
                    expression = TextInPutHandler.GetInputFromUser();
                    break;

                case "3":
                    Console.WriteLine("BYE! ;)");
                    run = false;
                    break;

                default:
                    Console.WriteLine("Option is not valid! please enter 1,2 or 3!");
                    break;
            }
            return expression;
        }

        private static void TrySolveBoard(string expression)
        {
            string str = expression;
            str = str.Replace('.', '0');

            Validation.CheckLength(str);
            Validation.CheckNumber(str);

            SudokuConstants.Board_size = (int)Math.Sqrt(str.Length);
            SudokuConstants.Sqrt_Board_size = (int)(Math.Sqrt(SudokuConstants.Board_size));

            Board board = new Board(str);
            Console.WriteLine(board);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (BoardSolver.SolveBoard(board))
            {
                stopWatch.Stop();
                Console.WriteLine(board);
                Console.WriteLine("Time took: " + stopWatch.ElapsedMilliseconds + " ms");
            }
            else
            {
                stopWatch.Stop();
                Console.WriteLine("Board is not Solvable");
                Console.WriteLine("Time took: " + stopWatch.ElapsedMilliseconds + " ms");
                
            }
            //Console.WriteLine($"{BoardSolver.cnt}");
        }

        private static void ShowMenu()
        {
            Console.WriteLine("\nPlease enter your Sudoku!: " +
                "\n1. Insert Sudoku using the command line."+
                "\n2. Insert Sudoku using a text file."+
                "\n3. Exit the Program");
        }
    }
}

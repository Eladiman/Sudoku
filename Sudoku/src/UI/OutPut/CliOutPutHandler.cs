using Sudoku.src.Entities.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.UI.OutPut
{
    public static class CliOutPutHandler
    {
        /// <summary>
        /// prints the result in the console
        /// </summary>
        /// <param name="board"> the board to print</param>
        /// <param name="stopwatch">the stopwatch to indicate the time </param>
        /// <param name="solved">flag to know if board is solved</param>
        public static void PrintInputForUser(Board board, Stopwatch stopwatch,bool solved)
        {
            if (solved)
            {
                Console.WriteLine(board);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Board is not Solvable");
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Time took: " + stopwatch.ElapsedMilliseconds + " ms");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

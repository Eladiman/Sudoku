using Sudoku.src.Entities.Models;
using System.Diagnostics;

namespace Sudoku.src.UI.OutPut
{
    public static class TextOutPutHandler
    {
        /// <summary>
        /// prints the result in the file given the path
        /// </summary>
        /// <param name="board"> the board to print</param>
        /// <param name="stopwatch">the stopwatch to indicate the time </param>
        /// <param name="solved">flag to know if board is solved</param>
        /// <param name="filePath">the file path to write into the file</param>
        public static void PrintInputForUserInText(Board board, Stopwatch stopwatch, bool solved, string filePath)
        {
            filePath = filePath.Trim();

            filePath = filePath.Trim('"');

            if (solved)
            {
                File.AppendAllText(filePath, "\nThe Solution is: \n");
                File.AppendAllText(filePath, board.GetString());
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                File.AppendAllText(filePath, "\nBoard is not Solvable\n");
            }
        }
    }
}

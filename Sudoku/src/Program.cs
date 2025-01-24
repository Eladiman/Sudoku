using Sudoku.src.Entities.Models;
using Sudoku.src.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Board board = new Board("400000805030000000000700000020000060000080400000010000000006030705002000001040000");
            Console.WriteLine(board);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (BoardSolver.SolveBoard(board))
            {
                stopWatch.Stop();
                Console.WriteLine("Time took: " + stopWatch.ElapsedMilliseconds+" ms");
                Console.WriteLine(board);
            }
            else
            {
                stopWatch.Stop();
                Console.WriteLine("Time took: " + stopWatch.ElapsedMilliseconds+" ms");
                Console.WriteLine("Board is not Solvable");
            }
        }
    }
}

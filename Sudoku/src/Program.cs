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
            Board board = new Board("800000070006010053040600000000080400003000700020005038000000800004050061900002000");
            Console.WriteLine(board);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (BoardSolver.SolveBoard(board))
            {
                stopWatch.Stop();
                Console.WriteLine("Time took: " + stopWatch.ElapsedMilliseconds + " ms");
                Console.WriteLine(board);
            }
            else
            {
                stopWatch.Stop();
                Console.WriteLine("Time took: " + stopWatch.ElapsedMilliseconds + " ms");
                Console.WriteLine("Board is not Solvable");
            }
        }
    }
}

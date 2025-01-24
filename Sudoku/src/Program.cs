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
            Board board = new Board("900800000000000500000000000020010003010000060000400070708600000000030100400000200");
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

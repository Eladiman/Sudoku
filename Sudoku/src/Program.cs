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
            string str = "000030000060000400007050800000406000000900000050010300400000020000300000000000000";
            str = str.Replace('.', '0');
            Board board = new Board(str);
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

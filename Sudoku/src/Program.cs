using Sudoku.src.Entities.Models;
using Sudoku.src.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Board board = new Board("000005080000601043000000000010500000000106000300000005530000061000000004000000000");
            Console.WriteLine(board);

            if(BoardSolver.SolveBoard(board)) Console.WriteLine(board);
            else Console.WriteLine("Board is not Solvable");
        }
    }
}

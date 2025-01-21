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
            Board board = new Board("400000805030000000000700000020000060000080400000010000000603070500200000104000000");
            Console.WriteLine(board);

            if(BoardSolver.SolveBoard(board)) Console.WriteLine(board);
            else Console.WriteLine("Board is not Solvable");
        }
    }
}

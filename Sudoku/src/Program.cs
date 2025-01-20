using Sudoku.src.Entities.Models;
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
            Board board = new Board("000030000060000400007050800000406000000900000050010300400000020000300000000000000");
            Console.WriteLine(board);
        }
    }
}

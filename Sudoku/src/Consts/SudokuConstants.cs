using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.Consts
{
    public static class SudokuConstants
    {
        public const int MAX_BOARD_SIZE = 25;

        public static int Board_size {  get; set; }
        //public static int Board_size = 9;
        public static int Sqrt_Board_size { get; set;}
        //public static int Sqrt_Board_size = 3;

        public const char ASCII_DIFF = '0';
    }
}

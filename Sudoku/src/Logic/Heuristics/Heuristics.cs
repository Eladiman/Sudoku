using Microsoft.VisualBasic;
using Sudoku.src.Consts;
using Sudoku.src.Entities.Interfaces;
using Sudoku.src.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Sudoku.src.Logic.Heuristics
{
    public static class Heuristics
    {
        /// <summary>
        /// Goes through all the empty cells and if it detects two cells with the same 2 options
        /// then Removes these options from the row/column/box where they were found
        /// </summary>
        /// <param name="board">The board on which the function will run</param>
        public static void NakedPairs(Board board)
        {

        }
        /// <summary>
        /// Goes through all the cells and looks for a cell that has only one option left.
        /// and then it adds the cell to the list of full cells
        /// </summary>
        /// <param name="board">The board on which the function will run</param>
        /// <returns>Returns true if a cell has been added to the filled cells</returns>
        public static bool NakedSingle(Board board)
        {
            return false;
        }
    }
}

using Sudoku.src.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.Logic
{
    public static class Heuristics
    {
        /// <summary>
        /// Goes through all the filled cells which did not visited them and Removes the possibility
        /// that they will exist from the row, column and box where they are.
        /// </summary>
        /// <param name="board"> The board on which the function will run </param>
        public static void FullCellsCleanUp(Board board)
        {

        }

        /// <summary>
        /// Goes through all the empty cells and searches for each row column and box
        /// if there is a cell that has a number that the other cells in the row/column/box do not have.
        /// and if so then it adds the cell to the full cells.
        /// </summary>
        /// <param name="board">The board on which the function will run</param>
        /// <returns>Returns true if a cell has been added to the filled cells</returns>
        public static bool HiddenSingle(Board board)
        {
            return false;
        }

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

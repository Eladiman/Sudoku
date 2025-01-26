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
            int lastFullCellIndex = board.GetLastFullCellIndex();
            for (; lastFullCellIndex < board.FullCellsSize(); lastFullCellIndex++)
            {
                UpdateTile(board,board.GetFullCellCoordinate(lastFullCellIndex));
            }
            board.SetLastFullCellIndex(lastFullCellIndex);
        }

        private static void UpdateTile(Board board, Coordinate coordinate)
        {
            UpdateRow(board, coordinate);
            UpdateCol(board, coordinate);
            UpdateBox(board, coordinate);
        }
        /// <summary>
        ///  Goes through all the cells in the box of the position it received.
        ///  and Removes the number of the cell in this position from the entire box
        /// </summary>
        /// <param name="board"></param>
        /// <param name="coordinate"></param>
        private static void UpdateBox(Board board, Coordinate coordinate)
        {
            int numberToDelete = board.GetTile(coordinate).GetCurrentNumber();
            int startOfBoxRow = ((coordinate.X) / SudokuConstants.Sqrt_Board_size) * SudokuConstants.Sqrt_Board_size;
            int startOfBoxCol = ((coordinate.Y) / SudokuConstants.Sqrt_Board_size) * SudokuConstants.Sqrt_Board_size;
            int row = startOfBoxRow;
            int col = startOfBoxCol;

            for (; row < startOfBoxRow + SudokuConstants.Sqrt_Board_size; row++)
            {
                col = startOfBoxCol;
                for (; col < SudokuConstants.Sqrt_Board_size + startOfBoxCol; col++)
                {
                    if (!(col == coordinate.Y && row == coordinate.X))
                    {
                        board.RemoveNumber(row, col, numberToDelete);
                    }
                }
            }
        }
        /// <summary>
        ///  Goes through all the cells in the column of the position it received.
        ///  and Removes the number of the cell in this position from the entire column
        /// </summary>
        /// <param name="board"></param>
        /// <param name="coordinate"></param>
        private static void UpdateCol(Board board, Coordinate coordinate)
        {
            int numberToDelete = board.GetTile(coordinate).GetCurrentNumber();

            for (int col = 0; col < SudokuConstants.Board_size; col++)
            {
                if(col!=coordinate.Y)
                {
                    board.RemoveNumber(coordinate.X, col,numberToDelete);
                }
            }
        }
        /// <summary>
        ///  Goes through all the cells in the row of the position it received.
        ///  and Removes the number of the cell in this position from the entire row
        /// </summary>
        /// <param name="board"></param>
        /// <param name="coordinate"></param>
        private static void UpdateRow(Board board, Coordinate coordinate)
        {
            int numberToDelete = board.GetTile(coordinate).GetCurrentNumber();

            for (int row = 0; row < SudokuConstants.Board_size; row++)
            {
                if (row != coordinate.X)
                {
                    board.RemoveNumber(row, coordinate.Y, numberToDelete);
                }
            }
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
            ITile currentTile;
            foreach(Coordinate coordinate in board.GetEmptyCells())
            {
                currentTile = board.GetTile(coordinate);
                if (HiddenSingleRow(board, currentTile) || HiddenSingleCol(board, currentTile) || HiddenSingleBox(board, currentTile))
                {
                    return true;
                }  
            }
            return false;
        }

        private static bool HiddenSingleBox(Board board, ITile currentTile)
        {
            int startOfBoxRow = ((currentTile.GetCoordinate().X) / SudokuConstants.Sqrt_Board_size) * SudokuConstants.Sqrt_Board_size;
            int startOfBoxCol = ((currentTile.GetCoordinate().Y) / SudokuConstants.Sqrt_Board_size) * SudokuConstants.Sqrt_Board_size;
            int row = startOfBoxRow;
            int col = startOfBoxCol;
            foreach (int possibility in currentTile.GetAvailableNumbers())
            {
                row = startOfBoxRow;
                bool found = false;
                for (; row < startOfBoxRow + SudokuConstants.Sqrt_Board_size; row++)
                {
                    col = startOfBoxCol;
                    for (; col < SudokuConstants.Sqrt_Board_size + startOfBoxCol; col++)
                    {
                        if (!(col == currentTile.GetCoordinate().Y && row == currentTile.GetCoordinate().X))
                        {
                            if(board.GetTile(row, col).GetCurrentNumber() == 0
                                && board.GetTile(row, col).ContainNumber(possibility))
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                    if(found)break;
                }
                if (!found)
                {
                    HiddenSingleSuccesses(board, currentTile, possibility);
                    return true;
                }
            }
            return false;
        }

        private static bool HiddenSingleCol(Board board, ITile currentTile)
        {
            int row = 0;
            foreach (int possibility in currentTile.GetAvailableNumbers())
            {
                bool found = false;
                for (row = 0; row < SudokuConstants.Board_size; row++)
                {
                    if (row!= currentTile.GetCoordinate().X 
                        && board.GetTile(row,currentTile.GetCoordinate().Y).GetCurrentNumber() == 0
                        && board.GetTile(row,currentTile.GetCoordinate().Y).ContainNumber(possibility))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    HiddenSingleSuccesses(board, currentTile, possibility);
                    return true;
                }
            }
            return false;
        }

        private static bool HiddenSingleRow(Board board, ITile currentTile)
        {
            int col = 0;
            foreach (int possibility  in currentTile.GetAvailableNumbers())
            {
                bool found = false;
                for(col = 0;col<SudokuConstants.Board_size;col++)
                {
                    if(col!= currentTile.GetCoordinate().Y
                        && board.GetTile(currentTile.GetCoordinate().X,col).GetCurrentNumber() == 0 
                        && board.GetTile(currentTile.GetCoordinate().X, col).ContainNumber(possibility))
                    {
                        found = true; 
                        break;
                    }
                }
                if(!found)
                {
                    HiddenSingleSuccesses(board, currentTile,possibility);
                    return true;
                }
            }
            return false;
        }

        private static void HiddenSingleSuccesses(Board board, ITile currentTile, int possibility)
        {
            currentTile.UpdateCurrentNumberAndDeletePossibilities(possibility);
            board.AddFullCell(currentTile.GetCoordinate());
            board.RemoveEmptyCell(currentTile.GetCoordinate());
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

using Sudoku.src.Consts;
using Sudoku.src.Entities.Interfaces;
using Sudoku.src.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.Logic.Heuristics
{
    public static class HiddenSingleHeuristics
    {
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
            foreach (Coordinate coordinate in board.GetEmptyCells())
            {
                currentTile = board.GetTile(coordinate);
                if (HiddenSingleRow(board, currentTile) || HiddenSingleCol(board, currentTile) || HiddenSingleBox(board, currentTile))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Implementing the hidden single for each cell in a box. 
        /// Looping through the list of options for an empty cell
        /// and looking for an option that is not repeated in any other empty cell.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="currentTile"></param>
        /// <returns>True if the hidden single row found the searched cell. false otherwise</returns>
        private static bool HiddenSingleBox(Board board, ITile currentTile)
        {
            int startOfBoxRow = currentTile.GetCoordinate().X / SudokuConstants.Sqrt_Board_size * SudokuConstants.Sqrt_Board_size;
            int startOfBoxCol = currentTile.GetCoordinate().Y / SudokuConstants.Sqrt_Board_size * SudokuConstants.Sqrt_Board_size;
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
                            if (board.GetTile(row, col).GetCurrentNumber() == 0
                                && board.GetTile(row, col).ContainNumber(possibility))
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                    if (found) break;
                }
                if (!found)
                {
                    HiddenSingleSuccesses(board, currentTile, possibility);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Implementing the hidden single for each cell in a col. 
        /// Looping through the list of options for an empty cell
        /// and looking for an option that is not repeated in any other empty cell.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="currentTile"></param>
        /// <returns>True if the hidden single row found the searched cell. false otherwise</returns>
        private static bool HiddenSingleCol(Board board, ITile currentTile)
        {
            int row = 0;
            foreach (int possibility in currentTile.GetAvailableNumbers())
            {
                bool found = false;
                for (row = 0; row < SudokuConstants.Board_size; row++)
                {
                    if (row != currentTile.GetCoordinate().X
                        && board.GetTile(row, currentTile.GetCoordinate().Y).GetCurrentNumber() == 0
                        && board.GetTile(row, currentTile.GetCoordinate().Y).ContainNumber(possibility))
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
        /// <summary>
        /// Implementing the hidden single for each cell in a row. 
        /// Looping through the list of options for an empty cell
        /// and looking for an option that is not repeated in any other empty cell.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="currentTile"></param>
        /// <returns>True if the hidden single row found the searched cell. false otherwise</returns>
        private static bool HiddenSingleRow(Board board, ITile currentTile)
        {
            int col = 0;
            foreach (int possibility in currentTile.GetAvailableNumbers())
            {
                bool found = false;
                for (col = 0; col < SudokuConstants.Board_size; col++)
                {
                    if (col != currentTile.GetCoordinate().Y
                        && board.GetTile(currentTile.GetCoordinate().X, col).GetCurrentNumber() == 0
                        && board.GetTile(currentTile.GetCoordinate().X, col).ContainNumber(possibility))
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

        /// <summary>
        /// Called when a cell is found by the hidden single.
        /// and adds it to the list of full cells and deletes it from the list of empty cells.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="currentTile">The tile to update</param>
        /// <param name="possibility">the right possibility</param>
        private static void HiddenSingleSuccesses(Board board, ITile currentTile, int possibility)
        {
            currentTile.UpdateCurrentNumberAndDeletePossibilities(possibility);
            board.AddFullCell(currentTile.GetCoordinate());
            board.RemoveEmptyCell(currentTile.GetCoordinate());
        }
    }
}

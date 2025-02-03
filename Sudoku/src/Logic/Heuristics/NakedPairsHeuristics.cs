using Sudoku.src.Consts;
using Sudoku.src.Entities.Exceptions;
using Sudoku.src.Entities.Interfaces;
using Sudoku.src.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.Logic.Heuristics
{
    public static class NakedPairsHeuristics
    {
        private static int WANTED_SIZE = 2;
        /// <summary>
        /// Goes through all the empty cells and if it detects two cells with the same 2 options
        /// then Removes these options from the row/column/box where they were found
        /// </summary>
        /// <param name="board">The board on which the function will run</param>
        public static bool NakedPairs(Board board)
        {
            bool flag = false;
            if(NakedPairsRows(board)) flag = true;
            if(NakedPairsCols(board)) flag |= true;
            if(NakedPairsCols(board)) flag |= true;
            return flag;
        }

        private static bool NakedPairsBox(Board board)
        {
            List<ITile> emptyCellsInGivenBox;
            bool has_added = false;
            for (int row = 0; row < SudokuConstants.Sqrt_Board_size; row++)
            {
                for(int col = 0;col<SudokuConstants.Sqrt_Board_size;col++)
                {
                    emptyCellsInGivenBox = board.GetEmptyCellsBox(row* SudokuConstants.Sqrt_Board_size, col* SudokuConstants.Sqrt_Board_size);
                    if (NakedPairsInSingleIteration(board, emptyCellsInGivenBox)) has_added = true;
                }
            }
            return has_added;
        }

        private static bool NakedPairsCols(Board board)
        {
            List<ITile> emptyCellsInGivenCol;
            bool has_added = false;
            for (int col = 0; col < SudokuConstants.Board_size; col++)
            {
                emptyCellsInGivenCol = board.GetEmptyCellsCol(col);
                if (NakedPairsInSingleIteration(board, emptyCellsInGivenCol)) has_added = true;
            }
            return has_added;
        }

        private static bool NakedPairsRows(Board board)
        {
            List<ITile> emptyCellsInGivenRow;
            bool has_added = false;
            for (int row = 0; row < SudokuConstants.Board_size; row++)
            {
                emptyCellsInGivenRow = board.GetEmptyCellsRow(row);
                if (NakedPairsInSingleIteration(board, emptyCellsInGivenRow)) has_added =true;
            }
            return has_added;
        }

        private static bool NakedPairsInSingleIteration(Board board, List<ITile> emptyCellsInGivenRow)
        {
            HashSet<ITile> wantedCells = new HashSet<ITile>();

            bool has_change = false;
        
            foreach (ITile cell in emptyCellsInGivenRow)
            {
                if (cell.GetSize() == WANTED_SIZE)
                {
                    
                    if (!wantedCells.TryGetValue(cell,out ITile temp)) wantedCells.Add(cell);
                    else
                    {
                        if (RemovePossibilities(cell, temp, emptyCellsInGivenRow,board)) has_change= true;
                    }
                }
            }
            return has_change;
        }

        private static bool RemovePossibilities(ITile cell, ITile temp, List<ITile> emptyCellsInGivenRow,Board board)
        {
            bool found = false;
            foreach(ITile cellToDelete in emptyCellsInGivenRow)
            {
                if((!cellToDelete.GetCoordinate().Equals(temp.GetCoordinate())) && (!cellToDelete.GetCoordinate().Equals(cell.GetCoordinate())))
                {
                    foreach(int possibility in cell.GetAvailableNumbers())
                    {
                        cellToDelete.RemoveAvailableNumber(possibility);
                        if (cellToDelete.GetSize() == 0) throw new LogicalException();
                    }
                    if(cellToDelete.GetSize() == 1)
                    {
                        cellToDelete.UpdateCurrentNumber();
                        found = true;
                        board.AddFullCell(cellToDelete.GetCoordinate());
                        board.RemoveEmptyCell(cellToDelete.GetCoordinate());
                    }

                }
            }
            return found;
        }
    }
}

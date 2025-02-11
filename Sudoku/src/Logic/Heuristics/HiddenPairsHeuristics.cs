using Sudoku.src.Consts;
using Sudoku.src.Entities.Exceptions;
using Sudoku.src.Entities.Interfaces;
using Sudoku.src.Entities.Models;

namespace Sudoku.src.Logic.Heuristics
{
    public static class HiddenPairsHeuristics
    {
        private static int _wantedSize = 2;
        /// <summary>
        /// Goes through all the empty cells and if it detects two cells with the same 2 options
        /// then Removes these options from the row/column/box where they were found
        /// </summary>
        /// <param name="board">The board on which the function will run</param>
        public static void HiddenPairs(Board board)
        {
            HiddenPairsRows(board);
            HiddenPairsCols(board);
            HiddenPairsBoxes(board);
        }

        private static void HiddenPairsBoxes(Board board)
        {
            List<ITile> emptyCellsInGivenBox;
            for (int row = 0; row < SudokuConstants.Sqrt_Board_size; row++)
            {
                for (int col = 0; col < SudokuConstants.Sqrt_Board_size; col++)
                {
                    emptyCellsInGivenBox = board.GetEmptyCellsBox(row * SudokuConstants.Sqrt_Board_size, col * SudokuConstants.Sqrt_Board_size);
                    HiddenPairsInSingleIteration(board, emptyCellsInGivenBox);
                }
            }
        }

        /// <summary>
        /// Identifies hidden pairs in each column and removes unnecessary candidates.
        /// </summary>
        /// <param name="board">The Sudoku board to analyze.</param>
        private static void HiddenPairsCols(Board board)
        {
            List<ITile> emptyCellsInGivenCol;
            for (int col = 0; col < SudokuConstants.Board_size; col++)
            {
                emptyCellsInGivenCol = board.GetEmptyCellsCol(col);
                HiddenPairsInSingleIteration(board, emptyCellsInGivenCol);
            }
        }

        /// <summary>
        /// Identifies hidden pairs in each row and removes unnecessary candidates.
        /// </summary>
        /// <param name="board">The Sudoku board to analyze.</param>
        private static void HiddenPairsRows(Board board)
        {
            List<ITile> emptyCellsInGivenRow;
            for (int row = 0; row < SudokuConstants.Board_size; row++)
            {
                emptyCellsInGivenRow = board.GetEmptyCellsRow(row);
                HiddenPairsInSingleIteration(board, emptyCellsInGivenRow);
            }
        }

        /// <summary>
        /// Detects hidden pairs in a given row, column, or box and eliminates unnecessary candidates.
        /// if it find hidden pair that can be in more then 2 different tiles then it will rise an exception
        /// </summary>
        /// <param name="board">The Sudoku board to analyze.</param>
        /// <param name="emptyCells">A list of empty cells within a row, column, or box.</param>
        /// <exception cref="LogicalException">
        /// example:
        /// if the numbers 1,2 can only be in the first 3 tiles in a give row
        /// then there is a logical problem.
        /// </exception>
        private static void HiddenPairsInSingleIteration(Board board, List<ITile> emptyCells)
        {
            List<ITile>[] possibilityArray = new List<ITile>[Consts.SudokuConstants.Board_size];

            foreach (var tile in emptyCells)
            {
                AddToPossibilityArray(possibilityArray, tile);
            }

            for (int number = 0; number < possibilityArray.Length - 1; number++)
            {
                bool found = false;
                if (possibilityArray[number] != null && possibilityArray[number].Count == _wantedSize)
                {
                    int j = number + 1;
                    int place = j;
                    for (; j < possibilityArray.Length; j++)
                    {
                        if (IsListsEquals(possibilityArray, number, j))
                        {
                            if (!found)
                            {
                                found = true;
                                place = j;
                            }
                            else throw new LogicalException();
                        }
                    }
                    if (found) RemovePossibilitiesFromTiles(possibilityArray, number, place);
                }
            }

        }

        /// <summary>
        /// Removes irrelevant candidates from the detected hidden pairs.
        /// </summary>
        /// <param name="possibilityArray">The array of lists</param>
        /// <param name="index1">List index</param>
        /// <param name="index2">list index</param>
        private static void RemovePossibilitiesFromTiles(List<ITile>[] possibilityArray, int index1, int index2)
        {
            foreach (var tile in possibilityArray[index1])
            {
                foreach (var possibility in tile.GetAvailableNumbers())
                {
                    if (possibility != (index1 + 1) && possibility != (index2 + 1)) tile.RemoveAvailableNumber(possibility);
                }
            }

        }

        /// <summary>
        /// Checks if two lists of tiles contain the same elements.
        /// </summary>
        /// <param name="possibilityArray">The array of lists</param>
        /// <param name="index1">List index</param>
        /// <param name="index2">list index</param>
        /// <returns>true if lists are equals</returns>
        private static bool IsListsEquals(List<ITile>[] possibilityArray, int index1, int index2)
        {
            if (possibilityArray[index2] == null) return false;

            if (possibilityArray[index1].Count != possibilityArray[index2].Count) return false;

            foreach (var tile in possibilityArray[index1])
            {
                if (!IsListContain(possibilityArray[index2], tile)) return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if a list contains a specific tile.(by compering coordinates)
        /// </summary>
        /// <param name="tiles">A given list</param>
        /// <param name="tile">A given tile</param>
        /// <returns>true if in list</returns>
        private static bool IsListContain(List<ITile> tiles, ITile tile)
        {
            foreach (var tempTile in tiles) if (tempTile.GetCoordinate().Equals(tile.GetCoordinate())) return true;
            return false;
        }

        /// <summary>
        /// Goes through every possibility of a given tile and adds the tile to the corrects lists.
        /// </summary>
        /// <param name="possibilityArray">The lists array</param>
        /// <param name="tile">A given tile</param>
        private static void AddToPossibilityArray(List<ITile>[] possibilityArray, ITile tile)
        {
            foreach (var possibility in tile.GetAvailableNumbers())
            {
                if (possibilityArray[possibility - 1] == null) possibilityArray[possibility - 1] = new List<ITile>(); //if list is null create new list
                possibilityArray[possibility - 1].Add(tile);
            }
        }

    }
}

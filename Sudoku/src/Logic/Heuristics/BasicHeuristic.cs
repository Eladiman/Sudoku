using Sudoku.src.Consts;
using Sudoku.src.Entities.Models;

namespace Sudoku.src.Logic.Heuristics
{
    /// <summary>
    /// The following class responsible for Run a heuristic that
    /// Goes through all the filled cells and Removes the possibility
    /// that they will exist from the row, column and box where they are.
    /// </summary>
    public static class BasicHeuristic
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
                UpdateTile(board, board.GetFullCellCoordinate(lastFullCellIndex));
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
            int startOfBoxRow = coordinate.X / SudokuConstants.SqrtBoardSize * SudokuConstants.SqrtBoardSize;
            int startOfBoxCol = coordinate.Y / SudokuConstants.SqrtBoardSize * SudokuConstants.SqrtBoardSize;
            int row = startOfBoxRow;
            int col = startOfBoxCol;

            for (; row < startOfBoxRow + SudokuConstants.SqrtBoardSize; row++)
            {
                col = startOfBoxCol;
                for (; col < SudokuConstants.SqrtBoardSize + startOfBoxCol; col++)
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

            for (int col = 0; col < SudokuConstants.BoardSize; col++)
            {
                if (col != coordinate.Y)
                {
                    board.RemoveNumber(coordinate.X, col, numberToDelete);
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

            for (int row = 0; row < SudokuConstants.BoardSize; row++)
            {
                if (row != coordinate.X)
                {
                    board.RemoveNumber(row, coordinate.Y, numberToDelete);
                }
            }
        }
    }
}

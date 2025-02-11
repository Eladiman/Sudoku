
using Sudoku.src.Consts;
using Sudoku.src.Entities.Exceptions;
using Sudoku.src.Entities.Interfaces;
using System.Text;

namespace Sudoku.src.Entities.Models
{
    /// <summary>
    /// The following class responsible for representing a board and its methods
    /// </summary>
    public class Board
    {
        private Coordinate _currentTile;

        private List<Coordinate> _fullCells;

        private int _lastFullCellIndex;

        private List<Coordinate> _emptyCells;

        private ITile[,] _board;

        public Board(string expression)
        {
            SudokuConstants.BoardSize = (int)Math.Sqrt(expression.Length);

            _board = new Tile[SudokuConstants.BoardSize, SudokuConstants.BoardSize];

            _currentTile = new Coordinate();

            _fullCells = new List<Coordinate>();

            _lastFullCellIndex = 0;

            _emptyCells = new List<Coordinate>();

            InitializeBoard(expression);

        }
        /// <summary>
        /// Initialize the board from a given string
        /// </summary>
        /// <param name="expression"></param>
        private void InitializeBoard(string expression)
        {
            int index = 0;
            int currentNumber = 0;
            Coordinate currentCoordinate;
            for (int row = 0; row < SudokuConstants.BoardSize; row++)
            {
                for (int col = 0; col < SudokuConstants.BoardSize; col++)
                {
                    currentNumber = expression[index] - SudokuConstants.AsciiDiff;
                    currentCoordinate = new Coordinate(row, col);
                    _board[row, col] = new Tile(currentNumber, currentCoordinate);
                    if (currentNumber != 0) _fullCells.Add(currentCoordinate);//if full add to full cells list
                    else _emptyCells.Add(currentCoordinate);//if empty add to empty cells list
                    index++;
                }
            }
        }
        /// <summary>
        /// add to full cells list from a given coordinate
        /// </summary>
        /// <param name="coordinate"></param>
        public void AddFullCell(Coordinate coordinate)
        {
            _fullCells.Add(coordinate);
        }

        /// <summary>
        /// Remove empty cell from the empty cells list from a given coordinate
        /// </summary>
        /// <param name="coordinate"></param>
        public void RemoveEmptyCell(Coordinate coordinate)
        {
            _emptyCells.Remove(coordinate);
        }

        /// <summary>
        /// Gets the index before recursion and deletes all elements up to it.
        /// Delete the last full cells 
        /// </summary>
        /// <param name="lastIndex"></param>
        public void RestoreFullCells(int lastIndex)
        {
            int index = _fullCells.Count;
            for (; index > lastIndex; index--)
            {
                _fullCells.RemoveAt(index - 1);
            }
            _lastFullCellIndex = lastIndex;//update the last full cell index to the lastIndex
        }

        /// <summary>
        /// return the last full cell index in the full cells list
        /// </summary>
        /// <returns></returns>
        public int GetLastFullCellIndex() { return _lastFullCellIndex; }

        /// <summary>
        /// Checks if board is full
        /// </summary>
        /// <returns> True if full, false otherwise</returns>
        public bool IsBoardFull()
        {
            return _fullCells.Count == SudokuConstants.BoardSize * SudokuConstants.BoardSize;
        }

        /// <summary>
        /// Goes through all empty cells and returns the cell with the fewest options.
        /// </summary>
        /// <returns> The cell with the fewest options, null if board is full</returns>
        public ITile GetSmallestTile()
        {
            ITile minTile = null;
            int minCount = SudokuConstants.BoardSize;
            foreach (Coordinate coordinate in _emptyCells)
            {
                if (_board[coordinate.X, coordinate.Y].GetSize() <= minCount)
                {
                    minCount = _board[coordinate.X, coordinate.Y].GetSize();
                    minTile = _board[coordinate.X, coordinate.Y];
                }
            }
            return minTile;
        }

        /// <summary>
        /// Goes through all empty cells in the board
        /// and generates a dictionary of their locations and their possible values
        /// </summary>
        /// <returns>dictionary of all empty cells locations and their possible values</returns>
        public Dictionary<Coordinate, HashSet<int>> SaveBoardState()
        {
            Dictionary<Coordinate, HashSet<int>> savedCoordinates = new Dictionary<Coordinate, HashSet<int>>(_emptyCells.Count);
            foreach (Coordinate cell in _emptyCells)
            {
                savedCoordinates.Add(cell, _board[cell.X, cell.Y].GetAvailableNumbers());
            }
            return savedCoordinates;
        }

        /// <summary>
        /// Gets a dictionary of empty cells and the options they contained and initializes the board accordingly.
        /// </summary>
        /// <param name="boardState"> the wanted board state</param>
        public void RestoreBoardState(Dictionary<Coordinate, HashSet<int>> boardState)
        {
            _emptyCells.Clear();
            foreach (Coordinate restoredTilePlace in boardState.Keys)
            {
                _board[restoredTilePlace.X, restoredTilePlace.Y].SetCurrentNumber(0);
                _board[restoredTilePlace.X, restoredTilePlace.Y].SetAvailableNumbers(boardState[restoredTilePlace]);
                _emptyCells.Add(restoredTilePlace);
            }
        }

        /// <summary>
        /// This function returns a String representation of the board . 
        /// When the board size is larger than 9X9 
        /// then the numbers will be displayed according to the following characters in the ASCII table
        /// </summary>
        /// <returns>String representation of the board</returns>
        public override System.String ToString()
        {
            int MaxDigits = 1;
            StringBuilder sb = new StringBuilder();
            for (int Y = 0; Y < SudokuConstants.BoardSize; ++Y)
            {
                if (Y % SudokuConstants.SqrtBoardSize == 0)
                {
                    sb.Append('-', (SudokuConstants.BoardSize + SudokuConstants.SqrtBoardSize) * (MaxDigits + 1) + 1);
                    sb.Append('\n', 1);
                }

                for (int X = 0; X < SudokuConstants.BoardSize; ++X)
                {
                    if (X % SudokuConstants.SqrtBoardSize == 0)
                    {
                        sb.Append('|', 1);
                        sb.Append(' ', MaxDigits);
                    }
                    int CellValue = _board[Y, X].GetCurrentNumber() + SudokuConstants.AsciiDiff;
                    sb.Append((char)CellValue, 1);
                    sb.Append(' ', 1);
                }

                sb.Append('|', 1);
                sb.Append(' ', MaxDigits);
                sb.Append('\n', 1);
            }

            sb.Append('-', (SudokuConstants.BoardSize + SudokuConstants.SqrtBoardSize) * (MaxDigits + 1) + 1);
            sb.Append('\n', 1);

            return sb.ToString();
        }

        public void ReplaceTile(ITile tile)
        {
            _board[tile.GetCoordinate().X, tile.GetCoordinate().Y] = tile;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns> return the number of Full Cells </returns>
        public int FullCellsSize()
        {
            return _fullCells.Count;
        }

        /// <summary>
        /// given an index it will return the coordinate of the full cell
        /// from the FullCells list
        /// </summary>
        /// <param name="lastFullCellIndex"></param>
        /// <returns></returns>
        public Coordinate GetFullCellCoordinate(int lastFullCellIndex)
        {
            return _fullCells[lastFullCellIndex];
        }

        /// <summary>
        /// updates lastFullCellIndex to the given parameter
        /// </summary>
        /// <param name="lastFullCellIndex1"></param>
        public void SetLastFullCellIndex(int lastFullCellIndex1)
        {
            this._lastFullCellIndex = lastFullCellIndex1;
        }

        /// <summary>
        /// return the Tile from the specific coordinate
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public ITile GetTile(Coordinate coordinate)
        {
            return _board[coordinate.X, coordinate.Y];
        }
        /// <summary>
        /// return Tile from a specific coordinate (represented by row and col)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public ITile GetTile(int x, int y)
        {
            return _board[x, y];
        }

        /// <summary>
        /// Gets location and number. 
        /// Assuming the Tile is empty then the number will be deleted from its options. 
        /// If the Tile left with no possibilities after that then an exception will be thrown. 
        /// And if there is only one option after deletion, then the cell becomes its option.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="number"></param>
        /// <exception cref="LogicalException"> board is not solvable </exception>
        public void RemoveNumber(int x, int y, int number)
        {
            _board[x, y].RemoveAvailableNumber(number);
            if (_board[x, y].GetSize() == 0) throw new LogicalException();
            if (_board[x, y].GetCurrentNumber() == 0 && _board[x, y].GetSize() == 1) //if 1 option left add the cell to the full cells list
            {
                _board[x, y].UpdateCurrentNumber(); // make the cell full by making him his left option
                _fullCells.Add(_board[x, y].GetCoordinate());
                _emptyCells.Remove(_board[x, y].GetCoordinate());
            }
        }
        /// <summary>
        /// returns the list of empty cells
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object> GetEmptyCells()
        {
            return _emptyCells;
        }

        /// <summary>
        /// return list of all the empty cells in a given row
        /// </summary>
        /// <param name="row">the row to get the list from</param>
        /// <returns>list of all the empty cells in a given row</returns>
        public List<ITile> GetEmptyCellsRow(int row)
        {
            List<Coordinate> emptyRowCellsCoordinates = _emptyCells.Where(coordinate => coordinate.X == row).ToList();
            List<ITile> emptyRowCells = new List<ITile>();
            foreach (Coordinate coord in emptyRowCellsCoordinates)
            {
                emptyRowCells.Add(GetTile(coord));
            }
            return emptyRowCells;
        }
        /// <summary>
        /// return list of all the empty cells in a given column
        /// </summary>
        /// <param name="col">the column to get the list from</param>
        /// <returns>list of all the empty cells in a given column</returns>
        public List<ITile> GetEmptyCellsCol(int col)
        {
            List<Coordinate> emptyColCellsCoordinates = _emptyCells.Where(coordinate => coordinate.Y == col).ToList();
            List<ITile> emptyColCells = new List<ITile>();
            foreach (Coordinate coord in emptyColCellsCoordinates)
            {
                emptyColCells.Add(GetTile(coord));
            }
            return emptyColCells;
        }
        /// <summary>
        /// return list of all the empty cells in a given box
        /// </summary>
        /// <param name="row">the starting index of the box row</param>
        /// <param name="col">the starting index of the box col</param>
        /// <returns>list of all the empty cells in a given box</returns>
        public List<ITile> GetEmptyCellsBox(int row, int col)
        {
            List<Coordinate> emptyColCellsCoordinates = _emptyCells.Where(coordinate => coordinate.X < row + SudokuConstants.SqrtBoardSize && coordinate.X >= row && coordinate.Y < col + SudokuConstants.SqrtBoardSize && coordinate.Y >= col).ToList();
            List<ITile> emptyBoxCells = new List<ITile>();
            foreach (Coordinate coord in emptyColCellsCoordinates)
            {
                emptyBoxCells.Add(GetTile(coord));
            }
            return emptyBoxCells;
        }

        public string GetString()
        {
            StringBuilder sb = new StringBuilder();
            int currentNumber = 0;
            for (int row = 0; row < SudokuConstants.BoardSize; row++)
            {
                for (int col = 0; col < SudokuConstants.BoardSize; col++)
                {
                    currentNumber = _board[row, col].GetCurrentNumber() + SudokuConstants.AsciiDiff;
                    sb.Append((char)currentNumber,1);
                }
            }
            return sb.ToString();
        }
    }
}

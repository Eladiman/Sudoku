
using Sudoku.src.Consts;
using Sudoku.src.Entities.Exceptions;
using Sudoku.src.Entities.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.Entities.Models
{
    public class Board
    {
        private Coordinate currentTile;

        private List<Coordinate> fullCells;

        private int lastFullCellIndex;

        private List<Coordinate> emptyCells;

        private ITile[,] board;

        public Board(string expression)
        {
            board = new Tile[SudokuConstants.Board_size, SudokuConstants.Board_size];

            currentTile = new Coordinate();

            fullCells = new List<Coordinate>();

            lastFullCellIndex = 0;

            emptyCells = new List<Coordinate>();

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
            for (int row = 0; row < SudokuConstants.Board_size; row++)
            {
                for (int col = 0; col < SudokuConstants.Board_size; col++)
                {
                    currentNumber = expression[index] - SudokuConstants.ASCII_DIFF;
                    currentCoordinate = new Coordinate(row, col);
                    board[row, col] = new Tile(currentNumber, currentCoordinate);
                    if (currentNumber!=0) fullCells.Add(currentCoordinate);//if full add to full cells list
                    else emptyCells.Add(currentCoordinate);//if empty add to empty cells list
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
            fullCells.Add(coordinate);
        }

        /// <summary>
        /// Remove empty cell from the empty cells list from a given coordinate
        /// </summary>
        /// <param name="coordinate"></param>
        public void RemoveEmptyCell(Coordinate coordinate)
        {
            emptyCells.Remove(coordinate);
        }

        /// <summary>
        /// Gets the index before recursion and deletes all elements up to it.
        /// Delete the last full cells 
        /// </summary>
        /// <param name="lastIndex"></param>
        public void RestoreFullCells(int lastIndex)
        {
            int index = fullCells.Count;
            for(;index>lastIndex;index--)
            {
                fullCells.RemoveAt(index - 1);
            }
            lastFullCellIndex = lastIndex;//update the last full cell index to the lastIndex
        }

        /// <summary>
        /// return the last full cell index in the full cells list
        /// </summary>
        /// <returns></returns>
        public int GetLastFullCellIndex() { return lastFullCellIndex; }

        /// <summary>
        /// Checks if board is full
        /// </summary>
        /// <returns> True if full, false otherwise</returns>
        public bool IsBoardFull()
        {
            return fullCells.Count == SudokuConstants.Board_size * SudokuConstants.Board_size;  
        }

        /// <summary>
        /// Goes through all empty cells and returns the cell with the fewest options.
        /// </summary>
        /// <returns> The cell with the fewest options, null if board is full</returns>
        public ITile GetSmallestTile()
        {
            ITile minTile = null;
            int minCount = SudokuConstants.Board_size;
            foreach(Coordinate coordinate in emptyCells) 
            {
                if (board[coordinate.X,coordinate.Y].GetSize() <= minCount)
                {
                    minCount = board[coordinate.X, coordinate.Y].GetSize();
                    minTile = board[coordinate.X, coordinate.Y];
                }
            }
            return minTile;
        }

        /// <summary>
        /// Goes through all empty cells in the board
        /// and generates a dictionary of their locations and their possible values
        /// </summary>
        /// <returns>dictionary of all empty cells locations and their possible values</returns>
        public Dictionary<Coordinate,HashSet<int>> SaveBoardState()
        {
            Dictionary<Coordinate, HashSet<int>> savedCoordinates = new Dictionary<Coordinate, HashSet<int>>(emptyCells.Count);
            foreach (Coordinate cell in emptyCells)
            {
                savedCoordinates.Add(cell, board[cell.X,cell.Y].GetAvailableNumbers());
            }
            return savedCoordinates;
        }

        /// <summary>
        /// Gets a dictionary of empty cells and the options they contained and initializes the board accordingly.
        /// </summary>
        /// <param name="boardState"> the wanted board state</param>
        public void RestoreBoardState(Dictionary<Coordinate, HashSet<int>> boardState)
        {
            emptyCells.Clear();
            foreach(Coordinate restoredTilePlace in boardState.Keys)
            {
                board[restoredTilePlace.X, restoredTilePlace.Y].SetCurrentNumber(0);
                board[restoredTilePlace.X, restoredTilePlace.Y].SetAvailableNumbers(boardState[restoredTilePlace]);
                emptyCells.Add(restoredTilePlace);
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
            for (int Y = 0; Y < SudokuConstants.Board_size; ++Y)
            {
                if (Y % SudokuConstants.Sqrt_Board_size == 0)
                {
                    sb.Append('-', (SudokuConstants.Board_size + SudokuConstants.Sqrt_Board_size) * (MaxDigits + 1) + 1);
                    sb.Append('\n',1);
                }

                for (int X = 0; X < SudokuConstants.Board_size; ++X)
                {
                    if (X % SudokuConstants.Sqrt_Board_size == 0)
                    {
                        sb.Append('|',1);
                        sb.Append(' ',MaxDigits);
                    }
                    int CellValue = board[Y,X].GetCurrentNumber() + SudokuConstants.ASCII_DIFF;
                    sb.Append((char)CellValue,1);
                    sb.Append(' ',1);
                }

                sb.Append('|', 1);
                sb.Append(' ', MaxDigits);
                sb.Append('\n', 1);
            }

            sb.Append('-', (SudokuConstants.Board_size + SudokuConstants.Sqrt_Board_size) * (MaxDigits + 1) + 1);
            sb.Append('\n', 1);

            return sb.ToString();
        }

        public void ReplaceTile(ITile tile)
        {
            board[tile.GetCoordinate().X, tile.GetCoordinate().Y] = tile;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns> return the number of Full Cells </returns>
        public int FullCellsSize()
        {
            return fullCells.Count;
        }

        /// <summary>
        /// given an index it will return the coordinate of the full cell
        /// from the FullCells list
        /// </summary>
        /// <param name="lastFullCellIndex"></param>
        /// <returns></returns>
        public Coordinate GetFullCellCoordinate(int lastFullCellIndex)
        {
            return fullCells[lastFullCellIndex];
        }

        /// <summary>
        /// updates lastFullCellIndex to the given parameter
        /// </summary>
        /// <param name="lastFullCellIndex1"></param>
        public void SetLastFullCellIndex(int lastFullCellIndex1)
        {
            this.lastFullCellIndex = lastFullCellIndex1;
        }

        /// <summary>
        /// return the Tile from the specific coordinate
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public ITile GetTile(Coordinate coordinate)
        {
            return board[coordinate.X, coordinate.Y];
        }
        /// <summary>
        /// return Tile from a specific coordinate (represented by row and col)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public ITile GetTile(int x,int y)
        {
            return board[x,y];
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
            board[x,y].RemoveAvailableNumber(number);
            if (board[x, y].GetSize() == 0) throw new LogicalException();
            if (board[x,y].GetCurrentNumber() == 0 && board[x, y].GetSize() == 1) //if 1 option left add the cell to the full cells list
            {
                board[x, y].UpdateCurrentNumber(); // make the cell full by making him his left option
                fullCells.Add(board[x, y].GetCoordinate());
                emptyCells.Remove(board[x, y].GetCoordinate());
            }
        }
        /// <summary>
        /// returns the list of empty cells
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object> GetEmptyCells()
        {
            return emptyCells;
        }
    }
}

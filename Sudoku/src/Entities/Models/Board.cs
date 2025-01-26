
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

        private void InitializeBoard(string expression)
        {
            int index = 0;
            int currentNumber = 0;
            Coordinate currentCoordinate;
            for (int row = 0; row < SudokuConstants.Board_size; row++)
            {
                for (int col = 0; col < SudokuConstants.Board_size; col++)
                {
                    currentNumber = expression[index] - '0';
                    currentCoordinate = new Coordinate(row, col);
                    board[row, col] = new Tile(currentNumber, currentCoordinate);
                    if (currentNumber!=0) fullCells.Add(currentCoordinate);
                    else emptyCells.Add(currentCoordinate);
                    index++;   
                }
            }
        }

        public void AddFullCell(Coordinate coordinate)
        {
            fullCells.Add(coordinate);
        }

        public void RemoveEmptyCell(Coordinate coordinate)
        {
            emptyCells.Remove(coordinate);
        }

        public void RestoreFullCells(int lastIndex)
        {
            int index = fullCells.Count;
            for(;index>lastIndex;index--)
            {
                fullCells.RemoveAt(index - 1);
            }
            lastFullCellIndex = lastIndex;
        }

        public int GetLastFullCellIndex() { return lastFullCellIndex; }

        public bool IsBoardFull()
        {
            return fullCells.Count == SudokuConstants.Board_size * SudokuConstants.Board_size;  
        }

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

        public Dictionary<Coordinate,HashSet<int>> SaveBoardState()
        {
            Dictionary<Coordinate, HashSet<int>> savedCoordinates = new Dictionary<Coordinate, HashSet<int>>(emptyCells.Count);
            foreach (Coordinate cell in emptyCells)
            {
                savedCoordinates.Add(cell, board[cell.X,cell.Y].GetAvailableNumbers());
            }
            return savedCoordinates;
        }

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

        public override System.String ToString()
        {
            int MaxDigits = CountDigits(SudokuConstants.Board_size);
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

                    int CellValue = board[Y,X].GetCurrentNumber();
                    sb.Append(CellValue.ToString());
                    sb.Append(' ',MaxDigits - CountDigits(CellValue) + 1);
                }

                sb.Append('|', 1);
                sb.Append(' ', MaxDigits);
                sb.Append('\n', 1);
            }

            sb.Append('-', (SudokuConstants.Board_size + SudokuConstants.Sqrt_Board_size) * (MaxDigits + 1) + 1);
            sb.Append('\n', 1);

            return sb.ToString();
        }

        private static int CountDigits(int cellValue)
        {
            int count = 1;
            while(cellValue > 10)
            {
                cellValue /= 10;
                count += 1;
            }
            return count;
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
            if (board[x,y].GetCurrentNumber() == 0 && board[x, y].GetSize() == 1)
            {
                board[x, y].UpdateCurrentNumber();
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

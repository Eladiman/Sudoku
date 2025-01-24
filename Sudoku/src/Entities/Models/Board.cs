
using Sudoku.src.Entities.Exceptions;
using Sudoku.src.Entities.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sudoku.src.Entities.Models
{
    public class Board
    {
        private List<(int,int)> fullCells;

        private int lastFullCellIndex;

        private List<(int,int)> emptyCells;

        private ITile[,] board;

        /// <summary>
        /// Create board and initialize the attributes
        /// </summary>
        /// <param name="expression"> expression of the board </param>
        public Board(string expression)
        {
            board = new Tile[Constants.Board_size, Constants.Board_size];

            fullCells = new List<(int,int)>();

            lastFullCellIndex = 0;

            emptyCells = new List<(int,int)>();

            InitializeBoard(expression);

        }

        /// <summary>
        /// Gets an expression and initialize the board
        /// </summary>
        /// <param name="expression"></param>
        private void InitializeBoard(string expression)
        {
            int index = 0;
            int currentNumber = 0;
            for (int row = 0; row < Constants.Board_size; row++)
            {
                for (int col = 0; col < Constants.Board_size; col++)
                {
                    currentNumber = expression[index] - '0';
                    board[row, col] = new Tile(currentNumber, row,col);
                    if (currentNumber!=0) fullCells.Add((row,col));
                    else emptyCells.Add((row,col));
                    index++;   
                }
            }
        }
        /// <summary>
        /// Given an index of a full board cell it will update all the cells in the row and delete the current possibility
        /// </summary>
        /// <param name="x"> The row coordinate </param>
        /// <param name="y"> The col coordinate </param>
        /// <returns> true if there was a change, false otherwise</returns>
        /// <exception cref="LogicalException"></exception>
        public bool UpdateRow(int x, int y)
        {
            bool hasChange = false;

            int number = board[x, y].GetCurrentNumber();
            
            if (number == 0) return false;

            for (int col = 0; col < Constants.Board_size; col++)
            {
                if (col != y && board[x, col].RemoveAvailableNumber(number))
                {
                    if (board[x, col].GetSize() == 0) throw new LogicalException();
                    if (board[x, col].GetSize() == 1)
                    {
                        board[x, col].UpdateCurrentNumber();
                        fullCells.Add((x,col));
                        emptyCells.Remove((x,col));
                    }
                    hasChange = true;
                }
            }
            return hasChange;
        }
        public bool UpdateCol(int x, int y)
        {
            bool hasChange = false;

            int number = board[x,y].GetCurrentNumber();

            if (number == 0) return false;

            for (int row = 0; row < Constants.Board_size; row++)
            {
                if (row != x && board[row, y].RemoveAvailableNumber(number))
                {
                    if (board[row, y].GetSize() == 0) throw new LogicalException();
                    if (board[row, y].GetSize() == 1)
                    {
                        board[row, y].UpdateCurrentNumber();
                        fullCells.Add((row,y));
                        emptyCells.Remove((row, y));
                    }
                    hasChange = true;
                }
            }
            return hasChange;
        }

        public bool UpdateBlock(int x , int y)
        {
            bool hasChange = false;

            int number = board[x,y].GetCurrentNumber();

            if (number == 0) return false;

            int startOfBoxRow = (x / Constants.Sqrt_Board_size) * Constants.Sqrt_Board_size;

            int startOfBoxCol = (y / Constants.Sqrt_Board_size) * Constants.Sqrt_Board_size;

            for(int row = startOfBoxRow; row<Constants.Sqrt_Board_size + startOfBoxRow;row++)
            {
                for(int col = startOfBoxCol; col < Constants.Sqrt_Board_size + startOfBoxCol;col++)
                {
                    if((row !=x || col!= y) && board[row,col].RemoveAvailableNumber(number))
                    {
                        if (board[row,col].GetSize() == 0) throw new LogicalException();
                        if (board[row,col].GetSize() == 1 && board[row,col].GetCurrentNumber() == 0)
                        {
                            board[row,col].UpdateCurrentNumber();
                            fullCells.Add((row,col));
                            emptyCells.Remove((row,col));
                        }
                        hasChange = true;
                    }
                        
                }
            }
            return hasChange;
        }

        public bool UpdateTile(int x, int y)
        {
            bool hasChange = false;
            if (UpdateRow(x,y)) hasChange = true;
            if (UpdateCol(x,y)) hasChange = true;
            if (UpdateBlock(x,y)) hasChange = true;
            return hasChange;
        }

        public bool FullCellsCheck()
        {
            bool hasChange = false;
   
            for(;lastFullCellIndex < fullCells.Count;lastFullCellIndex++)
            {
                (int x, int y) = fullCells[lastFullCellIndex];
                if (UpdateTile(x,y))
                {
                    hasChange = true;
                }
            }
            return hasChange ;
        }

        public void AddFullCell(int x, int y)
        {
            fullCells.Add((x,y));
        }

        public void RemoveEmptyCell(int x, int y)
        {
            emptyCells.Remove((x,y));
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
        //public bool ImplementHiddenSingle(Coordinate coordinate)
        //{
        //    return ImplementHiddenSingleRow(coordinate) || ImplementHiddenSingleCol(coordinate) || ImplementHiddenSingleBlock(coordinate);
        //}

        //private bool ImplementHiddenSingleBlock(Coordinate coordinate)
        //{
        //    bool ValidPossibility = true;
        //    ITile currentTile = board[coordinate.X, coordinate.Y];
        //    if (currentTile.GetCurrentNumber() != 0) return false;
        //    Coordinate startOfBox = findStartOfBox(coordinate);
        //    int startOfBoxRow = startOfBox.X;
        //    int startOfBoxCol = startOfBox.Y;
        //    foreach (int possibility in currentTile.GetAvailableNumbers())
        //    {
        //        ValidPossibility = false;
        //        int row = 0;
        //        int col = 0;

        //        for (; row < Constants.Sqrt_Board_size && ValidPossibility; row++)
        //        {
        //            for (col = 0; col < Constants.Sqrt_Board_size && ValidPossibility ; col++)
        //            {
        //                if (startOfBoxRow + row != coordinate.X
        //                    && startOfBoxCol + col != coordinate.Y
        //                    && board[startOfBoxRow + row, startOfBoxCol + col].ContainNumber(possibility)) ValidPossibility = false;
        //            }
        //        }

        //        if (ValidPossibility)
        //        {
        //            board[startOfBoxRow + row - 1, startOfBoxCol + col - 1].SetCurrentNumber(possibility);
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //private bool ImplementHiddenSingleCol(Coordinate coordinate)
        //{
        //    bool ValidPossibility = true;
        //    ITile currentTile = board[coordinate.X, coordinate.Y];
        //    if (currentTile.GetCurrentNumber() != 0) return false;
        //    foreach (int possibility in currentTile.GetAvailableNumbers())
        //    {
        //        ValidPossibility = false;
        //        int row = 0;
        //        for (; row < Constants.Board_size && ValidPossibility; row++)
        //        {
        //            if (row != coordinate.X && board[row, coordinate.Y].ContainNumber(possibility)) ValidPossibility = false;
        //        }
        //        if (ValidPossibility)
        //        {
        //            board[row - 1, coordinate.Y].SetCurrentNumber(possibility);
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //private bool ImplementHiddenSingleRow(Coordinate coordinate)
        //{
        //    bool ValidPossibility = true;
        //    ITile currentTile = board[coordinate.X, coordinate.Y];
        //    if (currentTile.GetCurrentNumber() != 0) return false;
        //    foreach (int possibility in currentTile.GetAvailableNumbers())
        //    {
        //        ValidPossibility = false;
        //        int col = 0;
        //        for (; col < Constants.Board_size && ValidPossibility; col++)
        //        {
        //            if (col != coordinate.Y && board[coordinate.X, col].ContainNumber(possibility)) ValidPossibility = false;
        //        }
        //        if (ValidPossibility)
        //        {
        //            board[coordinate.X, col -1].SetCurrentNumber(possibility);
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public bool IsBoardFull()
        {
            return fullCells.Count == Constants.Board_size * Constants.Board_size;  
        }

        public ITile GetSmallestTile()
        {
            ITile minTile = null;
            int minCount = Constants.Board_size;
            foreach((int x, int y) in emptyCells) 
            {
                if (board[x,y].GetSize() <= minCount)
                {
                    minCount = board[x, y].GetSize();
                    minTile = board[x, y];
                }
            }
            return minTile;
        }

        public Dictionary<(int, int),HashSet<int>> SaveBoardState()
        {
            Dictionary<(int,int), HashSet<int>> savedCoordinates = new Dictionary<(int,int), HashSet<int>>(emptyCells.Count);
            foreach ((int x, int y) in emptyCells)
            {
                savedCoordinates.Add((x,y), board[x,y].GetAvailableNumbers());
            }
            return savedCoordinates;
        }

        public void RestoreBoardState(Dictionary<(int, int), HashSet<int>> boardState)
        {
            emptyCells.Clear();
            foreach((int x, int y) in boardState.Keys)
            {
                board[x,y].SetCurrentNumber(0);
                board[x,y].SetAvailableNumbers(boardState[(x,y)]);
                emptyCells.Add((x,y));
            }
        }

        public override System.String ToString()
        {
            int MaxDigits = CountDigits(Constants.Board_size);
            StringBuilder sb = new StringBuilder();
            for (int Y = 0; Y < Constants.Board_size; ++Y)
            {
                if (Y % Constants.Sqrt_Board_size == 0)
                {
                    sb.Append('-', (Constants.Board_size + Constants.Sqrt_Board_size) * (MaxDigits + 1) + 1);
                    sb.Append('\n',1);
                }

                for (int X = 0; X < Constants.Board_size; ++X)
                {
                    if (X % Constants.Sqrt_Board_size == 0)
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

            sb.Append('-', (Constants.Board_size + Constants.Sqrt_Board_size) * (MaxDigits + 1) + 1);
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
            board[tile.X, tile.Y] = tile;
        }

    }
}

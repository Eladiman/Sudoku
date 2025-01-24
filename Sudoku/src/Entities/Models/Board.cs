
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
        private Coordinate currentTile;

        private List<Coordinate> fullCells;

        private int lastFullCellIndex;

        private List<Coordinate> emptyCells;

        private ITile[,] board;

        public Board(string expression)
        {
            board = new Tile[Constants.Board_size, Constants.Board_size];

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
            for (int row = 0; row < Constants.Board_size; row++)
            {
                for (int col = 0; col < Constants.Board_size; col++)
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


        public Coordinate CurrentTile
        {
            get { return new Coordinate(currentTile.X, currentTile.Y); }
        }

        public bool UpdateRow(Coordinate coordinate)
        {
            bool hasChange = false;
            int number = board[coordinate.X, coordinate.Y].GetCurrentNumber();
            if (number == 0) return false;
            for (int col = 0; col < Constants.Board_size; col++)
            {
                if (col != coordinate.Y && board[coordinate.X, col].RemoveAvailableNumber(number))
                {
                    if (board[coordinate.X, col].GetSize() == 0) throw new LogicalException();
                    if (board[coordinate.X, col].GetSize() == 1)
                    {
                        board[coordinate.X, col].UpdateCurrentNumber();
                        fullCells.Add(board[coordinate.X, col].GetCoordinate());
                        emptyCells.Remove(board[coordinate.X, col].GetCoordinate());
                    }
                    hasChange = true;
                }
            }
            return hasChange;
        }
        public bool UpdateCol(Coordinate coordinate)
        {
            bool hasChange = false;
            int number = board[coordinate.X, coordinate.Y].GetCurrentNumber();
            if (number == 0) return false;
            for (int row = 0; row < Constants.Board_size; row++)
            {
                if (row != coordinate.X && board[row, coordinate.Y].RemoveAvailableNumber(number))
                {
                    if (board[row, coordinate.Y].GetSize() == 0) throw new LogicalException();
                    if (board[row, coordinate.Y].GetSize() == 1)
                    {
                        board[row, coordinate.Y].UpdateCurrentNumber();
                        fullCells.Add(board[row, coordinate.Y].GetCoordinate());
                        emptyCells.Remove(board[row, coordinate.Y].GetCoordinate());
                    }
                    hasChange = true;
                }
            }
            return hasChange;
        }

        public bool UpdateBlock(Coordinate coordinate)
        {
            bool hasChange = false;
            int number = board[coordinate.X, coordinate.Y].GetCurrentNumber();
            if (number == 0) return false;
            Coordinate startOfBox = findStartOfBox(coordinate);
            int startOfBoxRow = startOfBox.X;
            int startOfBoxCol = startOfBox.Y;
            for(int row = 0;row<Constants.Sqrt_Board_size;row++)
            {
                for(int col = 0;col < Constants.Sqrt_Board_size;col++)
                {
                    if(startOfBoxRow + row != coordinate.X 
                        && startOfBoxCol + col!= coordinate.Y
                        && board[startOfBoxRow + row, startOfBoxCol + col].RemoveAvailableNumber(number))
                    {
                        if (board[startOfBoxRow + row, startOfBoxCol + col].GetSize() == 0) throw new LogicalException();
                        if (board[startOfBoxRow + row, startOfBoxCol + col].GetSize() == 1 && board[startOfBoxRow + row, startOfBoxCol + col].GetCurrentNumber() == 0)
                        {
                            board[startOfBoxRow + row, startOfBoxCol + col].UpdateCurrentNumber();
                            fullCells.Add(board[startOfBoxRow + row, startOfBoxCol + col].GetCoordinate());
                            emptyCells.Remove(board[startOfBoxRow + row, startOfBoxCol + col].GetCoordinate());
                        }
                        hasChange = true;
                    }
                        
                }
            }
            return hasChange;
        }

        private Coordinate findStartOfBox(Coordinate coordinate)
        {
            int X = ((coordinate.X)/Constants.Sqrt_Board_size) * Constants.Sqrt_Board_size;
            int Y = ((coordinate.Y) / Constants.Sqrt_Board_size) * Constants.Sqrt_Board_size;
            return new Coordinate(X, Y);
        }

        public bool UpdateTile(Coordinate coordinate)
        {
            bool hasChange = false;
            if (UpdateRow(coordinate)) hasChange = true;
            if (UpdateCol(coordinate)) hasChange = true;
            if (UpdateBlock(coordinate)) hasChange = true;
            return hasChange;
        }

        public bool FullCellsCheck()
        {
            bool hasChange = false;
            for(;lastFullCellIndex < fullCells.Count;lastFullCellIndex++)
            {
                if (UpdateTile(fullCells[lastFullCellIndex]))
                {
                    hasChange = true;
                }
            }
            return hasChange ;
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
        public bool ImplementHiddenSingle(Coordinate coordinate)
        {
            return ImplementHiddenSingleRow(coordinate) || ImplementHiddenSingleCol(coordinate) || ImplementHiddenSingleBlock(coordinate);
        }

        private bool ImplementHiddenSingleBlock(Coordinate coordinate)
        {
            bool ValidPossibility = true;
            ITile currentTile = board[coordinate.X, coordinate.Y];
            if (currentTile.GetCurrentNumber() != 0) return false;
            Coordinate startOfBox = findStartOfBox(coordinate);
            int startOfBoxRow = startOfBox.X;
            int startOfBoxCol = startOfBox.Y;
            foreach (int possibility in currentTile.GetAvailableNumbers())
            {
                ValidPossibility = false;
                int row = 0;
                int col = 0;

                for (; row < Constants.Sqrt_Board_size && ValidPossibility; row++)
                {
                    for (col = 0; col < Constants.Sqrt_Board_size && ValidPossibility ; col++)
                    {
                        if (startOfBoxRow + row != coordinate.X
                            && startOfBoxCol + col != coordinate.Y
                            && board[startOfBoxRow + row, startOfBoxCol + col].ContainNumber(possibility)) ValidPossibility = false;
                    }
                }

                if (ValidPossibility)
                {
                    board[startOfBoxRow + row - 1, startOfBoxCol + col - 1].SetCurrentNumber(possibility);
                    return true;
                }
            }
            return false;
        }

        private bool ImplementHiddenSingleCol(Coordinate coordinate)
        {
            bool ValidPossibility = true;
            ITile currentTile = board[coordinate.X, coordinate.Y];
            if (currentTile.GetCurrentNumber() != 0) return false;
            foreach (int possibility in currentTile.GetAvailableNumbers())
            {
                ValidPossibility = false;
                int row = 0;
                for (; row < Constants.Board_size && ValidPossibility; row++)
                {
                    if (row != coordinate.X && board[row, coordinate.Y].ContainNumber(possibility)) ValidPossibility = false;
                }
                if (ValidPossibility)
                {
                    board[row - 1, coordinate.Y].SetCurrentNumber(possibility);
                    return true;
                }
            }
            return false;
        }

        private bool ImplementHiddenSingleRow(Coordinate coordinate)
        {
            bool ValidPossibility = true;
            ITile currentTile = board[coordinate.X, coordinate.Y];
            if (currentTile.GetCurrentNumber() != 0) return false;
            foreach (int possibility in currentTile.GetAvailableNumbers())
            {
                ValidPossibility = false;
                int col = 0;
                for (; col < Constants.Board_size && ValidPossibility; col++)
                {
                    if (col != coordinate.Y && board[coordinate.X, col].ContainNumber(possibility)) ValidPossibility = false;
                }
                if (ValidPossibility)
                {
                    board[coordinate.X, col -1].SetCurrentNumber(possibility);
                    return true;
                }
            }
            return false;
        }

        public bool IsBoardFull()
        {
            return fullCells.Count == Constants.Board_size * Constants.Board_size;  
        }

        public ITile GetSmallestTile()
        {
            ITile minTile = null;
            int minCount = Constants.Board_size;
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
            board[tile.GetCoordinate().X, tile.GetCoordinate().Y] = tile;
        }

    }
}

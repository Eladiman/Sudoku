
using Sudoku.src.Entities.Interfaces;
using System;
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

        private ITile[,] board;

        public Board(string expression)
        {
            board = new Tile[Constants.Board_size, Constants.Board_size];

            currentTile = new Coordinate();

            InitializeBoard(expression);

        }

        private void InitializeBoard(string expression)
        {
            int index = 0;
            for (int row = 0; row < Constants.Board_size; row++)
            {
                for (int col = 0; col < Constants.Board_size; col++)
                {
                    board[row, col] = new Tile(expression[index] - '0', new Coordinate(row, col));
                }
            }
        }


        public Coordinate CurrentTile
        {
            get { return new Coordinate(currentTile.X, currentTile.Y); }
        }

        public Coordinate NextTile()
        {
            int currentX = currentTile.X + 1;
            int currentY = currentTile.Y + 1;
            if (NextRowOfBoxes(currentX, currentY))
            {
                currentX = 1;
                currentY = (currentY + 1) % Constants.Board_size;
            }
            else if (NextBox(currentX, currentY))
            {
                currentY -= Constants.Sqrt_Board_size - 1;
                currentX++;
            }
            else
            {
                if (currentX % Constants.Sqrt_Board_size == 0)
                {
                    currentY++;
                    currentX -= Constants.Sqrt_Board_size - 1;
                }
                else currentX++;
            }
            currentTile.X = currentX - 1;
            currentTile.Y = currentY - 1;
            return new Coordinate(currentTile.X, currentTile.Y);
        }

        private bool NextBox(int currentX, int currentY)
        {
            return currentY % Constants.Sqrt_Board_size == 0 && currentX % Constants.Sqrt_Board_size == 0;
        }

        private bool NextRowOfBoxes(int currentX, int currentY)
        {
            return currentY % Constants.Sqrt_Board_size == 0 && currentX == Constants.Board_size;
        }

        public bool UpdateRow(Coordinate coordinate)
        {
            bool hasChange = false;
            int number = board[coordinate.X, coordinate.Y].GetCurrentNumber();
            if (number == 0) return false;
            for (int col = 0; col < Constants.Board_size; col++)
            {
                if (col != coordinate.Y) hasChange = board[coordinate.X, col].RemoveAvailableNumber(number);
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
                if (row != coordinate.X) hasChange = board[row, coordinate.Y].RemoveAvailableNumber(number);
            }
            return hasChange;
        }

        public bool UpdateBlock(Coordinate coordinate)
        {
            bool hasChange = false;
            int number = board[coordinate.X, coordinate.Y].GetCurrentNumber();
            if (number == 0) return false;
            int startOfBoxRow = coordinate.X - (Constants.Sqrt_Board_size - 1);
            int startOfBoxCol = coordinate.Y - (Constants.Sqrt_Board_size - 1);
            for(int row = 0;row<Constants.Sqrt_Board_size;row++)
            {
                for(int col = 0;col < Constants.Sqrt_Board_size;col++)
                {
                    if(startOfBoxRow + row != coordinate.X && startOfBoxCol + col!= coordinate.Y)
                        hasChange = board[startOfBoxRow + row,startOfBoxCol + col].RemoveAvailableNumber(number);
                }
            }
            return hasChange;
        }

        public bool IsBoardFull()
        {
            for (int row = 0; row < Constants.Board_size; row++)
            {
                for (int col = 0; col < Constants.Board_size; col++)
                {
                    if (board[row, col].GetCurrentNumber() == 0) return false;
                }
            }
            return true;
        }

        public Coordinate GetSmallestCoordinate()
        {
            Coordinate minCoordinate = null;
            int minCount = Constants.Board_size;
            for (int row = 0; row < Constants.Board_size; row++)
            {
                for (int col = 0; col < Constants.Board_size; col++)
                {
                    if (board[row, col].GetCurrentNumber() == 0 && board[row, col].GetSize() < minCount)
                    {
                        minCount = board[row, col].GetSize();
                        minCoordinate = new Coordinate(row, col);
                    }
                }
            }
            return minCoordinate;
        }

    }
}

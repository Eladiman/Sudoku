using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.Entities.Models
{
    public class Board
    {
        private Coordinate currentTile;

        private Tile[,] board;

        public Board(string expression)
        {
            board = new Tile[Constants.Board_size, Constants.Board_size];

            currentTile = new Coordinate();

            initializeBoard(expression);

        }

        private void initializeBoard(string expression)
        {
            int index = 0;
            for (int row = 0; row < Constants.Board_size; row++)
            {
                for (int col = 0; col < Constants.Board_size; col++)
                {
                    board[row, col] = new Tile(expression[index] - '0');
                }
            }
        }


        public Coordinate CurrentTile
        {
            get { return new Coordinate(currentTile.X, currentTile.Y); }
        }

        public Coordinate nextTile()
        {
            int currentX = currentTile.X + 1;
            int currentY = currentTile.Y + 1;
            if (nextRowOfBoxes(currentX, currentY))
            {
                currentX = 1;
                currentY = (currentY + 1) % Constants.Board_size;
            }
            else if (nextBox(currentX, currentY))
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

        private bool nextBox(int currentX, int currentY)
        {
            return currentY % Constants.Sqrt_Board_size == 0 && currentX % Constants.Sqrt_Board_size == 0;
        }

        private bool nextRowOfBoxes(int currentX, int currentY)
        {
            return currentY % Constants.Sqrt_Board_size == 0 && currentX == Constants.Board_size;
        }
    }
}

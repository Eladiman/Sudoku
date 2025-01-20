using Sudoku.src.Entities.Exceptions;
using Sudoku.src.Entities.Interfaces;
using Sudoku.src.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.Logic
{
    public static class BoardSolver
    {

        public static bool SolveBoard(Board board)
        {
            try
            {
                if (SolveBoardWithoutRecursion(board)) return true;
            }
            catch (LogicalException le) { return false; }

            ITile smallestTile = board.GetSmallestTile();

            if (smallestTile == null) return true;//not sure if neccery

            return SolveBoardWithoutRecursion(smallestTile,board);
        }

        private static bool SolveBoardWithoutRecursion(ITile smallestTile,Board board)
        {
            Dictionary<Coordinate, HashSet<int>> savedOptions = new Dictionary<Coordinate, HashSet<int>>();

            foreach (int currentPossibility in smallestTile.GetAvailableNumbers())
            {
                savedOptions = board.SaveBoardState();
                if (SolveBoard(board)) return true;
                smallestTile.RemoveAvailableNumber(currentPossibility);
                board.ReplaceTile(smallestTile);
                board.RestoreBoardState(savedOptions);
            }
            return false;
        }

        private static bool SolveBoardWithoutRecursion(Board board)
        {
            bool unSolvable= false;
            bool tryAgain = false;
            while (!unSolvable)
            {
                for (int time = 0; time < Constants.Board_size * Constants.Board_size; time++)
                {
                    if (board.IsBoardFull()) return true;
                    if (board.UpdateTile(board.CurrentTile)) tryAgain = true;
                    board.NextTile();
                }
                if (!tryAgain) unSolvable = true;
            }
            return false;
            
        }

    }
}

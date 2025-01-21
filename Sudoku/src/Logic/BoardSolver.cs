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

            Dictionary<Coordinate, HashSet<int>> savedOptions = new Dictionary<Coordinate, HashSet<int>>();

            foreach (int currentPossibility in smallestTile.GetAvailableNumbers())
            {
                try
                {
                    savedOptions = board.SaveBoardState();
                    smallestTile.SetCurrentNumber(currentPossibility);
                    board.ReplaceTile(smallestTile);

                    if (SolveBoard(board)) return true;

                    board.RestoreBoardState(savedOptions);
                    smallestTile.SetCurrentNumber(0);
                    smallestTile.RemoveAvailableNumber(currentPossibility);
                    board.ReplaceTile(smallestTile);
                }
                catch (LogicalException le) { return false; }

            }
            return false;
        }

        private static bool SolveBoardWithRecursion(ITile smallestTile,Board board)
        {
            Dictionary<Coordinate, HashSet<int>> savedOptions = new Dictionary<Coordinate, HashSet<int>>();

            foreach (int currentPossibility in smallestTile.GetAvailableNumbers())
            {
                try
                {
                    savedOptions = board.SaveBoardState();
                    smallestTile.SetCurrentNumber(currentPossibility);
                    board.ReplaceTile(smallestTile);

                    if (SolveBoard(board)) return true;

                    board.RestoreBoardState(savedOptions);
                    smallestTile.SetCurrentNumber(0);
                    smallestTile.RemoveAvailableNumber(currentPossibility);
                    board.ReplaceTile(smallestTile);
                }
                catch (LogicalException le) { return false;}
                
            }
            return false;
        }

        private static bool SolveBoardWithoutRecursion(Board board)
        {
            bool unSolvable= false;
            bool tryAgain = false;
            while (!unSolvable)
            {
                tryAgain = false;
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

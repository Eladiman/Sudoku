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

            if (smallestTile == null) return true;//board is full

            return SolveBoardWithRecursion(smallestTile,board);
        }

        private static bool SolveBoardWithRecursion(ITile smallestTile,Board board)
        {
            Dictionary<Coordinate, HashSet<int>> savedOptions = board.SaveBoardState();

            int lastFullCellIndex = board.GetLastFullCellIndex();

            HashSet<int> possibilities = smallestTile.GetAvailableNumbers();

            foreach (int currentPossibility in possibilities)
            {
                smallestTile.UpdateCurrentNumberAndDeletePossibilities(currentPossibility);
                board.AddFullCell(smallestTile.GetCoordinate());
                board.RemoveEmptyCell(smallestTile.GetCoordinate());
                //Console.WriteLine(board.ToString());
                if (SolveBoard(board)) return true;

                board.RestoreBoardState(savedOptions);
                board.RestoreFullCells(lastFullCellIndex);

            }
            return false;
        }
        private static bool SolveBoardWithoutRecursion(Board board)
        {
            Heuristics.FullCellsCleanUp(board);
            if (board.IsBoardFull()) return true;
            return false;
        }

    }
}

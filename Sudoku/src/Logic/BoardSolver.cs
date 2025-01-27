using Sudoku.src.Entities.Exceptions;
using Sudoku.src.Entities.Interfaces;
using Sudoku.src.Entities.Models;
using Sudoku.src.Logic.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.Logic
{
    public static class BoardSolver
    {
        /// <summary>
        /// This function calls the 2 solution functions. 
        /// In addition, it finds the cell with the smallest number of options 
        /// and sends it to the solution function with recursion.
        /// </summary>
        /// <param name="board"></param>
        /// <returns>True if the board was solved. false otherwise</returns>
        public static bool SolveBoard(Board board)
        {
            try
            {
                if (SolveBoardWithoutRecursion(board)) return true;
            }
            catch (LogicalException le) { return false; }

            ITile smallestTile = board.GetSmallestTile();//find the smallest tile

            if (smallestTile == null) return true;//board is full

            return SolveBoardWithRecursion(smallestTile,board);
        }

        /// <summary>
        /// Trying to solve the board with recursion.
        /// by going over all the options of the cell with the fewest number of options.
        /// </summary>
        /// <param name="smallestTile"></param>
        /// <param name="board"></param>
        /// <returns>True if the board was solved. false otherwise</returns>
        private static bool SolveBoardWithRecursion(ITile smallestTile,Board board)
        {
            Dictionary<Coordinate, HashSet<int>> savedEmptyCellsState = board.SaveBoardState(); // Save the empty cells state before the backtrack

            int lastFullCellIndex = board.GetLastFullCellIndex();

            HashSet<int> possibilities = smallestTile.GetAvailableNumbers();//save possibilities of the smallest tile

            foreach (int currentPossibility in possibilities)
            {
                smallestTile.UpdateCurrentNumberAndDeletePossibilities(currentPossibility);
                board.AddFullCell(smallestTile.GetCoordinate());
                board.RemoveEmptyCell(smallestTile.GetCoordinate());
                //Console.WriteLine(board.ToString());
                if (SolveBoard(board)) return true;

                board.RestoreBoardState(savedEmptyCellsState);
                board.RestoreFullCells(lastFullCellIndex);

            }
            return false;
        }

        /// <summary>
        /// Attempts to solve the board without recursion by using heuristics.
        /// </summary>
        /// <param name="board"></param>
        /// <returns>True if the board was solved. false otherwise</returns>
        private static bool SolveBoardWithoutRecursion(Board board)
        {
            bool again = true;
            while(again)
            {
                BasicHeuristic.FullCellsCleanUp(board);
                if (!HiddenSingleHeuristics.HiddenSingle(board)) again = false;
            }
            if (board.IsBoardFull()) return true;
            return false;
        }

    }
}

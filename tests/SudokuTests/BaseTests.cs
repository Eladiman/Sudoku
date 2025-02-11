using Sudoku.src.Entities.Models;
using Sudoku.src.Logic;

namespace SudokuTests;

public static class BaseTests
{
    public static void CheckSudokuSolver(string expression)
    {
        Board board = new Board(expression);
        bool solve = BoardSolver.SolveBoard(board);
        Assert.True(solve);
    }
}
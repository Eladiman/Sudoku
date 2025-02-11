namespace Sudoku.src.Consts
{
    /// <summary>
    /// The following class responsible for saving the board size and other constants
    /// </summary>
    public static class SudokuConstants
    {
        public const int MaxBoardSize = 25;
        public static int BoardSize { get; set; }
        public static int SqrtBoardSize => (int)Math.Sqrt(BoardSize);

        public const char AsciiDiff = '0';
    }
}

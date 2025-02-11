namespace Sudoku.src.Consts
{
    /// <summary>
    /// The following class responsible for saving the board size and other constants
    /// </summary>
    public static class SudokuConstants
    {
        public const int MAX_BOARD_SIZE = 25;
        public static int Board_size { get; set; }
        public static int Sqrt_Board_size { get; set; }

        public const char ASCII_DIFF = '0';
    }
}

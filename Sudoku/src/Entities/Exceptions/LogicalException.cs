namespace Sudoku.src.Entities.Exceptions
{
    /// <summary>
    /// The following class responsible for Logical Exception - board not solvable
    /// </summary>
    internal class LogicalException : Exception
    {
        public LogicalException() { }
        public LogicalException(string message) : base(message) { }
    }
}

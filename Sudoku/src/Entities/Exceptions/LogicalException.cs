namespace Sudoku.src.Entities.Exceptions
{
    internal class LogicalException : Exception
    {
        public LogicalException() { }
        public LogicalException(string message) : base(message) { }
    }
}

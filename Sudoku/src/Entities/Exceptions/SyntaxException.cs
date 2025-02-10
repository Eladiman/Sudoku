namespace Sudoku.src.Entities.Exceptions
{
    internal class SyntaxException : Exception
    {
        public SyntaxException() { }

        public SyntaxException(string message) : base(message) { }
    }
}

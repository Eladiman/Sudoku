namespace Sudoku.src.Entities.Exceptions
{
    /// <summary>
    ///  The following class responsible for Syntax Exception - an wanted chars/ length not valid
    /// </summary>
    internal class SyntaxException : Exception
    {
        public SyntaxException() { }

        public SyntaxException(string message) : base(message) { }
    }
}

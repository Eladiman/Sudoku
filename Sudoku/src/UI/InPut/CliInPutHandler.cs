namespace Sudoku.src.UI.InPut
{
    /// <summary>
    /// The following class responsible for read the board string from the cli.
    /// </summary>
    public static class CliInPutHandler
    {
        /// <summary>
        /// read expression from the cli
        /// </summary>
        /// <returns>
        /// return the expression
        /// </returns>
        public static string GetInputFromUser()
        {
            string str = Console.ReadLine();
            if (str == null) return "";
            return str;
        }


    }
}

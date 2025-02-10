namespace Sudoku.src.UI.InPut
{
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

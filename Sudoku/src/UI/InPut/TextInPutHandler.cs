namespace Sudoku.src.UI.InPut
{
    public static class TextInPutHandler
    {
        /// <summary>
        /// read expression from a given text file
        /// </summary>
        /// <returns>
        /// return the expression
        /// </returns>
        public static string GetInputFromUser(string filePath)
        {
            if (filePath == null) return "";

            filePath = filePath.Trim();

            filePath = filePath.Trim('"');

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found!.Please try again.");
            }

            return File.ReadAllText(filePath).Trim();
        }
    }
}

namespace Sudoku.src.UI.InPut
{
    /// <summary>
    /// The following class responsible for read the board string from a given file path.
    /// </summary>
    public static class TextInPutHandler
    {
        /// <summary>
        /// read expression from a given text file
        /// file should contain only 1 sudoku inside of it and nothing more.
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

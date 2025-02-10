using Sudoku.src.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.UI
{
    public static class TextInPutHandler
    {
        /// <summary>
        /// read expression from a given text file
        /// </summary>
        /// <returns>
        /// return the expression
        /// </returns>
        public static string GetInputFromUser()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter file path: ");
            Console.ForegroundColor = ConsoleColor.White;
            string filePath = Console.ReadLine();

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

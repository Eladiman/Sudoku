using Sudoku.src.Consts;
using Sudoku.src.Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
namespace Sudoku.src.Logic
{
    public static class Validation
    {
        /// <summary>
        /// Checks the length of the board and throws an exception for a board with invalid length.
        /// </summary>
        /// <param name="expression"></param>
        /// <exception cref="SyntaxException">
        /// If board length is not a power of a number from 1 to 5throws exception.
        /// </exception>
        public static void CheckLength(string expression)
        {
            if (expression == null || expression.Length == 0) throw new SyntaxException("Expression can't be empty!");
            double length = Math.Sqrt(expression.Length);
            bool isInt = length == (int)length;//checks if int
            if (!isInt || length > SudokuConstants.MAX_BOARD_SIZE)
                throw new SyntaxException($"expression Length is not Valid! should be a Power of a number from 1 to 5");
        }

        /// <summary>
        /// Goes through the board and checks the validity of the characters. 
        /// Invalid characters are characters that are not in the range between 0 and 0 + the board size.
        /// </summary>
        /// <param name="expression"></param>
        /// <exception cref="SyntaxException">
        /// If board contain an invalid char throws exception.
        /// </exception>
        public static void CheckNumber(string expression)
        {
            int length = (int)Math.Sqrt(expression.Length);
            foreach (char number in expression)
            {
                if (number < SudokuConstants.ASCII_DIFF || number > SudokuConstants.ASCII_DIFF + length)
                    throw new SyntaxException($"{number} is not a valid parameter in the Sudoku (should be contained only chars from {SudokuConstants.ASCII_DIFF} - {length})!");
            }
        }

    }
}
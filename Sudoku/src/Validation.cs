using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
namespace Sudoku.src
{
    public static class Validation
    {
        public static bool CheckLength(String expression)
        {
            if (expression == null) return false;
            return expression.Length == Constants.Board_size * Constants.Board_size;
        }
        public static bool CheckNumber(String expression)
        {
            foreach (char number in expression)
            {
                if (!char.IsDigit(number)) return false;
            }
            return true;
        }
        public static bool CheckNumberCount(String expression)
        {
            int[] numberCountArray = new int[Constants.Board_size - 1];
            //fill counter array
            foreach (char number in expression)
            {
                if (number != '0')
                {
                    numberCountArray[number - '1']++;
                }
            }
            //a specific number appear more then boars size times
            foreach (int counter in numberCountArray)
            {
                if (counter > Constants.Board_size) return false;
            }
            return true;
        }
    }
}
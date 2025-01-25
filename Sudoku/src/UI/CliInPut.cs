using Sudoku.src.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.UI
{
    public static class CliInPut
    {
        /// <summary>
        /// read expression from TextFile
        /// </summary>
        /// <returns>
        /// return the expression
        /// </returns>
        public static string GetInputFromUser()
        {
            String str = Console.ReadLine();
            if (str == null) return "";
            return str;
        }
    }
}

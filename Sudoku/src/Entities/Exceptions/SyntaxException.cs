using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.Entities.Exceptions
{
    internal class SyntaxException : Exception
    {
        public SyntaxException() { }

        public SyntaxException(string message) : base(message) { }
    }
}

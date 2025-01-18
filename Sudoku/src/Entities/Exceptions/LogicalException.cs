using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.Entities.Exceptions
{
    internal class LogicalException : Exception
    {
        public LogicalException() { }
        public LogicalException(string message) : base(message) { }
    }
}

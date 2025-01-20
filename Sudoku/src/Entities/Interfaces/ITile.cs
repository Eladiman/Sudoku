using Sudoku.src.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.Entities.Interfaces
{
    public interface ITile
    {
        public int GetFirstNumberAvailable();

        public int GetSize();

        public bool RemoveAvailableNumber(int number);

        public HashSet<int> GetAvailableNumbers();

        public void AddNumber(int number);

        public int GetCurrentNumber();

        public void SetCurrentNumber(int number);

        public Coordinate GetCoordinate();
    }
}

using Sudoku.src.Consts;
using Sudoku.src.Entities.Exceptions;
using Sudoku.src.Entities.Interfaces;

namespace Sudoku.src.Entities.Models
{
    /// <summary>
    /// The following class responsible for representing an implementation of a cell and its methods
    /// use of coordinate for place
    /// use of hash set for possibilities
    /// </summary>
    public class Tile : ITile
    {
        private Coordinate _place;

        private HashSet<int> _possibilities;

        private int _currentNumber;

        /// <summary>
        /// Gets a number and a coordinate and initialize its starting values according to the number
        /// </summary>
        /// <param name="number"></param>
        /// <param name="coordinate"></param>
        public Tile(int number, Coordinate coordinate)
        {
            _possibilities = new HashSet<int>();
            _place = coordinate;
            _currentNumber = 0;
            if (number != 0)
            {
                _possibilities.Add(number);
                _currentNumber = number;
            }
            else
            {
                //if number is 0 empty add all the possibilities to cell
                for (int numberToFill = 1; numberToFill <= SudokuConstants.BoardSize; numberToFill++)
                {
                    AddNumber(numberToFill);
                }
            }
        }

        public HashSet<int> GetAvailableNumbers()
        {
            return new HashSet<int>(_possibilities);
        }

        public void SetAvailableNumbers(HashSet<int> availableNumbers)
        {
            _possibilities = new HashSet<int>(availableNumbers);
        }

        public int GetSize()
        {
            return _possibilities.Count;
        }

        /// <summary>
        /// gets a number and remove it from the cell possibilities
        /// </summary>
        /// <param name="number"></param>
        /// <returns>true is the number was removed false otherwise</returns>
        /// <exception cref="LogicalException"> if there is an attempted to remove a number from a full cell then throw exception</exception>
        public bool RemoveAvailableNumber(int number)
        {
            if (_currentNumber != 0 && _currentNumber == number) throw new LogicalException(); //attempt to execute an invalid board state
            if (_currentNumber == 0 && _possibilities.Contains(number))
            {
                _possibilities.Remove(number);
                return true;
            }
            return false;
        }

        public void UpdateCurrentNumber()
        {
            _currentNumber = _possibilities.First();
        }

        public void AddNumber(int number)
        {
            _possibilities.Add(number);
        }

        public int GetCurrentNumber()
        {
            return _currentNumber;
        }

        public void SetCurrentNumber(int number)
        {
            _currentNumber = number;
        }

        public Coordinate GetCoordinate()
        {
            return _place;
        }

        public bool ContainNumber(int number)
        {
            return _possibilities.Contains(number);
        }

        public void UpdateCurrentNumberAndDeletePossibilities(int number)
        {
            _possibilities.Clear();
            _possibilities.Add(number);
            _currentNumber = number;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            Tile other = (Tile)obj;
            if (_possibilities.Equals(other._possibilities)) return true;
            return false;
        }
    }
}

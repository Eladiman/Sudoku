using Sudoku.src.Consts;
using Sudoku.src.Entities.Exceptions;
using Sudoku.src.Entities.Interfaces;

namespace Sudoku.src.Entities.Models
{
    public class Tile : ITile
    {
        private Coordinate place;

        private HashSet<int> _tiles;

        private int currentNumber;

        /// <summary>
        /// Gets a number and a coordinate and initialize its starting values according to the number
        /// </summary>
        /// <param name="number"></param>
        /// <param name="coordinate"></param>
        public Tile(int number, Coordinate coordinate)
        {
            _tiles = new HashSet<int>();
            place = coordinate;
            currentNumber = 0;
            if (number != 0)
            {
                _tiles.Add(number);
                currentNumber = number;
            }
            else
            {
                //if number is 0 empty add all the possibilities to cell
                for (int numberToFill = 1; numberToFill <= SudokuConstants.Board_size; numberToFill++)
                {
                    AddNumber(numberToFill);
                }
            }
        }

        public HashSet<int> GetAvailableNumbers()
        {
            return new HashSet<int>(_tiles);
        }

        public void SetAvailableNumbers(HashSet<int> availableNumbers)
        {
            _tiles = new HashSet<int>(availableNumbers);
        }

        public int GetSize()
        {
            return _tiles.Count;
        }

        /// <summary>
        /// gets a number and remove it from the cell possibilities
        /// </summary>
        /// <param name="number"></param>
        /// <returns>true is the number was removed false otherwise</returns>
        /// <exception cref="LogicalException"> if there is an attempted to remove a number from a full cell then throw exception</exception>
        public bool RemoveAvailableNumber(int number)
        {
            if (currentNumber != 0 && currentNumber == number) throw new LogicalException(); //attempt to execute an invalid board state
            if (currentNumber == 0 && _tiles.Contains(number))
            {
                _tiles.Remove(number);
                return true;
            }
            return false;
        }

        public void UpdateCurrentNumber()
        {
            currentNumber = _tiles.First();
        }

        public void AddNumber(int number)
        {
            _tiles.Add(number);
        }

        public int GetCurrentNumber()
        {
            return currentNumber;
        }

        public void SetCurrentNumber(int number)
        {
            currentNumber = number;
        }

        public Coordinate GetCoordinate()
        {
            return place;
        }

        public bool ContainNumber(int number)
        {
            return _tiles.Contains(number);
        }

        public void UpdateCurrentNumberAndDeletePossibilities(int number)
        {
            _tiles.Clear();
            _tiles.Add(number);
            currentNumber = number;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            Tile other = (Tile)obj;
            if (_tiles.Equals(other._tiles)) return true;
            return false;
        }
    }
}

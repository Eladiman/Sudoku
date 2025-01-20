using Sudoku.src.Entities.Exceptions;
using Sudoku.src.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.Entities.Models
{
    public class Tile : ITile
    {
        private Coordinate place;

        private HashSet<int> _tiles;
       
        private int currentNumber;

        public Tile(int number, Coordinate coordinate)
        {
            _tiles = new HashSet<int>();
            place = coordinate;
            currentNumber = 0;
            if (number != 0) { 
                _tiles.Add(number);
                currentNumber = number;
            }
            else { 
                for(int numberToFill = 1 ;numberToFill<=Constants.Board_size;numberToFill++)
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
            _tiles=new HashSet<int>(availableNumbers);
        }

        public int GetSize()
        {
            return _tiles.Count;
        }
        public bool RemoveAvailableNumber(int number)
        {
            if (currentNumber == number) throw new LogicalException(); //attempt to execute an invalid board state
            if (_tiles.Contains(number))
            {
                _tiles.Remove(number);
                if (GetSize() == 1) UpdateCurrentNumber(); 
                return true;
            }
            return false;
        }

        private void UpdateCurrentNumber()
        {
            foreach (int number in _tiles)
            {
                currentNumber = number;
                break;
            }
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
            return new Coordinate(place.X,place.Y);
        }
    }
}

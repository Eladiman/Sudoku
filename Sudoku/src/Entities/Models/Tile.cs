using Sudoku.src.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.Entities.Models
{
    public class Tile : ITile
    {
        private Coordinate place;

        private HashSet<int> _tiles;

        private HashSet<int>.Enumerator firstAvailableNumber;
       
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
            firstAvailableNumber = _tiles.GetEnumerator();
            firstAvailableNumber.MoveNext();
        }

        public HashSet<int> GetAvailableNumbers()
        {
            return new HashSet<int>(_tiles);
        }

        public int GetFirstNumberAvailable()
        {
            return firstAvailableNumber.Current;
        }
        public int GetSize()
        {
            return _tiles.Count;
        }
        public bool RemoveAvailableNumber(int number)
        {
            if(_tiles.Contains(number))
            {
                _tiles.Remove(number);
                
                if(GetSize() == 0) return false;//TODO: add an Exception Board is not solvable
                
                
                if (firstAvailableNumber.Current == number)
                {
                    firstAvailableNumber = _tiles.GetEnumerator();
                    firstAvailableNumber.MoveNext();
                }
                if (GetSize() == 1) currentNumber = GetFirstNumberAvailable(); 
                return true;
            }
            return false;
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

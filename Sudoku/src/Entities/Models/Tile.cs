using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.Entities.Models
{
    public class Tile
    {
        private HashSet<int> _tiles;
        private HashSet<int>.Enumerator firstAvailableNumber;

        public Tile(int number)
        {
            _tiles = new HashSet<int>();

            if (number != 0) { 
                _tiles.Add(number);
            }
            else { 
                for(int numberToFill = 1;numberToFill<=Constants.Board_size;numberToFill++)
                {
                    _tiles.Add(numberToFill);
                }
            }
            firstAvailableNumber = _tiles.GetEnumerator();
            firstAvailableNumber.MoveNext();
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
                if (firstAvailableNumber.Current == number)
                {
                    firstAvailableNumber = _tiles.GetEnumerator();
                    firstAvailableNumber.MoveNext();
                }
                return true;
            }
            return false;
        }

    }
}

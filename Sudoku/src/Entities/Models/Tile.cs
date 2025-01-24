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
        public int X { get; private set; }
        
        public int Y { get; private set; }

        private HashSet<int> _tiles;
       
        private int currentNumber;

        public Tile(int number, int x, int y)
        {
            _tiles = new HashSet<int>();
            X = x;
            Y = y;
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
            if (currentNumber == number && number != 0) throw new LogicalException(); //attempt to execute an invalid board state
            
            if (_tiles.Contains(number) && currentNumber == 0)
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.Entities.Models
{
    public class Tile
    {
        private int availableNumberCounter;

        private int lastAvailableNumber;

        private bool[] availableNumbers;

        public Tile(int number)
        {
            availableNumbers = new bool[Constants.Board_size];

            for (int i = 0; i < availableNumbers.Length; i++)
            {
                availableNumbers[i] = true;
            }

            lastAvailableNumber = 1;
            availableNumberCounter = Constants.Board_size;

            if (number > 0)
            {
                lastAvailableNumber = number;
                availableNumberCounter = 1;
            }
        }

        public int LastAvailableNumber
        {
            get { return lastAvailableNumber; }
        }

        public int AvailableNumberCounter
        {
            get { return availableNumberCounter; }
        }

        public bool RemoveAvailableNumber(int number)
        {
            if (availableNumbers[number - 1])
            {
                availableNumbers[number - 1] = false;

                availableNumberCounter--;
                //TODO: throw an exception if 0

                for (int i = lastAvailableNumber - 1; i < availableNumbers.Length; i++)
                {
                    if (availableNumbers[i])
                    {
                        lastAvailableNumber = i + 1;
                        break;
                    }
                }
                return true;
            }
            return false;
        }

    }
}

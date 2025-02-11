using Sudoku.src.Entities.Models;

namespace Sudoku.src.Entities.Interfaces
{
    /// <summary>
    /// The following interface responsible for representing the methods of a cell in board
    /// </summary>
    public interface ITile
    {

        public int GetSize(); 

        public bool RemoveAvailableNumber(int number); // gets a number and remove it from the cell possibilities

        public HashSet<int> GetAvailableNumbers();

        public void AddNumber(int number); // gets a number and add it to the cell possibilities

        public int GetCurrentNumber();

        public void SetCurrentNumber(int number);

        public void SetAvailableNumbers(HashSet<int> availableNumbers);

        public Coordinate GetCoordinate();

        public void UpdateCurrentNumber();

        // gets a number and make it as the cells current number  by removing the other cell possibilities
        public void UpdateCurrentNumberAndDeletePossibilities(int number); 

        public bool ContainNumber(int number);//return true if the given number is in the cell possebilities
    }
}

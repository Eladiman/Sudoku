using Sudoku.src.Entities.Models;

namespace Sudoku.src.Entities.Interfaces
{
    public interface ITile
    {

        public int GetSize();

        public bool RemoveAvailableNumber(int number);

        public HashSet<int> GetAvailableNumbers();

        public void AddNumber(int number);

        public int GetCurrentNumber();

        public void SetCurrentNumber(int number);

        public void SetAvailableNumbers(HashSet<int> availableNumbers);

        public Coordinate GetCoordinate();

        public void UpdateCurrentNumber();

        public void UpdateCurrentNumberAndDeletePossibilities(int number);

        public bool ContainNumber(int number);
    }
}

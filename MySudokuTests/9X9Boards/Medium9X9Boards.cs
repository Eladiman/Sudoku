using Sudoku.src.Entities.Models;
using Sudoku.src.Logic;

namespace MySudokuTests._9X9Boards
{
    [TestClass]
    public class Medium9X9Boards
    {
        [TestMethod]
        public void Medium9X9Board1()
        {
            //Arrange
            string expression = "800000000003600000070090200050007000000045700000100030001000068008500010090000400";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Medium9X9Board2()
        {
            //Arrange
            string expression = "400000805030000000000700000020000060000080400000010000000603070500200000104000000";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Medium9X9Board3()
        {
            //Arrange
            string expression = "800000000095000000076000000000624798000593142000718536000006417000070983000800265";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Medium9X9Board4()
        {
            //Arrange
            string expression = "800000000059000000067000000000728563000691487000534912000976030000412708000853000";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Medium9X9Board5()
        {
            //Arrange
            string expression = "100048000050000900006000300000570200803000000000900000000000416700000000002000000";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }
    }
}

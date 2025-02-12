using Sudoku.src.Entities.Models;
using Sudoku.src.Logic;

namespace MySudokuTests._9X9Boards
{
    [TestClass]
    public class Simple9X9Boards
    {
        [TestMethod]
        public void Simple9X9Board1()
        {
            //Arrange
            string expression = "000000000000003085001020000000507000004000100090000000500000073002010000000040009";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Simple9X9Board2()
        {
            //Arrange
            string expression = "000006000059000008200008000045000000003000000006003054000325006000000000000000000";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Simple9X9Board3()
        {
            //Arrange
            string expression = "005300000800000020070010500400005300010070006003200080060500009004000030000009700";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Simple9X9Board4()
        {
            //Arrange
            string expression = "900800000000000500000000000020010003010000060000400070708600000000030100400000200";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Simple9X9Board5()
        {
            //Arrange
            string expression = "003080000000350000070000600005000000020009407000000001000000080060000030100004000";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

    }
}

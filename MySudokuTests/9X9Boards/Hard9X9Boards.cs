using Sudoku.src.Entities.Models;
using Sudoku.src.Logic;

namespace MySudokuTests._9X9Boards
{
    [TestClass]
    public class Hard9X9Boards
    {
        [TestMethod]
        public void Hard9X9Board1()
        {
            //Arrange
            string expression = "003000002080050000700800049000000100006003000900500078009060014000400200100000500";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Hard9X9Board2()
        {
            //Arrange
            string expression = "509600000030807920000300800000016080050000010000000032104030000006709000000000003";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Hard9X9Board3()
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
        public void Hard9X9Board4()
        {
            //Arrange
            string expression = "100000027000304015500170683430962001900007256006810000040600030012043500058001000";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Hard9X9Board5()
        {
            //Arrange
            string expression = "000000008003000400090020060000079000000061200060502070008000500010000020405000003";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }
    }
}

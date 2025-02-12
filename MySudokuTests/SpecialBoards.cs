using Sudoku.src.Entities.Exceptions;
using Sudoku.src.Entities.Models;
using Sudoku.src.Logic;

namespace MySudokuTests
{
    [TestClass]
    public class SpecialBoards
    {
        [TestMethod]
        public void Empty25X25Board()
        {
            //Arrange
            string expression = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Empty16X16Board()
        {
            //Arrange

            string expression = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Empty9X9Board()
        {
            //Arrange
            string expression = "000000000000000000000000000000000000000000000000000000000000000000000000000000000";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void NotSolvAble25X25Board()
        {
            //Arrange
            string expression = "0090000000000000000000030902000000000000000000000000000000000000000000000000000000100000000000301000040000000000265000009700200600059000000000700000000000000040000000000000000000000000000000000000000000000000000000000000000000000000000000000000400000000900000001000000000007001000004020000700004007000000000000003000020000000000000100000000000001000000000000000010000000000000042000001000000000000000000000000000100000500000900100040000000000000000000000400600000000000000000000000000000000000000000000000000000000200000000000000000000000000400000000900000001000000000000000700000000000000040000000069000000000000000000000000";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsFalse(solved);
        }

        [TestMethod]
        public void NotSolveAble16X16Board()
        {
            //Arrange

            string expression = ";0?0=>010690000000710000500:?0;4000000<0400070=005<3000800000000500@000:?80>10004<30>?8;00=20000>?8;270060000000000000900000000?0000?00000>0=000?3:0000>0026000000;>61029@0<00000100<0@00:40000800500:0?;>012600800?0;0000090<0@0;07000005<00?8:00003050:4080709";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsFalse(solved);
        }

        [TestMethod]
        public void NotSolveAble9X9Board()
        {
            //Arrange
            string expression = "000005080000601043000000000010500000000106000300000005530000061000000004000000000";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsFalse(solved);
        }

        [TestMethod]
        public void NotValidLengthBoard()
        {
            //Arrange
            string expression = "000000000000000000000000";

            //Assert + Act
            Assert.ThrowsException<SyntaxException>(() => Validator.CheckLength(expression));
        }

        [TestMethod]
        public void UseOfUnWantedCharBoard()
        {
            //Arrange
            string expression = "s00000a0000000000000000000s00000000s000000000000000000000000000000000000000000000";

            //Assert + Act
            Assert.ThrowsException<SyntaxException>(() => Validator.CheckNumber(expression));
        }
    }
}
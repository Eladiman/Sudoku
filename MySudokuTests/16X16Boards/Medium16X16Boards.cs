using Sudoku.src.Entities.Models;
using Sudoku.src.Logic;

namespace MySudokuTests._16X16Boards
{
    [TestClass]
    public class Medium16X16Boards
    {
        [TestMethod]
        public void Medium16X16Board1()
        {
            //Arrange
            string expression = "8001?796000:000007000:4000000@=1<002500;=0000000000;8@0190?000000008746000:00005000?030<;00=@0000020000010@9040000;5@9086070002<0;0000000@004000005>000@07420000968@0207000;=050000030<:0>0006002<000003000000@000:000>=0000000000>06?00040<;0006?@00000:3;0180=";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Medium16X16Board2()
        {
            //Arrange
            string expression = "000<0000:?00001000000500001<000?00=0?00:00070>0003;0000040957:0004005=0006>?0@00@1>00?00=0000000200=007004@000<:500000;0020000310004200?>0000900>000@00309000200;00010=0000000@>30090080000@070000010;>530600=0800030@?00=0200>40>000000@<04205900@0000490000;00";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Medium16X16Board3()
        {
            //Arrange
            string expression = "00?00=0309<007003:000@06007000000900<?040000500:0001>702=:600000740086001300?0<@000;=0000<@0000>281000><00:00000>30600000;00900000:4000050=810@0007?6409>0000<000000030000240800@200700801000=?0000=500?0>;<000906000:;>0=?00000?>0049000203:6;00;00300060070000";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Medium16X16Board4()
        {
            //Arrange
            string expression = "<000509000000062000000005000?400000000=000000:000000<06070008000700000000050403050004000000000000800000000600?00000005>;04000000:;000000608000000000000000000000609>70000000004042000009000006>00020000@000008?0?0000:000@0000000001000000020000000010000=000000";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Medium16X16Board5()
        {
            //Arrange
            string expression = "0000:=000000000?70050;01:00@90<8900800700004600=60:=080000070002=00030890>?500012;01@:000008007>00001000@0000900000<>?0740000000006@900000>0100002;0600=800<00500070002000000000<0900>?5;4020=0@0020=0@0<0907000>?500400=6000<803<0000002001@:0000=68000?0004020";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }
    }
}

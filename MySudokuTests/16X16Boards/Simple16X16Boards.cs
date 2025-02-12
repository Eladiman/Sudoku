using Sudoku.src.Entities.Models;
using Sudoku.src.Logic;

namespace MySudokuTests._16X16Boards
{
    [TestClass]
    public class Simple16X16Boards
    {
        [TestMethod]
        public void Simple16X16Board1()
        {
            //Arrange
            string expression = "0>@03?;0040860070000:0>200<000?000620095:0=0000800=00060?300000<00?070<0@0500>4:=:;7000>1<30?006><30=04000000002080000:0000=005;000015049?;7800>0000>0000=02000070:000?0<>00000900060<0980000?;0?60<5;0009000080;04>00=?70:0000001200800;00@00900009000:30050<70";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Simple16X16Board2()
        {
            //Arrange
            string expression = ";07805>0300:=0<@004007000<0?:002:0000000000437000050009?000000080400;8000>07930000>37<0000?=;0049?07005=03000080500=0?3024:8<0@008000000;0000@=500=000000800?0>000:090?0067000;<000<0:;0000>72407:090;1000500630=0<>?070:00008000000>0:00=0050000635@9<00;00>000";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Simple16X16Board3()
        {
            //Arrange
            string expression = "0000>03?8470<05000=500000>0610000004000000<00@0;010008000029=:0000300:00080000070000<60;00@00014200?0105<004@0090<0@90?2103:600890420@<0005?8000030600500=00000??00000=8710@;0025=0000002;>0000010000047300000<0000000000<?>08610080?0@:06000;00;>00000000:82030";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Simple16X16Board4()
        {
            //Arrange
            string expression = "0006<920170080@5200980>@;:?0000<=0<:00;06000200>00000310@0000?0000>0000000;098007400000000>=005??598000000700;0:;0:00?000000=><04<0002:9830?>0000060070000000900:000=040020>000059?00000<0000200000<0>970003?680003040<2000870;00007;0080=06<:090050306000000000";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Simple16X16Board5()
        {
            //Arrange
            string expression = "000>0000000=:;9607?50900000;0=<0000;04000?0<7000<0:006000000>0?00?700<0190>034:=0000>00:00=0<?10400@0=050:?7060200500?0680<000700000000900400>300200000000010000>00<08;0209000600=@6:>0<0;0?9107:0;8<002@=0900000040000=000:00800100000453000<0000230000048000=0";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }
    }
}

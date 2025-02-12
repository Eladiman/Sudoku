using Sudoku.src.Entities.Models;
using Sudoku.src.Logic;

namespace MySudokuTests._16X16Boards
{
    [TestClass]
    public class Hard16X16Boards
    {
        [TestMethod]
        public void Hard16X16Board1()
        {
            //Arrange
            string expression = "1000000<0050:200004?09>00000@70000750004>0198000000000@724000>6100807530:24000>66190000000700:00?:0061008<;00@7050000?020>60000;000980;=00074?:00?02>009=00;03@0000@00?00006;00<00=800000004000>0>00=80;700@240004000000;=0000000700:200000000=80<00300000000000";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Hard16X16Board2()
        {
            //Arrange
            string expression = "10023400<06000700080007003009:6;0<00:0010=0;00>0300?200>000900<0=000800:0<201?000;76000@000?005=000:05?0040800;0@0059<00100000800200000=00<580030=00?0300>80@000580010002000=9?000<406@0=00700050300<0006004;00@0700@050>0010020;1?900=002000>000>000;0200=3500<";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Hard16X16Board3()
        {
            //Arrange
            string expression = "030000;062<000@0001;:620@80>700400020@8900035=109>0030000;0=0000=000620080003?05:00008000030000<0@000450;<0000090?4500000900>@0004000<:10>600870000000060008040=0200070050?010<0@873050?0:000200;<060000300000=020>070?0000500:60000501000;020000001006;00098030";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Hard16X16Board4()
        {
            //Arrange
            string expression = "0000000004;000000000000000300000;=00000@20000000000600000000200?0500000?000007@090<7000=005@0060000000000000900300007000902001=00<090000=00000:0000000000000000600000000000000000000000080005000000400800010000;51000;00000000<00300005000000000<0000403?;70000>";

            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Hard16X16Board5()
        {
            //Arrange
            string expression = "0500030000192000:000000000004100001900?000>;<00060001900?0=00>;5980100:0;57>000030@04190:00=000;>0000<309000?0=00:?0500006@0800000;060000000:?20200?;50><00@08010004020:0;0060<3<00@00000:?00000070;000<41900:0002=00007@000100400080?000>0536000<0008000=00>000";
            //Act
            Board board = new Board(expression);
            bool solved = BoardSolver.SolveBoard(board);

            //Assert
            Assert.IsTrue(solved);
        }
    }
}

namespace SudokuTests.SmallBoards
{
    public class SimpleSmallSudokuTests
    {

        [Fact]
        public void SimpleSmallSudoku1()
        {
            string expression = "005300000800000020070010500400005300010070006003200080060500009004000030000009700";

            BaseTests.CheckSudokuSolver(expression);
        }

        [Fact]
        public void SimpleSmallSudoku2()
        {
            string expression = "000000000000003085001020000000507000004000100090000000500000073002010000000040009";

            BaseTests.CheckSudokuSolver(expression);
        }

        [Fact]
        public void SimpleSmallSudoku3()
        {
            string expression = "000006000059000008200008000045000000003000000006003054000325006000000000000000000";

            BaseTests.CheckSudokuSolver(expression);
        }

        [Fact]
        public void SimpleSmallSudoku4()
        {
            string expression = "900800000000000500000000000020010003010000060000400070708600000000030100400000200";

            BaseTests.CheckSudokuSolver(expression);
        }

        [Fact]
        public void SimpleSmallSudoku5()
        {
            string expression = "800000070006010053040600000000080400003000700020005038000000800004050061900002000";

            BaseTests.CheckSudokuSolver(expression);
        }
    }
}

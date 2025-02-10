namespace SudokuTests.SmallBoards
{
    public class HardSmallSudokuTests
    {
        [Fact]
        public void HardSmallSudoku1()
        {
            string expression = "001000007000890000000000600260030000000500074900000000001040508300000000000000200";

            BaseTests.CheckSudokuSolver(expression);
        }

        [Fact]
        public void HardSmallSudoku2()
        {
            string expression = "307040000000000918000000004000007000001600000002500000000003800900005000206000000";

            BaseTests.CheckSudokuSolver(expression);
        }

        [Fact]
        public void HardSmallSudoku3()
        {
            string expression = "000000410900300000300050000048007000000000062010000000600200005070000800000090000";

            BaseTests.CheckSudokuSolver(expression);
        }

        [Fact]
        public void HardSmallSudoku4()
        {
            string expression = "609000008000701000400000000000006000402000003003000050001050007080009000000000200";

            BaseTests.CheckSudokuSolver(expression);
        }

        [Fact]
        public void HardSmallSudoku5()
        {
            string expression = "805000002000901000300000000607004002000500000000000600003800000100009000400000070";

            BaseTests.CheckSudokuSolver(expression);
        }
    }
}

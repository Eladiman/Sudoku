namespace SudokuTests.SmallBoards
{
    public class MediumSmallSudokuTests
    {
        [Fact]
        public void MediumSmallSudoku1()
        {
            string expression = "090300020000070500001000000070860040000090200050000000000010006340008000000000000";

            BaseTests.CheckSudokuSolver(expression);
        }

        [Fact]
        public void MediumSmallSudoku2()
        {
            string expression = "080000063000040200000000000001080350070000090000060000020907000000000003540000000";

            BaseTests.CheckSudokuSolver(expression);
        }

        [Fact]
        public void MediumSmallSudoku3()
        {
            string expression = "200040500001000003000000000000600008020703090000001000004000506000007000900080000";

            BaseTests.CheckSudokuSolver(expression);
        }

        [Fact]
        public void MediumSmallSudoku4()
        {
            string expression = "704000002000801000300000000506001002000400000000000900003700000900005000800000060";

            BaseTests.CheckSudokuSolver(expression);
        }

        [Fact]
        public void MediumSmallSudoku5()
        {
            string expression = "000040001030600000800000000109005000000000870000200000070000260500094000000000300";

            BaseTests.CheckSudokuSolver(expression);
        }
    }
}

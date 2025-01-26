﻿using Sudoku.src.Consts;
using Sudoku.src.Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
namespace Sudoku.src.Logic
{
    public static class Validation
    {
        public static void CheckLength(string expression)
        {
            if (expression == null) throw new SyntaxException("Expression can't be empty!");
            if (expression.Length != SudokuConstants.Board_size * SudokuConstants.Board_size)
                throw new SyntaxException($"expression Length is not Valid! should be: {SudokuConstants.Board_size * SudokuConstants.Board_size}!");
        }
        public static void CheckNumber(string expression)
        {
            foreach (char number in expression)
            {
                if (!char.IsDigit(number))
                    throw new SyntaxException($"{number} is not a valid parameter in the Sudoku (should be contained only numbers)!");
            }
        }
        public static void CheckNumberCount(string expression)
        {
            int[] numberCountArray = new int[SudokuConstants.Board_size - 1];
            int indexOfNumber = 0;
            //fill counter array
            foreach (char number in expression)
            {
                if (number != '0')
                {
                    numberCountArray[number - '1']++;
                }
            }
            //a specific number appear more then boars size times
            foreach (int counter in numberCountArray)
            {
                if (counter > SudokuConstants.Board_size)
                    throw new LogicalException($"The number {indexOfNumber + 1} appeared more then {SudokuConstants.Board_size} times. Board is not solvable!");
                indexOfNumber++;
            }
        }


    }
}
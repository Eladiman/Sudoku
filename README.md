
# Elad's Sudoku

Hello! Welcome to Elad's amazing Sudoku!
This is a special Sudoku solver capable of handling varying board sizes (from 1x1 to 25x25) at remarkably fast speeds!



## Features

- Can solve any board from 1x1 to 25x25 - boards must have an integer length that is a power of a number between 1 and 5.
- For unsolvable boards, returns an appropriate message.
- Capable of handling invalid boards (incorrect length/invalid characters).
- Capable of reading and writing from both files and command-line interface (CLI).


## Main Algorithm - Optimizations

The algorithm is a smart algorithm that combines both recursion (BackTracking) and various heuristics to guide the computer very quickly towards the solution!

**Main Heuristics**

 - Basic Heuristic - Goes through all the filled cells which did not visited them and     Removes the possibility that they will exist from the row, column and box where they are. 
 
 - Hidden Single - Goes through all the empty cells and searches for each row column and box. if there is a cell that has a number that the other cells in the row/column/box do not have. and if so then it adds the cell to the full cells.
 
 - Hidden Pairs - Goes through all the empty cells and searches for each row column and box. if there is 2 cells that has 2 numbers that the other cells in the row/column/box do not have. and if so then it remove the other possibilities from those 2 cells.
    

If none of these heuristics managed to return a solution to the board, then recursion will be used, which takes the cell with the fewest options and begins (by using backtracking), to go through its options while reactivating the heuristics.


## Installation - Run Guide

In order to install your Sudoku, you need to follow these steps:
- Navigate to the folder where you want to store the project.
- clone the project using this command:
```bash
  git clone https://github.com/Eladiman/Sudoku.git
```
- Navigate to the Project Folder and open solution file.
- Now Run the project and enjoy my sudoku ;) 

Here is an example of the project when runing the project and enter a sudoku to solve:
![Image](https://github.com/user-attachments/assets/45c52602-2900-4a69-bb2e-0372d93015cf)

    
## Running Tests

To run tests, do the following steps:
- Press `Test` then `Run All Tests`
After that, the following window should be opened:
![Image](https://github.com/user-attachments/assets/7c0fc206-3323-41a4-a7b0-d76de712c2c5)


## Special Notes

- To maximize the Sudoku solving capabilities, you need to run the project in release mode.
- When choosing the option to load Sudoku from a file (by pressing 2), make sure the correct path is entered and that the file contains only one Sudoku puzzle with no additional information. Additionally, the solution will be printed on the screen and also saved in the given file."

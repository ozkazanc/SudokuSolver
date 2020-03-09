using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Program
    {
        static int[,] board = new int[9, 9];
        static int totalSolutions = 0;

        static void Main(string[] args)
        {
            Console.Write("Sudoku Solver\n\n");

            InitBoard();
            if (!IsValidBoard()) {
                Console.WriteLine("The given board is not valid.");
                Console.ReadLine();
                return;
            }

            PrintBoard();
            Solve();

            if (totalSolutions == 0)
                Console.WriteLine("\nNo possible solution were found.");

        }
        static void Solve() {
            for (int x = 0; x < 9; x++) {
                for (int y = 0; y < 9; y++) {
                    if (board[x, y] == 0)       // if the position does not have a number
                    {
                        for (int n = 1; n < 10; n++) {
                            if (IsPossible(x, y, n)) {
                                board[x, y] = n;    // put the number there and solve again
                                Solve();
                                board[x, y] = 0;    // backtrack by resetting the position
                            }
                        }
                        return;
                    }
                }
            }

            totalSolutions++;
            Console.WriteLine("\nSolution {0}", totalSolutions);
            PrintBoard();
        }

        static bool IsPossible(int x, int y, int n) {
            // Check the row
            for (int i = 0; i < 9; i++) {
                if (board[x, i] == n)
                    return false;
            }

            // Check the column
            for (int i = 0; i < 9; i++) {
                if (board[i, y] == n)
                    return false;
            }

            // Check the 3x3 square
            int x0 = (x / 3) * 3;
            int y0 = (y / 3) * 3;
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if (board[x0 + i, y0 + j] == n)
                        return false;
                }
            }

            return true;
        }

        static void InitBoard() {
            //board[0, 4] = 2;
            //board[0, 5] = 1;
            //board[0, 8] = 6;
            //board[1, 1] = 7;
            //board[1, 3] = 6;
            //board[2, 2] = 5;
            //board[2, 6] = 1;
            //board[3, 0] = 9;
            //board[3, 4] = 3;
            //board[3, 7] = 2;
            //board[4, 1] = 4;
            //board[4, 2] = 2;
            //board[4, 4] = 5;
            //board[4, 6] = 3;
            //board[4, 7] = 8;
            //board[5, 1] = 6;
            //board[5, 4] = 1;
            //board[5, 8] = 5;
            //board[6, 2] = 4;
            //board[6, 6] = 2;
            //board[7, 5] = 2;
            //board[7, 7] = 3;
            //board[8, 0] = 2;
            //board[8, 3] = 8;
            //board[8, 4] = 9;

            board[0, 0] = 4;
            board[0, 1] = 8;
            board[0, 4] = 5;
            board[0, 6] = 1;
            board[0, 7] = 3;
            board[1, 4] = 1;
            board[1, 5] = 6;
            board[1, 8] = 5;
            board[2, 1] = 5;
            board[2, 5] = 8;
            board[2, 6] = 9;
            board[2, 7] = 2;
            board[3, 0] = 3;
            board[3, 1] = 1;
            board[3, 3] = 8;
            board[4, 2] = 9;
            board[4, 3] = 1;
            board[4, 5] = 7;
            board[4, 6] = 8;
            board[5, 5] = 9;
            board[5, 7] = 1;
            board[5, 8] = 2;
            board[6, 1] = 3;
            board[6, 2] = 1;
            board[6, 3] = 2;
            board[6, 7] = 4;
            board[7, 0] = 9;
            board[7, 3] = 7;
            board[7, 4] = 8;
            board[8, 1] = 2;
            board[8, 2] = 5;
            board[8, 4] = 4;
            board[8, 7] = 7;
            board[8, 8] = 9;
        }

        static bool IsValidBoard() {
            for (int x = 0; x < 9; x++) {
                for (int y = 0; y < 9; y++) {
                    if (board[x, y] != 0)  {
                        int temp = board[x, y];
                        board[x, y] = -1;
                        if(!IsPossible(x, y, temp)){
                            return false;
                        }

                        board[x, y] = temp;
                    }
                }
            }
            return true;
        }

        static void PrintBoard() {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine(); //new line at each row  
            }
        }
    }
}

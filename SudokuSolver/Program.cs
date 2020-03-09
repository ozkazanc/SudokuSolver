using System;
using System.IO;
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

        static readonly string fileName = "solve_this.txt";
        static string path = Path.Combine(Environment.CurrentDirectory, @"..\..\", fileName);

        static void Main(string[] args)
        {
            Console.Write("Sudoku Solver\n\n");

            if (!InitBoard())
                return;

            if (!IsValidBoard())              
                return;
            
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

        static bool InitBoard() {

            if (!File.Exists(path)) {
                Console.WriteLine("Error: The file does not exist\n");
                return false;
            }

            // Read entire text file content in one string    
            string text = File.ReadAllText(path);

            char[] delimiterChars = { ' ', ',', '\r', '\n', '\t' };
            string[] words = text.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            // We need at least 81 number to create a sudoku puzzle
            if (words.Length < 81) {
                Console.WriteLine("Error: The file does not contain a valid sudoku puzzle.\n");
                return false;
            }

            for (int x = 0; x < 9; x++) {
                for (int y = 0; y < 9; y++) {
                    int number = 0;

                    bool isParsable = Int32.TryParse(words[x * 9 + y], out number);

                    if (isParsable)
                        board[x, y] = number;

                    else {
                        Console.WriteLine("Error: The file does not contain a valid sudoku puzzle.\n");
                        return false;
                    }
                }
            }

            return true;  
        }

        static bool IsValidBoard() {
            for (int x = 0; x < 9; x++) {
                for (int y = 0; y < 9; y++) {
                    if (board[x, y] != 0)
                    {
                        // Store the current board number at that position and 
                        // try to insert it at the same position to see if it is valid
                        int temp = board[x, y];
                        board[x, y] = -1;

                        if(!IsPossible(x, y, temp)){
                            Console.WriteLine("The given board is not valid.\n");
                            return false;
                        }

                        board[x, y] = temp;     // restore the given number at the position
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

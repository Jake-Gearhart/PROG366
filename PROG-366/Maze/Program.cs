using System;
using System.Collections.Generic;
using System.Data;

namespace Maze
{
    class Program
    {
        static void Main()
        {
            Maze maze = new();
            maze.AskForAction();
        }
    }

    class Maze
    {
        private char[,] maze = {
            {'█',' ','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█'},
            {'█',' ',' ',' ','█',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','█'},
            {'█',' ',' ',' ','█',' ',' ',' ',' ','█',' ',' ',' ','█',' ','█','█','█',' ','█'},
            {'█',' ','█',' ','█','█','█','█','█','█',' ',' ','█','█',' ','█',' ','█',' ','█'},
            {'█',' ','█',' ',' ',' ',' ','█',' ',' ',' ',' ','█',' ',' ','█',' ',' ',' ','█'},
            {'█',' ','█','█','█',' ',' ','█',' ',' ',' ','█','█',' ',' ','█',' ','█',' ','█'},
            {'█','█','█',' ',' ',' ',' ','█',' ','█','█','█',' ',' ',' ','█',' ','█','█','█'},
            {'█',' ','█',' ',' ',' ',' ','█','█','█',' ','█',' ',' ',' ','█',' ',' ',' ','█'},
            {'█',' ','█','█','█',' ',' ',' ','█',' ',' ',' ',' ',' ',' ','█',' ',' ',' ','█'},
            {'█',' ','█',' ','█',' ',' ',' ','█',' ',' ',' ','█',' ',' ','█',' ',' ',' ','█'},
            {'█',' ','█',' ','█',' ',' ',' ','█',' ',' ',' ','█','█','█','█','█','█',' ','█'},
            {'█',' ','█',' ',' ',' ',' ','█','█',' ',' ',' ',' ','█',' ',' ',' ','█',' ','█'},
            {'█',' ',' ',' ','█','█','█','█',' ',' ',' ',' ',' ','█',' ',' ',' ','█',' ','█'},
            {'█',' ',' ','█','█',' ',' ','█',' ',' ',' ',' ',' ',' ','█',' ',' ','█',' ','█'},
            {'█',' ','█','█',' ',' ',' ','█',' ',' ',' ',' ',' ',' ','█',' ',' ','█',' ','█'},
            {'█',' ','█',' ',' ',' ',' ','█',' ',' ','█','█','█','█','█',' ',' ',' ',' ','█'},
            {'█',' ',' ',' ',' ',' ',' ','█','█',' ','█',' ',' ',' ',' ',' ','█','█','█','█'},
            {'█',' ','█',' ',' ',' ',' ',' ',' ',' ','█',' ',' ','█','█','█','█',' ',' ','█'},
            {'█',' ','█',' ',' ',' ',' ',' ',' ',' ','█',' ',' ',' ',' ',' ',' ',' ',' ','█'},
            {'█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','X','█'}
        };

        private int playerY = 0;
        private int playerX = 1;

        public void AskForAction ()
        {
            Console.Clear();
            Print();
            Console.WriteLine($"Use W A S D to move.\n");
            ConsoleKeyInfo input = Console.ReadKey();
            if (input.Key == ConsoleKey.W) MoveUp();
            else if (input.Key == ConsoleKey.A) MoveLeft();
            else if (input.Key == ConsoleKey.S) MoveDown();
            else if (input.Key == ConsoleKey.D) MoveRight();
            else {
                AskForAction();
                return;
            }

            if (maze[playerY, playerX] == 'X')
            {
                Console.Clear();
                Print();
                Console.WriteLine("You win!");
            }
            else
            {
                AskForAction();
            }
        }

        private void Move(int y, int x)
        {
            if (y < 0) return;
            if (y > maze.GetLength(0) - 1) return;

            if (x < 0) return;
            if (x > maze.GetLength(1) - 1) return;

            if (maze[y, x] != '█')
            {
                playerY = y;
                playerX = x;
            }
        }

        private void MoveUp()
        {
            Move(playerY - 1, playerX);
        }

        private void MoveLeft()
        {
            Move(playerY, playerX - 1);
        }

        private void MoveDown()
        {
            Move(playerY + 1, playerX);
        }

        private void MoveRight()
        {
            Move(playerY, playerX + 1);
        }

        private void Print ()
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (i == playerY && j == playerX)
                    {
                        Console.Write("😀");
                    }
                    else if (maze[i, j] == '█')
                    {
                        Console.Write("██");
                    }
                    else if (maze[i, j] == 'X')
                    {
                        Console.Write("⛳");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.Write('\n');
            }
            Console.Write('\n');
        }
    }
}
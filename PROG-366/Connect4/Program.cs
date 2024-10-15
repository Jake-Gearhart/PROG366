using System;

namespace Connect4
{
    class Program
    {
        static void Main(string[] args)
        {
            new Game();
        }
    }

    public class Game
    {
        private int[,] gameboard = new int[6, 7];

        public Game()
        {
            Console.WriteLine("Current Board:\n");
            Print();
            AskForAction();
        }

        private int currentPlayer = 1;
        private int piecesOnBoard = 0;

        private void AskForAction()
        {
            Console.WriteLine($"Input column for player {currentPlayer}:");
            int x = Int32.Parse(Console.ReadLine());

            bool isValid = ValidateInput(x);

            if (isValid == false)
            {
                Console.WriteLine("Invalid input.");
                AskForAction();
                return;
            }

            bool continueGame = DropPiece(x);
            if (continueGame == false) return;

            //Console.Clear();
            Console.WriteLine("Current Board:\n");
            Print();
            AskForAction();
        }

        private bool ValidateInput (int x)
        {
            if (x < 0 || x > gameboard.GetLength(0) || gameboard[0, x] != 0)
            {
                return false;
            }

            return true;
        }

        private bool DropPiece(int x)
        {
            for (int i = gameboard.GetLength(0) - 1; i >= 0; i--)
            {
                if (gameboard[i, x] == 0)
                {
                    gameboard[i, x] = currentPlayer;
                    piecesOnBoard++;

                    if (CheckForTie(piecesOnBoard) == true)
                    {
                        Console.WriteLine("The game ends in a tie!\n");
                        Print();
                        return false;
                    }

                    int winner = CheckForWin(currentPlayer, i, x);
                    if (winner != 0)
                    {
                        Console.WriteLine($"{winner} won the game!\n");
                        Print();
                        return false;
                    }

                    if (currentPlayer == 1) currentPlayer = 2;
                    else currentPlayer = 1;
                    return true;
                }
            }

            return true;
        }

        private bool CheckForTie(int pieces)
        {
            if (pieces >= gameboard.GetLength(0) * gameboard.GetLength(1))
            {
                return true;
            }

            return false;
        }

        private int CheckForWin(int player, int y, int x)
        {
            //check vertical
            int matchLength = 1;
            matchLength += GetMatchLength(player, y, +1, x, 0); // down
            if (matchLength >= 4) return player;

            //check horizontal
            matchLength = 1;
            matchLength += GetMatchLength(player, y, 0, x, -1); // left
            matchLength += GetMatchLength(player, y, 0, x, +1); // right
            if (matchLength >= 4) return player;

            //check diagonal downwards (\)
            matchLength = 1;
            matchLength += GetMatchLength(player, y, -1, x, -1); // top-left
            matchLength += GetMatchLength(player, y, +1, x, +1); // bottom-right
            if (matchLength >= 4) return player;

            //check diagonal upwards (/)
            matchLength = 1;
            matchLength += GetMatchLength(player, y, +1, x, -1); // bottom-left
            matchLength += GetMatchLength(player, y, -1, x, +1); // top-right
            if (matchLength >= 4) return player;

            return 0;
        }

        private int GetMatchLength(int player, int y, int y_multiplier, int x, int x_multiplier)
        {
            int matchLength = 0;

            for (int i = 1; i <= 3; i++)
            {
                int y_index = y + i * y_multiplier;
                int x_index = x + i * x_multiplier;

                if (y_index >= 0 && y_index < gameboard.GetLength(0) &&
                    x_index >= 0 && x_index < gameboard.GetLength(1) &&
                    gameboard[y_index, x_index] == player) matchLength++;
                else break;
            }

            return matchLength;
        }

        public void Print()
        {
            for (int i = 0; i < gameboard.GetLength(0); i++)
            {
                for (int j = 0; j < gameboard.GetLength(1); j++)
                {
                    Console.Write(gameboard[i, j] + " ");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }
    }
}

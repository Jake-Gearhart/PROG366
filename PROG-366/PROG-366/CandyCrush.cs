using System;

namespace CandyCrush
{
    class Program
    {
        static void Main()
        {
            Gameboard gameboard = new Gameboard();
        }
    }

    public class Gameboard
    {
        private int[,] gameboard;
        private readonly Random rng = new Random();

        private int RandomNumber()
        {
            return rng.Next(1, 4);
        }

        public Gameboard()
        {
            Console.WriteLine("Input Board Height:");
            int boardHeight = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Input Board Width:");
            int boardWidth = Int32.Parse(Console.ReadLine());

            gameboard = new int[boardHeight, boardWidth];
            Populate();
            ClearMatches();
        }

        private void AskForAction()
        {
            Console.WriteLine("\nCurrent Board:");
            Print();

            Console.WriteLine("Input y coordinate to remove:");
            int y = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Input x coordinate to remove:");
            int x = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Would you like to use a powerup? (Enter \"yes\" to use)");
            string powerup = Console.ReadLine();

            if (powerup == "yes")
            {
                PowerupRemove(y, x);
            }
            else
            {
                Remove(y, x);
            }
        }

        private void Populate()
        {
            for (int i = 0; i < gameboard.GetLength(0); i++)
            {
                for (int j = 0; j < gameboard.GetLength(1); j++)
                {
                    gameboard[i, j] = RandomNumber();
                }
            }

            Console.WriteLine("\nThe board was generated...");
            Print();
        }

        public void Print()
        {
            for (int i = 0; i < gameboard.GetLength(0); i++)
            {
                for (int j = 0; j < gameboard.GetLength(1); j++)
                {
                    Console.Write(gameboard[i, j]);
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        public void Remove(int y, int x)
        {
            gameboard[y, x] = 0;
            Console.WriteLine($"(y:{y}, x:{x}) was removed...");
            Print();
            Fall();
            ClearMatches();
        }

        public void PowerupRemove(int y, int x)
        {
            int powerupValue = gameboard[y, x];
            for (int i = 0; i < gameboard.GetLength(0); i++)
            {
                for (int j = 0; j < gameboard.GetLength(1); j++)
                {
                    if (gameboard[i, j] == powerupValue)
                    {
                        gameboard[i, j] = 0;
                    }
                }
            }
            Console.WriteLine($"All {powerupValue}s were removed...");
            Print();
            Fall();
            ClearMatches();
        }

        public void ClearMatches()
        {
            bool changedGameboard = false;
            for (int i = 0; i < gameboard.GetLength(0); i++)
            {
                bool result = ClearMatchesInRow(i);
                if (result == true) changedGameboard = true;
            }

            if (changedGameboard == true)
            {
                Console.WriteLine("Matches were cleared...");
                Print();
                Fall();
                ClearMatches();
            }
            else
            {
                Console.WriteLine("The board is now stable.");
                AskForAction();
            }
        }

        public bool ClearMatchesInRow(int y)
        {
            bool ProcessSequence(int y, int startX, int length)
            {
                if (length < 3) return false;

                for (int i = 0; i < length; i++)
                {
                    gameboard[y, startX + i] = 0;
                }

                return true;
            }

            int matchLength = 1;
            int currentNumber = gameboard[y, 0];
            bool changedGameboard = false;
            bool result;

            for (int i = 1; i < gameboard.GetLength(1); i++)
            {
                if (gameboard[y, i] == currentNumber)
                {
                    matchLength++;
                }
                else
                {
                    result = ProcessSequence(y, i - matchLength, matchLength);
                    if (result == true) changedGameboard = true;
                    matchLength = 1;
                    currentNumber = gameboard[y, i];
                }
            }

            result = ProcessSequence(y, gameboard.GetLength(1) - matchLength, matchLength);
            if (result == true) changedGameboard = true;

            return changedGameboard;
        }

        public void Fall()
        {
            for (int i = 0; i < gameboard.GetLength(1); i++)
            {
                FallColumn(i);
            }

            Console.WriteLine("Numbers fell...");
            Print();
        }

        public void FallColumn(int x)
        {
            int currentY = gameboard.GetLength(0) - 1;

            for (int i = gameboard.GetLength(0) - 1; i >= 0; i--)
            {
                if (gameboard[i, x] != 0)
                {
                    gameboard[currentY, x] = gameboard[i, x];
                    currentY--;
                }
            }

            while (currentY >= 0)
            {
                gameboard[currentY, x] = RandomNumber();
                currentY--;
            }
        }
    }
}
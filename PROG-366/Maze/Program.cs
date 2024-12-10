namespace Maze
{
    class Program
    {
        static void Main()
        {
            Maze maze = new();
            ManualInputInterface input = new(maze);
            input.AskForAction();
        }
    }

    class ManualInputInterface (Maze maze)
    {
        public void AskForAction()
        {
            Console.Clear();
            maze.Print();
            Console.WriteLine($"Use W A S D to move.\nPress Space to automatically solve from your current position.\n");
            ConsoleKeyInfo input = Console.ReadKey();
            if (input.Key == ConsoleKey.W) maze.MoveUp();
            else if (input.Key == ConsoleKey.A) maze.MoveLeft();
            else if (input.Key == ConsoleKey.S) maze.MoveDown();
            else if (input.Key == ConsoleKey.D) maze.MoveRight();
            else if (input.Key == ConsoleKey.Spacebar) {
                AutomaticSolver solver = new(maze);
                solver.AnimateSolve();
            }
            else {
                AskForAction();
                return;
            }

            if (maze.CheckWin() == true)
            {
                Console.Clear();
                maze.Print();
                Console.WriteLine("You win!");
            }
            else
            {
                AskForAction();
            }
        }
    }

    class AutomaticSolver (Maze maze)
    {
        public void AnimateSolve ()
        {
            List<char>? sequence = DepthFirstSearch(maze.playerY, maze.playerX, [], []);

            if (sequence == null) {
                Console.WriteLine("Maze is unsolveable.");
                return;
            }
            
            foreach (char c in sequence)
            {
                Console.WriteLine(c);
                if (c == 'W') maze.MoveUp();
                else if (c == 'A') maze.MoveLeft();
                else if (c == 'D') maze.MoveRight();
                else maze.MoveDown();
                Console.Clear();
                maze.Print();
                Thread.Sleep(150);
            }
        }

        private List<char>? DepthFirstSearch(int y, int x, HashSet<(int, int)> visited, List<char> path)
        {
            if (maze.IsWinningPosition(y, x)) return path;

            var result = CheckMove(y - 1, x, 'W', visited, path); // Up
            if (result != null) return result;

            result = CheckMove(y, x - 1, 'A', visited, path); // Left
            if (result != null) return result;

            result = CheckMove(y, x + 1, 'D', visited, path); // Right
            if (result != null) return result;

            result = CheckMove(y + 1, x, 'S', visited, path); // Down
            if (result != null) return result;

            return null;
        }

        private List<char>? CheckMove(int y, int x, char direction, HashSet<(int, int)> visited, List<char> path)
        {
            if (visited.Contains((y, x))) return null;
            if (!maze.IsOpenPosition(y, x)) return null;

            visited.Add((y, x));

            List<char> newPath = [..path, direction];

            return DepthFirstSearch(y, x, visited, newPath);
        }
    }

    class Maze
    {
        public readonly char[,] maze = {
            {'█',' ','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█'},
            {'█',' ',' ',' ','█',' ',' ',' ',' ','█',' ',' ',' ',' ',' ',' ',' ',' ',' ','█'},
            {'█',' ','█',' ','█','█','█','█',' ','█',' ','█','█','█',' ','█','█','█',' ','█'},
            {'█',' ','█',' ',' ',' ',' ','█',' ',' ',' ','█',' ','█',' ','█',' ','█',' ','█'},
            {'█','█','█','█','█','█',' ','█','█','█',' ','█',' ','█',' ','█',' ',' ',' ','█'},
            {'█',' ',' ',' ',' ','█',' ','█',' ',' ',' ','█',' ','█',' ','█',' ','█',' ','█'},
            {'█','█','█','█',' ',' ',' ','█',' ','█','█','█',' ','█',' ','█',' ','█',' ','█'},
            {'█',' ','█',' ',' ','█','█','█','█','█',' ','█',' ',' ',' ','█',' ','█',' ','█'},
            {'█',' ','█',' ','█','█',' ',' ',' ','█',' ','█',' ','█',' ','█',' ','█','█','█'},
            {'█',' ','█',' ',' ',' ',' ','█','█','█',' ','█',' ','█',' ','█',' ',' ',' ','█'},
            {'█',' ','█','█','█','█',' ','█',' ','█',' ','█',' ','█','█','█','█','█',' ','█'},
            {'█',' ','█',' ',' ',' ',' ','█',' ','█',' ','█',' ','█',' ',' ',' ','█',' ','█'},
            {'█',' ','█',' ','█','█','█','█',' ','█',' ','█',' ','█','█','█',' ','█',' ','█'},
            {'█',' ','█',' ','█',' ',' ','█',' ','█',' ','█',' ','█',' ','█',' ','█',' ','█'},
            {'█',' ',' ',' ','█','█',' ','█',' ',' ',' ',' ',' ',' ',' ','█',' ','█',' ','█'},
            {'█',' ','█','█','█','█',' ','█','█',' ','█','█','█','█','█','█',' ','█',' ','█'},
            {'█',' ',' ',' ',' ','█',' ',' ',' ',' ','█',' ',' ',' ',' ',' ',' ',' ',' ','█'},
            {'█','█',' ','█',' ','█','█','█','█',' ','█',' ','█','█','█','█','█','█','█','█'},
            {'█',' ',' ','█',' ',' ',' ',' ',' ',' ','█',' ',' ',' ',' ',' ',' ',' ',' ','█'},
            {'█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','X','█'}
        };

        public int playerY = 0;
        public int playerX = 1;

        private void Move(int y, int x)
        {
            if (IsOpenPosition(y, x) == false) return;

            playerY = y;
            playerX = x;
        }

        public bool IsOpenPosition(int y, int x)
        {
            if (y < 0) return false;
            if (y > maze.GetLength(0) - 1) return false;

            if (x < 0) return false;
            if (x > maze.GetLength(1) - 1) return false;

            if (maze[y, x] == '█') return false;

            return true;
        }

        public void MoveUp()
        {
            Move(playerY - 1, playerX);
        }

        public void MoveLeft()
        {
            Move(playerY, playerX - 1);
        }

        public void MoveDown()
        {
            Move(playerY + 1, playerX);
        }

        public void MoveRight()
        {
            Move(playerY, playerX + 1);
        }

        public bool IsWinningPosition (int y, int x)
        {
            if (maze[y, x] == 'X') return true;
            
            return false;
        }

        public bool CheckWin()
        {
            return IsWinningPosition(playerY, playerX);
        }

        public void Print ()
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
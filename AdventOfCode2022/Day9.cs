namespace AdventOfCode2022 {
    internal static class Day9 {
        public static void Run(string path) {
            Console.WriteLine("Day Nine");
            List<string> input = FileHelper.ReadFileAsList(path);
            
            Solve(input, tails: 1);
            Solve(input, tails: 9);
        }

        private static void Solve(List<string> input, int tails) {
            var matrix = new Dictionary<(int, int), bool>();
            var snake = new List<Position>();

            for (int i = 0; i < tails + 1; i++)
                snake.Add(new Position(0, 0));

            foreach(var line in input) {
                string[] splited = line.Split(' ');
                string direction = splited[0];
                int steps = int.Parse(splited[1]);

                for (int step = 0; step < steps; step++) {
                    MoveHead(snake, direction);

                    for (int index = 1; index < snake.Count; index++) {
                        MoveTail(snake, index);

                        if (index == snake.Count - 1)
                            matrix[(snake[index].X, snake[index].Y)] = true;
                    }
                }
            }

            Console.WriteLine(matrix.Count(x => x.Value == true));
        }

        private static void MoveHead(List<Position> snake, string direction) {
            if (direction == "R")
                snake[0].X++;
            else if (direction == "L")
                snake[0].X--;
            else if (direction == "U")
                snake[0].Y++;
            else if (direction == "D")
                snake[0].Y--;
        }

        private static void MoveTail(List<Position> snake, int index) {
            if (RightUp(snake[index - 1], snake[index])) {
                snake[index].X++;
                snake[index].Y++;
            }
            else if (LeftUp(snake[index - 1], snake[index])) {
                snake[index].X--;
                snake[index].Y++;
            }
            else if (RightDown(snake[index - 1], snake[index])) {
                snake[index].X++;
                snake[index].Y--;
            }
            else if (LeftDown(snake[index - 1], snake[index])) {
                snake[index].X--;
                snake[index].Y--;
            }
            else if (Right(snake[index - 1], snake[index]))
                snake[index].X++;
            else if (Left(snake[index - 1], snake[index]))
                snake[index].X--;
            else if (Up(snake[index - 1], snake[index]))
                snake[index].Y++;
            else if (Down(snake[index - 1], snake[index]))
                snake[index].Y--;
        }

        private static bool RightUp(Position head, Position tail)
            => (head.X - tail.X == 1 && head.Y - tail.Y == 2) ||
            (head.X - tail.X == 2 && head.Y - tail.Y == 2) ||
            (head.X - tail.X == 2 && head.Y - tail.Y == 1);

        private static bool LeftUp(Position head, Position tail)
            => (head.X - tail.X == -1 && head.Y - tail.Y == 2) ||
            (head.X - tail.X == -2 && head.Y - tail.Y == 2) ||
            (head.X - tail.X == -2 && head.Y - tail.Y == 1);

        private static bool RightDown(Position head, Position tail)
            => (head.X - tail.X == 1 && head.Y - tail.Y == -2) ||
            (head.X - tail.X == 2 && head.Y - tail.Y == -2) ||
            (head.X - tail.X == 2 && head.Y - tail.Y == -1);

        private static bool LeftDown(Position head, Position tail)
            => (head.X - tail.X == -1 && head.Y - tail.Y == -2) ||
            (head.X - tail.X == -2 && head.Y - tail.Y == -2) ||
            (head.X - tail.X == -2 && head.Y - tail.Y == -1);

        private static bool Right(Position head, Position tail) 
            => head.X - tail.X > 1 && head.Y == tail.Y;

        private static bool Left(Position head, Position tail)
            => head.X - tail.X < -1 && head.Y == tail.Y;

        private static bool Up(Position head, Position tail)
            => head.Y - tail.Y > 1 && head.X == tail.X;

        private static bool Down(Position head, Position tail)
            => head.Y - tail.Y < -1 && head.X == tail.X;

        internal record Position(int X, int Y) {
            public int X { get; set; } = X;
            public int Y { get; set; } = Y;
        }
    }
}
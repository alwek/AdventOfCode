namespace AdventOfCode2022 {
    internal static class Day9 {
        public static void Run(string path) {
            Console.WriteLine("Day Nine");
            List<string> input = FileHelper.ReadFileAsList(path);

            Solve(input);
        }

        private static void Solve(List<string> input) {
            var matrix = new Dictionary<(int, int), bool>();
            var head = new Position(0, 0);
            var tail = new Position(0, 0);

            foreach(var line in input) {
                string[] splited = line.Split(' ');
                string direction = splited[0];
                int steps = int.Parse(splited[1]);

                for (int i = 0; i < steps; i++) {
                    if (direction == "R")
                        head.X++;
                    else if (direction == "L")
                        head.X--;
                    else if (direction == "U")
                        head.Y++;
                    else if (direction == "D")
                        head.Y--;

                    if ((head.X - tail.X == 1 && head.Y - tail.Y == 2) ||
                        (head.X - tail.X == 2 && head.Y - tail.Y == 2) ||
                        (head.X - tail.X == 2 && head.Y - tail.Y == 1)) {
                        tail.X++;
                        tail.Y++;
                    }
                    else if ((head.X - tail.X == -1 && head.Y - tail.Y == 2) ||
                            (head.X - tail.X == -2 && head.Y - tail.Y == 2) ||
                            (head.X - tail.X == -2 && head.Y - tail.Y == 1)) {
                        tail.X--;
                        tail.Y++;
                    }
                    else if ((head.X - tail.X == 1 && head.Y - tail.Y == -2) ||
                            (head.X - tail.X == 2 && head.Y - tail.Y == -2) ||
                            (head.X - tail.X == 2 && head.Y - tail.Y == -1)) {
                        tail.X++;
                        tail.Y--;
                    }
                    else if ((head.X - tail.X == -1 && head.Y - tail.Y == -2) ||
                            (head.X - tail.X == -2 && head.Y - tail.Y == -2) ||
                            (head.X - tail.X == -2 && head.Y - tail.Y == -1)) {
                        tail.X--;
                        tail.Y--;
                    }
                    else if (head.X - tail.X > 1 && head.Y == tail.Y)
                        tail.X++;
                    else if (head.X - tail.X < -1 && head.Y == tail.Y)
                        tail.X--;
                    else if(head.Y - tail.Y > 1 && head.X == tail.X)
                        tail.Y++;
                    else if (head.Y - tail.Y < -1 && head.X == tail.X)
                        tail.Y--;

                    matrix[(tail.X, tail.Y)] = true;
                }
            }

            Console.WriteLine(matrix.Count(x => x.Value == true));
        }

        internal record Position(int X, int Y) {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
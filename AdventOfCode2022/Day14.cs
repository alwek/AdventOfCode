namespace AdventOfCode2022 {
    internal static partial class Day14 {
        public static void Run(string path) {
            Console.WriteLine("Day Fourteen");
            List<string> input = FileHelper.ReadFileAsList(path);

            Solve(input, false);
            Solve(input, true);
        }

        private static void Solve(List<string> input, bool partTwo) {
            List<(int X, int Y)> points = new();
            if(partTwo) input.Add("0,166 -> 1000,166"); // specific to my input

            foreach (string wall in input) {
                string[] splitted = wall.Split(" -> ");
                for(int i = 0; i < splitted.Length - 1; i++) {
                    var first = splitted[i].Split(",");
                    var second = splitted[i + 1].Split(",");
                    int x0 = int.Parse(first[0]), y0 = int.Parse(first[1]);
                    int x1 = int.Parse(second[0]), y1 = int.Parse(second[1]);
                    points.AddRange(BresenhamLineAlgorithm(x0, y0, x1, y1));
                }
            }

            var minX = points.Min(point => point.X);
            var maxX = points.Max(point => point.X);
            var maxY = points.Max(point => point.Y);
            string[,] array = new string[maxY + 1, maxX - minX + 1];

            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    array[i, j] = ".";

            foreach(var (x, y) in points) 
                array[y, x - minX] = "#";
            if (!partTwo) Print(array);

            int sands = 0;
            bool spaceAvailable = true;
            while (spaceAvailable) {
                bool falling = true;
                int x = 0, y = 500 - minX;

                while (falling) {
                    try {
                        if (array[x + 1, y] != "#" && array[x + 1, y] != "o")
                            x++;
                        else if (array[x + 1, y - 1] != "#" && array[x + 1, y - 1] != "o") {
                            x++;
                            y--;
                        }
                        else if (array[x + 1, y + 1] != "#" && array[x + 1, y + 1] != "o") {
                            x++;
                            y++;
                        }
                        else if (array[x, y] == "o") {
                            falling = false;
                            spaceAvailable = false;
                        }
                        else {
                            array[x, y] = "o";
                            sands++;
                            falling = false;
                        }
                    }
                    catch (Exception)
                    {
                        falling = false;
                        spaceAvailable = false;
                    }
                }
            }

            if(!partTwo) Print(array);
            Console.WriteLine(sands);
        }

        private static IEnumerable<(int X, int Y)> BresenhamLineAlgorithm(int x0, int y0, int x1, int y1) {
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;

            while (true) {
                yield return new (x0, y0);

                if (x0 == x1 && y0 == y1)
                    break;

                int e2 = 2 * err;
                if (e2 > -dy) {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx) {
                    err += dx;
                    y0 += sy;
                }
            }
        }

        private static void Print(string[,] array) {
            for (int i = 0; i < array.GetLength(0); i++) {
                for (int j = 0; j < array.GetLength(1); j++)
                    Console.Write(array[i, j]);
                Console.Write("\n");
            }
        }
    }
}
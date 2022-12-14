using System.Drawing;
using System.Runtime.CompilerServices;

namespace AdventOfCode2022 {
    internal static partial class Day14 {
        public static void Run(string path) {
            Console.WriteLine("Day Fourteen");
            List<string> input = FileHelper.ReadFileAsList(path);

            Solve(input, false);
            Solve(input, true);
        }

        private static void Solve(List<string> input, bool partTwo) {
            List<Point> points = new();
            if(partTwo)
                input.Add("0,166 -> 1000,166");

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

            foreach(var point in points) 
                array[point.Y, point.X - minX] = "#";

            if (!partTwo)
                Print(array);

            bool flag = true;
            int sands = 0;
            while (flag) {
                bool canFall = true;
                int x = 0, y = 500 - minX;

                while (canFall) {
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
                        else {
                            if (array[x, y] == "o") {
                                canFall = false;
                                flag = false;
                            }
                            else {
                                array[x, y] = "o";
                                sands++;
                                canFall = false;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        canFall = false;
                        flag = false;
                    }
                }
            }

            if(!partTwo)
                Print(array);
            Console.WriteLine(sands);
        }

        private static void Print(string[,] array) {
            for (int i = 0; i < array.GetLength(0); i++) {
                for (int j = 0; j < array.GetLength(1); j++)
                    Console.Write(array[i, j]);
                Console.Write("\n");
            }
        }

        private static IEnumerable<Point> BresenhamLineAlgorithm(int x0, int y0, int x1, int y1) {
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;

            while (true) {
                yield return new Point(x0, y0);

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
    }
}
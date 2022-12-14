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
                    points.AddRange(GetPointsOnLine(x0, y0, x1, y1));
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

        // Bresenham's Line Algorithm
        // http://ericw.ca/notes/bresenhams-line-algorithm-in-csharp.html 
        public static IEnumerable<Point> GetPointsOnLine(int x0, int y0, int x1, int y1) {
            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep) {
                int t = x0;
                x0 = y0;
                y0 = t;
                t = x1;
                x1 = y1;
                y1 = t;
            }
            if (x0 > x1) {
                int t = x0;
                x0 = x1;
                x1 = t;
                t = y0;
                y0 = y1;
                y1 = t;
            }
            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            int error = dx / 2;
            int ystep = (y0 < y1) ? 1 : -1;
            int y = y0;
            for (int x = x0; x <= x1; x++) {
                yield return new Point((steep ? y : x), (steep ? x : y));
                error -= dy;
                if (error < 0) {
                    y += ystep;
                    error += dx;
                }
            }
            yield break;
        }
    }
}
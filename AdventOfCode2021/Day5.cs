namespace AdventOfCode2021 {
    internal static class Day5 {
        public static void Run(string path) {
            Console.WriteLine("Day 5");
            var input = FileHelper.ReadFileAsList(path);

            Console.WriteLine("Part One");
            Solve(input, false);
            Console.WriteLine("Part Two");
            Solve(input, true);
        }

        private static void Solve(List<string> input, bool diagonal, bool print = false) {
            List<Line> lines = ExtractLines(input);
            int[,] matrix = CreateMatrix(lines);

            foreach (var line in lines) {
                if (line.X1 != line.X2 && line.Y1 != line.Y2 && !diagonal)
                    continue;

                int xIncrement = line.X1 - line.X2 < 0 ? 1 : line.X1 - line.X2 > 0 ? -1 : 0;
                int yIncrement = line.Y1 - line.Y2 < 0 ? 1 : line.Y1 - line.Y2 > 0 ? -1 : 0;

                int xIndex = line.X1, yIndex = line.Y1;
                while (xIndex != line.X2 + xIncrement || yIndex != line.Y2 + yIncrement) {
                    matrix[xIndex, yIndex]++;
                    xIndex += xIncrement;
                    yIndex += yIncrement;
                }
            }

            if(print) PrintMatrix(matrix);
            Console.WriteLine($"Number of dangerous zones: {NumberOfDangerousZones(matrix)}");
        }

        private static void PrintMatrix(int[,] matrix) {
            for (int i = 0; i < matrix.GetLength(1); i++) {
                for (int j = 0; j < matrix.GetLength(0); j++)
                    Console.Write(matrix[j, i]);
                Console.Write("\n");
            }
        }

        private static int NumberOfDangerousZones(int[,] matrix) {
            int count = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] > 1)
                        count++;

            return count;
        }

        private static int[,] CreateMatrix(List<Line> lines) {
            int xMax = lines.SelectMany(line => new[] { line.X1, line.X2 }).Max() + 1;
            int yMax = lines.SelectMany(line => new[] { line.Y1, line.Y2 }).Max() + 1;

            return new int[xMax, yMax];
        }

        private static List<Line> ExtractLines(List<string> input) {
            List<Line> lines = new();
            foreach(var line in input) {
                var firstCoordinates = line.Split(" -> ")[0].Split(',');
                var secondCoordinates = line.Split(" -> ")[1].Split(',');

                lines.Add(new Line(int.Parse(firstCoordinates[0]), 
                    int.Parse(firstCoordinates[1]), 
                    int.Parse(secondCoordinates[0]), 
                    int.Parse(secondCoordinates[1])));
            }

            return lines;
        }
    }

    internal class Line {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }

        public Line(int x1, int y1, int x2, int y2) {
            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x2;
            this.Y2 = y2;
        }
    }
}
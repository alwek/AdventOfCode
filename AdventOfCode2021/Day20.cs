namespace AdventOfCode2021 {
    internal static class Day20 {
        public static void Run(string path) {
            Console.WriteLine("Day 20");
            var input = FileHelper.ReadFileAsList(path).ToArray();

            Console.WriteLine("Part One");
            Solve(input, 2);
            Console.WriteLine("Part Two");
            Solve(input, 50);
        }

        private static void Solve(string[] input, int steps) {
            string algorithm = input[0];
            Dictionary<(int X, int Y), bool> pixels = input[2..]
                .SelectMany((x, i) => x.Select((y, j) => (j, i, y)))
                .ToDictionary(x => (x.j, x.i), x => x.y == '#');
            List<(int x, int y)> neighbours = new() {
                (-1, -1), (0, -1), (1, -1),
                (-1, 0), (0, 0), (1, 0),
                (-1, 1), (0, 1), (1, 1)};

            for(int step = 0; step < steps; step++) {
                var minX = pixels.Keys.Min(x => x.X);
                var minY = pixels.Keys.Min(x => x.Y);
                var maxX = pixels.Keys.Max(x => x.X);
                var maxY = pixels.Keys.Max(x => x.Y);

                Dictionary<(int X, int Y), bool> copy = new();

                for (int y = minY - 1; y <= maxY + 1; y++) {
                    for (int x = minX - 1; x <= maxX + 1; x++) {
                        var adjacents = neighbours
                            .Select(z => (z.x + x, z.y + y))
                            .ToList();
                        var binary = new string(adjacents
                            .Select(z => PixelIsLit(algorithm, pixels, z, minX + 1) ? '1' : '0')
                            .ToArray());
                        copy[(x, y)] = algorithm[Convert.ToInt32(binary, 2)] == '#';
                    }
                }

                pixels = copy;
            }

            Console.WriteLine($"There are {pixels.Count(x => x.Value)} pixels lit");
        }

        private static bool PixelIsLit(string algorithm, Dictionary<(int X, int Y), bool> pixels, (int X, int Y) coord, int minX) 
            => !pixels.ContainsKey(coord) && minX % 2 == 0 && algorithm[0] == '#' 
            || pixels.ContainsKey(coord) && pixels[coord];
    }
}
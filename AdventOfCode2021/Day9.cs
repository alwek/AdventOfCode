namespace AdventOfCode2021 {
    /// <summary>
    /// Contains solutions inspired by external sources.
    /// Code found here is not to be credited to me.
    /// </summary>
    internal static class Day9 {
        private static Dictionary<(int X, int Y), int> input = new();
        private static List<(int X, int Y)> forbidden = new();

        public static void Run(string path) {
            input = FileHelper.ReadFileAsList(path)
                .SelectMany((x, i) => x.Select((y, j) => (j, i, y)))
                .ToDictionary(x => (x.j, x.i), x => x.y - '0');
            forbidden = new() { (-1, 0), (0, 1), (0, -1), (1, 0) };

            Console.WriteLine($"Part One\nSum of lowpoints are {GetSumLowpoints()}");
            Console.WriteLine($"Part Two\nProduct of three largest basins is {GetLargestBasins()}");
        }

        private static int GetSumLowpoints() => 
            GetLowpoints()
            .Select(x => input[x] + 1)
            .Sum();

        private static int GetLargestBasins() => 
            GetLowpoints()
            .Select(x => GetBasins(x))
            .OrderByDescending(x => x)
            .Take(3)
            .Aggregate((x, y) => x * y);

        private static List<(int X, int Y)> GetLowpoints() {
            var lowPoints = new List<(int X, int Y)>();
            foreach (var coordinate in input.Keys) {
                var neighbours = forbidden
                    .Select(neighbour => (neighbour.X + coordinate.X, neighbour.Y + coordinate.Y))
                    .Where(x => input.ContainsKey(x)).ToList();
                
                if (neighbours.All(x => input[coordinate] < input[x]))
                    lowPoints.Add(coordinate);
            }

            return lowPoints;
        }

        private static int GetBasins((int X, int Y) coord) {
            var queue = new Queue<(int X, int Y)>(new[] { coord });
            var visited = new List<(int X, int Y)>();

            while (queue.Count > 0) {
                var current = queue.Dequeue();
                visited.Add(current);

                var neighbours = forbidden
                    .Select(x => (X: x.X + current.X, Y: x.Y + current.Y))
                    .Where(x => input.ContainsKey(x) 
                        && input[x] != 9 
                        && !visited.Contains(x) 
                        && !queue.Contains(x)).ToList();
                neighbours.ForEach(x => queue.Enqueue(x));
            }

            return visited.Count;
        }
    }
}
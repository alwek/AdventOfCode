namespace AdventOfCode2021 {
    internal static class Day11 {
        public static void Run(string path) {
            Console.WriteLine("Day 11");
            var input = FileHelper.ReadFileAsList(path)
                .SelectMany((x, i) => x.Select((y, j) => (j, i, y)))
                .ToDictionary(x => (x.j, x.i), x => x.y - '0');
            
            Console.WriteLine("Part One and Two");
            Solve(input);
        }

        private static void Solve(Dictionary<(int X, int Y), int> input) {
            int steps = 0, flashes = 0;
            bool allFlashed = false;
            List<(int X, int Y)> adjacents = new() { 
                (-1, -1), (0, -1), (1, -1),
                (-1, 0), (1, 0),
                (-1, 1), (0, 1), (1, 1),
            };
            
            while (!allFlashed) {
                foreach (var key in input.Keys.ToList())
                    input[key]++;

                var queue = new Queue<(int X, int Y)>(input.Keys.Where(x => input[x] > 9));
                var visited = new List<(int X, int Y)>();

                while(queue.Count > 0) {
                    var currentKey = queue.Dequeue();
                    visited.Add(currentKey);

                    var adjacentKeys = adjacents
                        .Select(x => 
                            (X: x.X + currentKey.X, 
                            Y: x.Y + currentKey.Y))
                        .Where(x => input.ContainsKey(x)
                            && !visited.Contains(x)
                            && !queue.Contains(x))
                        .ToList();
                    adjacentKeys.ForEach(adjacentKey => input[adjacentKey]++);

                    input
                        .Where(x => 
                            x.Value > 9 && 
                            !visited.Contains(x.Key) && 
                            !queue.Contains(x.Key))
                        .ToList()
                        .ForEach(x => queue
                            .Enqueue(x.Key));
                }

                int flashesThisStep = input.Count(x => x.Value > 9);
                flashes += flashesThisStep;
                foreach (var key in input.Keys.ToList())
                    input[key] = input[key] > 9 ? 0 : input[key];
                steps++;
                allFlashed = flashesThisStep == 100;

                if (steps == 100)
                    Console.WriteLine($"There are {flashes} flashes after {steps} steps");
                if (allFlashed)
                    Console.WriteLine($"All flashed at step {steps}");
            }
        }
    }
}
using System.Collections.Immutable;

namespace AdventOfCode2021 {
    internal class Day12 {
        private static Dictionary<string, string[]> Connections = new();

        public static void Run(string path) {
            Console.WriteLine("Day 12");
            var input = FileHelper.ReadFileAsList(path);

            Console.WriteLine("Part One");
            Solve(input, false);
            Console.WriteLine("Part Two");
            Solve(input, true);
        }

        private static void Solve(List<string> input, bool partTwo) {
            Connections = GetConnections(input);
            int paths = GetNumberOfPaths("start", ImmutableHashSet.Create("start"), false, partTwo);
            Console.WriteLine($"There are {paths} paths");
        }

        private static int GetNumberOfPaths(string currentCave, ImmutableHashSet<string> visitedCaves, 
            bool smallCaveVisitedTwice, bool partTwo) {
            if (currentCave.Equals("end"))
                return 1;

            int res = 0;
            foreach (var cave in Connections[currentCave]) {
                bool isLargeCave = cave.All(c => char.IsUpper(c));
                bool isVisited = visitedCaves.Contains(cave);

                if (!isVisited || isLargeCave)
                    res += GetNumberOfPaths(cave, visitedCaves.Add(cave), smallCaveVisitedTwice, partTwo);
                else if (partTwo && !isLargeCave && cave != "start" && !smallCaveVisitedTwice)
                    res += GetNumberOfPaths(cave, visitedCaves, true, partTwo);
            }

            return res;
        }

        private static Dictionary<string, string[]> GetConnections(List<string> input) {
            List<(string, string)> dict = new();
            foreach (string line in input) {
                string[] splitted = line.Split("-");
                dict.Add((splitted[0], splitted[1]));
                dict.Add((splitted[1], splitted[0]));
            }

            return dict
                .GroupBy(x => x.Item1)
                .ToDictionary(
                    x => x.Key,
                    x => x.Select(y => y.Item2)
                    .ToArray());
        }
    }
}
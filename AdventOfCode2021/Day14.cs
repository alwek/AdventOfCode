using System.Text.RegularExpressions;

namespace AdventOfCode2021 {
    internal class Day14 {
        public static void Run(string path) {
            Console.WriteLine("Day 14");
            var input = FileHelper.ReadFileAsList(path);

            Console.WriteLine("Part One");
            Solve(input, 10);
            Console.WriteLine("Part Two");
            Solve(input, 40);
        }

        private static void Solve(List<string> input, int steps) {
            Dictionary<string, string> combinations = GetCombinations(input);
            string polymer = input[0];

            Dictionary<string, long> pairs = polymer
                .Select((x, i) => i < polymer.Length - 1
                    ? polymer[i..(i + 2)]
                    : string.Empty)
                .Where(x => x != "")
                .Distinct()
                .ToDictionary(
                    x => x, 
                    x => (long)Regex.Matches(polymer, x).Count);

            Dictionary<string, long> characters = polymer
                .Distinct()
                .ToDictionary(
                    x => x.ToString(), 
                    x => (long) polymer.Count(y => y == x));

            for(int step = 0; step < steps; step++) {
                Dictionary<string, long> newPairs = new();
                foreach (string current in pairs.Keys.ToList()) {
                    string left = current[0] + combinations[current];
                    string right = combinations[current] + current[1];

                    newPairs[left] = newPairs.GetValueOrDefault(left, 0) + pairs[current];
                    newPairs[right] = newPairs.GetValueOrDefault(right, 0) + pairs[current];

                    characters[combinations[current]] += pairs[current];
                }

                pairs = newPairs.ToDictionary(x => x.Key, x => x.Value);
            }

            Console.WriteLine($"The sum of after {steps} steps is " +
                $"{characters.Max(x => x.Value) - characters.Min(x => x.Value)}");
        }

        private static Dictionary<string, string> GetCombinations(List<string> input) {
            Dictionary<string, string> combinations = new();
            foreach (string line in input.Skip(2)) {
                string[] splitted = line.Split(" -> ");
                combinations.Add(splitted[0], splitted[1]);
            }

            return combinations;
        }
    }
}
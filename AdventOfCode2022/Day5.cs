using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace AdventOfCode2022 {
    internal static class Day5 {
        public static void Run(string path) {
            Console.WriteLine("Day Five");
            List<string> input = FileHelper.ReadFileAsList(path);

            PartOne(input);
            PartTwo(input);
        }

        private static void PartOne(List<string> input) {
            var index = input.IndexOf(string.Empty);
            var setup = input.Take(index - 1).Reverse();
            var instructions = input.Take(new Range(index + 1, input.Count));
            var positions = Regex.Matches(input[index - 1], "\\d+");
            var stacks = new List<Stack<string>>();

            int max = int.Parse(positions.Last().Value);
            for(int i = 0; i < max; i++)
                stacks.Add(new Stack<string>(100));

            foreach (var line in setup) {
                foreach (Match position in positions.Cast<Match>()) {
                    string character = line[position.Index].ToString();
                    int value = int.Parse(position.Value);

                    if(!string.IsNullOrWhiteSpace(character))
                        stacks[value - 1].Push(character);
                }
            }

            foreach(var instruction in instructions) {
                var match = Regex.Matches(instruction, "\\d+");
                int amount = int.Parse(match[0].Value);
                int from = int.Parse(match[1].Value);
                int to = int.Parse(match[2].Value);

                for (int i = 0; i < amount; i++) {
                    string box = stacks[from - 1].Pop();
                    stacks[to - 1].Push(box);
                }
            }

            var result = string.Concat(stacks.Select(x => x.Peek()));
            Console.WriteLine(result);
        }

        private static void PartTwo(List<string> input) {
            var index = input.IndexOf(string.Empty);
            var numbers = input[index - 1];
            var setup = input.Take(index - 1).Reverse();
            var instructions = input.Take(new Range(index + 1, input.Count));
            var positions = Regex.Matches(numbers, "\\d+");
            var stacks = new List<Stack<string>>();

            int max = int.Parse(positions.Last().Value);
            for (int i = 0; i < max; i++) {
                stacks.Add(new Stack<string>(100));
            }

            foreach (var line in setup) {
                foreach (Match position in positions) {
                    string character = line[position.Index].ToString();
                    int value = int.Parse(position.Value);

                    if (string.IsNullOrWhiteSpace(character))
                        continue;
                    else
                        stacks[value - 1].Push(character);
                }
            }

            foreach (var instruction in instructions) {
                var match = Regex.Matches(instruction, "\\d+");

                int amount = int.Parse(match[0].Value);
                int from = int.Parse(match[1].Value);
                int to = int.Parse(match[2].Value);
                var queue = new Stack<string>();

                for (int i = 0; i < amount; i++) {
                    string box = stacks[from - 1].Pop();
                    queue.Push(box);
                }

                for (int i = 0; i < amount; i++) {
                    string box = queue.Pop();
                    stacks[to - 1].Push(box);
                }
            }

            var result = string.Concat(stacks.Select(x => x.Peek()));
            Console.WriteLine(result);
        }
    }
}
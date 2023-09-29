using System.Text.RegularExpressions;

namespace AdventOfCode2022 {
    internal static class Day5 {
        public static void Run(string path) {
            Console.WriteLine("Day Five");
            List<string> input = FileHelper.ReadFileAsList(path);

            Solve(input, false);
            Solve(input, true);
        }

        private static void Solve(List<string> input, bool isPartTwo) {
            var index = input.IndexOf(string.Empty);
            var setup = input.Take(index - 1).Reverse();
            var instructions = input.Take(new Range(index + 1, input.Count));
            var positions = Regex.Matches(input[index - 1], "\\d+");
            var stacks = new List<Stack<string>>();

            int max = int.Parse(positions.Last().Value);
            for(int i = 0; i < max; i++)
                stacks.Add(new Stack<string>(100));

            foreach (var item in setup) {
                foreach (Match position in positions.Cast<Match>()) {
                    string character = item[position.Index].ToString();
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

                if(isPartTwo)
                    PartTwo(stacks, amount, from, to);
                else
                    PartOne(stacks, amount, from, to);
            }

            var result = string.Concat(stacks.Select(x => x.Peek()));
            Console.WriteLine(result);
        }

        private static void PartTwo(List<Stack<string>> stacks, int amount, int from, int to) {
            var temporary = new Stack<string>();
            for (int i = 0; i < amount; i++) {
                string box = stacks[from - 1].Pop();
                temporary.Push(box);
            }
            for (int i = 0; i < amount; i++) {
                string box = temporary.Pop();
                stacks[to - 1].Push(box);
            }
        }

        private static void PartOne(List<Stack<string>> stacks, int amount, int from, int to) {
            for (int i = 0; i < amount; i++) {
                string box = stacks[from - 1].Pop();
                stacks[to - 1].Push(box);
            }
        }
    }
}
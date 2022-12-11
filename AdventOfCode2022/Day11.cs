using System.Text.RegularExpressions;

namespace AdventOfCode2022 {
    internal static partial class Day11 {
        public static void Run(string path) {
            Console.WriteLine("Day Eleven");
            string input = FileHelper.ReadFileAsString(path);

            Solve(input, 20, 3);
            Solve(input, 10000, 1);
        }

        private static void Solve(string input, int rounds, int level) {
            List<Monkey> monkeys = GetMonkeys(input);

            for(int round = 0; round < rounds; round++) {
                foreach(var monkey in monkeys) {
                    foreach(var item in monkey.Items) {
                        int worry = monkey.Operation(item);
                        worry /= level;
                        bool isDivisible = monkey.Test(worry);

                        if (isDivisible)
                            monkeys.Where(x => x.Id == monkey.ThrowToIfTrue).First().Items.Add(worry);
                        else
                            monkeys.Where(x => x.Id == monkey.ThrowToIfFalse).First().Items.Add(worry);

                        monkey.Inspections++;
                    }

                    monkey.Items.Clear();
                }
            }

            var top = monkeys.OrderBy(x => x.Inspections).TakeLast(2).ToList();
            int first = top.First().Inspections;
            int second = top.Last().Inspections;
            Console.WriteLine(first * second);
        }

        private static List<Monkey> GetMonkeys(string input) {
            List<Monkey> monkeys = new();
            string[] splitted = input.Split("\r\n\r\n");

            foreach (var line in splitted) {
                string[] parts = line.Split("\r\n", StringSplitOptions.TrimEntries);
                int id = int.Parse(Pattern().Match(parts[0]).Value);
                int divisibleBy = int.Parse(Pattern().Match(parts[3]).Value);
                int iftrue = int.Parse(Pattern().Match(parts[4]).Value);
                int iffalse = int.Parse(Pattern().Match(parts[5]).Value);
                List<int> items = Pattern().Matches(parts[1]).Select(x => int.Parse(x.Value)).ToList();

                var part = parts[2].Split(" ");
                string ops = part[^2];
                _ = int.TryParse(Pattern().Match(parts[2]).Value ?? "0", out int operationvalue);

                var objects = Pattern().Matches(parts[1]).Select(x => int.Parse(x.Value)).ToList();

                var monkey = new Monkey(id, divisibleBy, operationvalue, iffalse, iftrue, ops) 
                    { Items = objects };
                monkeys.Add(monkey);
            }

            return monkeys;
        }

        private record Monkey(int Id, int DivisibleByValue, int OperationValue, int ThrowToIfFalse, int ThrowToIfTrue, string Ops) {
            internal List<int> Items { get; set; } = new List<int>();
            internal int Inspections { get; set; }
            internal bool Test(int value) => value % DivisibleByValue == 0;
            internal int Operation(int value) {
                int opvalue = OperationValue == 0 ? value : OperationValue;

                return Ops == "*"
                    ? value * opvalue
                : Ops == "/"
                    ? value / opvalue
                : Ops == "+"
                    ? value + opvalue
                : value - opvalue;
            }
        }

        [GeneratedRegex("\\d+")]
        private static partial Regex Pattern();
    }
}
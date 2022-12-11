using System.Text.RegularExpressions;

namespace AdventOfCode2022 {
    internal static partial class Day11 {
        public static void Run(string path) {
            Console.WriteLine("Day Eleven");
            string input = FileHelper.ReadFileAsString(path);

            Solve(input, 20, 3);
            Solve(input, 10000, 1);
        }

        private static void Solve(string input, long rounds, long level) {
            List<Monkey> monkeys = GetMonkeys(input);
            long supermod = level == 3 ? 1
                : monkeys.Select(x => x.DivisibleByValue).Aggregate((a, b) => a * b);

            for(long round = 0; round < rounds; round++) {
                foreach(var monkey in monkeys) {
                    foreach(var item in monkey.Items) {
                        long worry = monkey.Operation(item);
                        worry = level == 1 
                            ? worry % supermod 
                            : worry / level;

                        if (monkey.Test(worry))
                            monkeys.Where(x => x.Id == monkey.ThrowToIfTrue).First().Items.Add(worry);
                        else
                            monkeys.Where(x => x.Id == monkey.ThrowToIfFalse).First().Items.Add(worry);

                        monkey.Inspections++;
                    }

                    monkey.Items.Clear();
                }
            }

            var monkeybusiness = monkeys
                .OrderBy(x => x.Inspections)
                .TakeLast(2)
                .Select(x => x.Inspections)
                .Aggregate((x, y) => x * y);
            Console.WriteLine(monkeybusiness);
        }

        private static List<Monkey> GetMonkeys(string input) {
            List<Monkey> monkeys = new();
            string[] splitted = input.Split("\n\n");

            foreach (var line in splitted) {
                string[] parts = line.Split("\n", StringSplitOptions.TrimEntries);
                long id = long.Parse(Pattern().Match(parts[0]).Value);
                long divisibleBy = long.Parse(Pattern().Match(parts[3]).Value);
                long iftrue = long.Parse(Pattern().Match(parts[4]).Value);
                long iffalse = long.Parse(Pattern().Match(parts[5]).Value);
                List<long> items = Pattern().Matches(parts[1]).Select(x => long.Parse(x.Value)).ToList();

                var part = parts[2].Split(" ");
                string ops = part[^2];
                int opsvalue = int.TryParse(part[^1], out opsvalue) ? opsvalue : 0;

                var monkey = new Monkey(id, divisibleBy, opsvalue, iffalse, iftrue, ops, items);
                monkeys.Add(monkey);
            }

            return monkeys;
        }

        private record Monkey(long Id, long DivisibleByValue, long OperationValue, long ThrowToIfFalse, 
            long ThrowToIfTrue, string Ops, List<long> Items) {
            internal long Inspections { get; set; }
            internal bool Test(long value) => value % DivisibleByValue == 0;
            internal bool SuperTest(long value) => value % value * DivisibleByValue == 0;
            internal long Operation(long value) {
                long opvalue = OperationValue == 0 ? value : OperationValue;

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
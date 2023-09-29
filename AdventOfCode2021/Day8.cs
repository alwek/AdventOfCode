namespace AdventOfCode2021 {
    internal static class Day8 {
        public static void Run(string path) {
            Console.WriteLine("Day 8");
            var input = FileHelper.ReadFileAsList(path);

            Solve(input);
        }

        private static void Solve(List<string> input) {
            List<int> numbers = new();
            int count = 0;

            foreach(string line in input) {
                string[] splitted = line.Split(" | ");
                List<string> signals = splitted[0].Split(" ").ToList();
                List<string> output = splitted[1].Split(" ").ToList();

                for (int i = 0; i < signals.Count; i++)
                    signals[i] = string.Concat(signals[i].OrderBy(c => c));
                for (int i = 0; i < output.Count; i++)
                    output[i] = string.Concat(output[i].OrderBy(c => c));

                Dictionary<int, string> mappings = new();
                mappings.Add(1, signals.First(x => x.Length == 2));
                mappings.Add(4, signals.First(x => x.Length == 4));
                mappings.Add(7, signals.First(x => x.Length == 3));
                mappings.Add(8, signals.First(x => x.Length == 7));
                mappings.Add(9, signals.First(x => x.Length == 6 && !mappings[4].Any(y => !x.Contains(y))));
                mappings.Add(0, signals.First(x => x.Length == 6 && x != mappings[9] && !mappings[7].Any(y => !x.Contains(y))));
                mappings.Add(6, signals.First(x => x.Length == 6 && x != mappings[0] && x != mappings[9]));
                mappings.Add(3, signals.First(x => x.Length == 5 && !mappings[1].Any(y => !x.Contains(y))));
                mappings.Add(5, signals.First(x => x.Length == 5 && x != mappings[3] 
                    && !x.Contains(mappings[3].First(x => !mappings[6].Contains(x)))));
                mappings.Add(2, signals.First(x => x.Length == 5 && x != mappings[3] 
                    && x.Contains(mappings[3].First(x => !mappings[6].Contains(x)))));

                string number = mappings.First(x => x.Value.Equals(output[0])).Key.ToString()
                    + mappings.First(x => x.Value.Equals(output[1])).Key.ToString()
                    + mappings.First(x => x.Value.Equals(output[2])).Key.ToString()
                    + mappings.First(x => x.Value.Equals(output[3])).Key.ToString();
                numbers.Add(int.Parse(number));

                count += number.Count(x => x.Equals('1') || x.Equals('4') || x.Equals('7') || x.Equals('8'));
            }

            Console.WriteLine("Part One");
            Console.WriteLine($"Values 1, 4, 7 and 8 appears {count} times");
            Console.WriteLine("Part Two");
            Console.WriteLine($"The sum of all outputs are {numbers.Sum()}");
        }
    }
}
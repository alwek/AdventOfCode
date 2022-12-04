namespace AdventOfCode2022 {
    internal static class Day3 {
        public static void Run(string path) {
            Console.WriteLine("Day Three");
            List<string> input = FileHelper.ReadFileAsList(path);

            PartOne(input);
            PartTwo(input);
        }

        private static void PartOne(List<string> input) {
            int count = 0;

            foreach (string item in input) {
                int half = item.Length / 2;
                var first = item.Take(half);
                var second = item.TakeLast(half);

                char letter = first
                    .Where(x => second.Contains(x))
                    .FirstOrDefault();
                count += char.IsLower(letter) ? letter - 96 : letter - 38;
            }

            Console.WriteLine($"Sum of pair values: {count}");
        }

        private static void PartTwo(List<string> input) {
            int count = 0;

            for (int i = 0; i < input.Count - 2; i += 3) {
                var first = input[i];
                var second = input[i + 1];
                var third = input[i + 2];

                char letter = first
                    .Where(x => second.Contains(x) && third.Contains(x))
                    .FirstOrDefault();
                count += char.IsLower(letter) ? letter - 96 : letter - 38;
            }

            Console.WriteLine($"Sum of common values: {count}");
        }
    }
}
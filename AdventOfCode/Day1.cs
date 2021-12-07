namespace AdventOfCode2021 {
    internal static class Day1 {
        internal static void Run(string path) {
            Console.WriteLine("Day One");
            List<int> input = FileHelper.ReadFileAsList(path)
                .Select(x => int.Parse(x)).ToList();

            PartOne(input);
            PartTwo(input);
        }

        private static void PartOne(List<int> input) {
            Console.WriteLine("Part One");

            int increments = 0;
            for (int i = 1; i < input.Count; i++)
                if (input[i] > input[i - 1])
                    increments++;

            Console.WriteLine($"Total of {increments} increments");
        }

        private static void PartTwo(List<int> input) {
            Console.WriteLine("Part Two");

            int increments = 0;
            for (int i = 1; i < input.Count - 2; i++) {
                int a = input[i - 1] + input[i] + input[i + 1];
                int b = input[i] + input[i + 1] + input[i + 2];

                if (b > a)
                    increments++;
            }

            Console.WriteLine($"Total of {increments} increments");
        }
    }
}
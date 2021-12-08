namespace AdventOfCode2021 {
    internal static class Day6 {
        public static void Run(string path) {
            Console.WriteLine("Day 6");
            var input = FileHelper.ReadFileAsString(path);

            Console.WriteLine("Part One");
            Solve(input, 80);
            Console.WriteLine("Part Two");
            Solve(input, 256);
        }

        private static void Solve(string input, int days) {
            long[] lanternfish = new long[9];
            input.Split(',').ToList().ForEach(x => lanternfish[long.Parse(x)]++);

            for (int day = 0; day < days; day++) {
                long count = lanternfish[0];

                for (int i = 1; i < lanternfish.Length; i++)
                    lanternfish[i - 1] = lanternfish[i];

                lanternfish[6] += lanternfish[8] = count;
            }

            Console.WriteLine($"Number of fish after {days} days: {lanternfish.Sum()}");
        }
    }
}
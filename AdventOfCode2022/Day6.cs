namespace AdventOfCode2022 {
    internal static class Day6 {
        public static void Run(string path) {
            Console.WriteLine("Day Six");
            string input = FileHelper.ReadFileAsString(path);

            Solve(input, 4);
            Solve(input, 14);
        }

        private static void Solve(string input, int size) {
            string marker = string.Empty;
            int index = 0;

            for (int i = 0; i < input.Length - size; i++) {
                if (input.Substring(i, size).Distinct().Count() == size) {
                    marker = input.Substring(i, size);
                    index = i + size;
                    break;
                }
            }

            Console.WriteLine($"Index: {index}, Marker: {marker}");
        }
    }
}
namespace AdventOfCode2020 {
    internal static class Day1 {
        internal static void Run(string path) {
            Console.WriteLine("Day One");
            List<int> input = FileHelper.ReadFileAsList(path)
                .Select(int.Parse).ToList(); ;

            PartOne(input);
            PartTwo(input);
        }

        private static void PartOne(List<int> input) {
            Console.WriteLine("Part One");
            bool sumFound = false;

            for (int i = 0; i < input.Count; i++)
            {
                int x = input[i];
                for (int j = i + 1; j < input.Count; j++)
                {
                    int y = input[j];
                    if (x + y == 2020)
                    {
                        Console.WriteLine($"Product: {x * y}");
                        sumFound = true;
                        break;
                    }
                }

                if (sumFound)
                    break;
            }
        }

        private static void PartTwo(List<int> input) {
            Console.WriteLine("Part Two");
            bool sumFound = false;

            for (int i = 0; i < input.Count; i++) {
                int x = input[i];
                for (int j = i + 1; j < input.Count; j++) {
                    int y = input[j];
                    for (int k = j + 1; k < input.Count; k++) {
                        int z = input[k];
                        if (x + y + z == 2020) {
                            Console.WriteLine($"Product: {x * y * z}");
                            sumFound = true;
                            break;
                        }
                    }

                    if (sumFound)
                        break;
                }

                if (sumFound)
                    break;
            }
        }
    }
}
namespace AdventOfCode2020 {
    internal static class Day1 {
        public static void Run(string path) {
            Console.WriteLine("Day One");
            int[] input = FileHelper.ReadFileAsList(path)
                .Select(int.Parse).ToArray();

            PartOne(input);
            PartTwo(input);
        }

        private static void PartOne(int[] input)
        {
            bool sumFound = false;

            for (int i = 0; i < input.Length; i++)
            {
                int x = input[i];
                for (int j = i + 1; j < input.Length; j++)
                {
                    int y = input[j];
                    if (x + y == 2020)
                    {
                        System.Console.WriteLine($"X: {x}");
                        System.Console.WriteLine($"Y: {y}");
                        System.Console.WriteLine($"Sum: {2020}");
                        System.Console.WriteLine($"Product: {x * y}");
                        break;
                    }
                }

                if (sumFound)
                    break;
            }
        }

        private static void PartTwo(int[] input)
        {
            bool sumFound = false;

            for (int i = 0; i < input.Length; i++)
            {
                int x = input[i];
                for (int j = i + 1; j < input.Length; j++)
                {
                    int y = input[j];
                    for (int k = j + 1; k < input.Length; k++)
                    {
                        int z = input[k];
                        if (x + y + z == 2020)
                        {
                            System.Console.WriteLine($"X: {x}");
                            System.Console.WriteLine($"Y: {y}");
                            System.Console.WriteLine($"Z: {z}");
                            System.Console.WriteLine($"Sum: {2020}");
                            System.Console.WriteLine($"Product: {x * y * z}");
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
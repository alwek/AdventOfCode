namespace AdventOfCode2020 {
    internal static class Day3 {
        public static void Run(string path) {
            Console.WriteLine("Day One");
            string[,] input = FileHelper.ReadFileAs2DArray(path);

            PartOne(input);
            PartTwo(input);
        }

        private static void PartOne(string[,] input)
        {
            long treesEncountered = SlopeDown(1, 3, input);
            System.Console.WriteLine($"Trees ecnountered: {treesEncountered}");
        }

        private static void PartTwo(string[,] input)
        {
            long treesEncountered = SlopeDown(1, 3, input);
            treesEncountered *= SlopeDown(1, 1, input);
            treesEncountered *= SlopeDown(1, 5, input);
            treesEncountered *= SlopeDown(1, 7, input);
            treesEncountered *= SlopeDown(2, 1, input);
            System.Console.WriteLine($"Trees ecnountered: {treesEncountered}");
        }

        private static long SlopeDown(int rowMovement, int columnMovement, string[,] input)
        {
            int rows = input.GetLength(0);
            int columns = input.GetLength(1);
            int x = 0, y = 0;
            long treesEncountered = 0;

            while (x < rows - rowMovement)
            {
                x += rowMovement;
                y += columnMovement;

                if (y >= columns)
                    y -= columns;

                if (input[x, y].Equals("#"))
                    treesEncountered++;
            }

            return treesEncountered;
        }
    }
}
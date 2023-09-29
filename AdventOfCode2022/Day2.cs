namespace AdventOfCode2022 {
    internal static class Day2 {
        public static void Run(string path) {
            Console.WriteLine("Day Two");
            List<string> input = FileHelper.ReadFileAsList(path);

            PartOne(input);
            PartTwo(input);
        }

        private static void PartOne(List<string> input) {
            int score = 0;

            foreach (string line in input) { 
                string[] splitted = line.Split(' ');
                string player = splitted[1];
                string opponent = splitted[0];

                score += opponent == "A" && player == "X" ? 4 
                    : opponent == "A" && player == "Y" ? 8
                    : opponent == "A" && player == "Z" ? 3
                    : opponent == "B" && player == "X" ? 1
                    : opponent == "B" && player == "Y" ? 5
                    : opponent == "B" && player == "Z" ? 9
                    : opponent == "C" && player == "X" ? 7
                    : opponent == "C" && player == "Y" ? 2
                    : opponent == "C" && player == "Z" ? 6 
                    : 0;
            }

            Console.WriteLine($"Total score: {score}");
        }

        private static void PartTwo(List<string> input) {
            int score = 0;

            foreach (string line in input) {
                string[] splitted = line.Split(' ');
                string player = splitted[1];
                string opponent = splitted[0];

                score += opponent == "A" && player == "X" ? 3
                    : opponent == "A" && player == "Y" ? 4
                    : opponent == "A" && player == "Z" ? 8
                    : opponent == "B" && player == "X" ? 1
                    : opponent == "B" && player == "Y" ? 5
                    : opponent == "B" && player == "Z" ? 9
                    : opponent == "C" && player == "X" ? 2
                    : opponent == "C" && player == "Y" ? 6
                    : opponent == "C" && player == "Z" ? 7
                    : 0;
            }

            Console.WriteLine($"Total score: {score}");
        }
    }
}
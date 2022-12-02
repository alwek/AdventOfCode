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

                if (opponent == "A" && player == "X") {
                    score += 3;
                }
                else if (opponent == "A" && player == "Y") {
                    score += 6;
                }
                else if (opponent == "A" && player == "Z") {
                    score += 0;
                }
                else if (opponent == "B" && player == "X") {
                    score += 0;
                }
                else if (opponent == "B" && player == "Y") {
                    score += 3;
                }
                else if (opponent == "B" && player == "Z") {
                    score += 6;
                }
                else if (opponent == "C" && player == "X") {
                    score += 6;
                }
                else if (opponent == "C" && player == "Y") {
                    score += 0;
                }
                else if (opponent == "C" && player == "Z") {
                    score += 3;
                }

                score += player == "X" ? 1 : player == "Y" ? 2 : 3;
            }

            Console.WriteLine(score);
        }

        private static void PartTwo(List<string> input) {
            int score = 0;

            foreach (string line in input) {
                string[] splitted = line.Split(' ');
                string player = splitted[1];
                string opponent = splitted[0];

                if (opponent == "A" && player == "X") {
                    score += 3;
                }
                else if (opponent == "A" && player == "Y") {
                    score += 4;
                }
                else if (opponent == "A" && player == "Z") {
                    score += 8;
                }
                else if (opponent == "B" && player == "X") {
                    score += 1;
                }
                else if (opponent == "B" && player == "Y") {
                    score += 5;
                }
                else if (opponent == "B" && player == "Z") {
                    score += 9;
                }
                else if (opponent == "C" && player == "X") {
                    score += 2;
                }
                else if (opponent == "C" && player == "Y") {
                    score += 6;
                }
                else if (opponent == "C" && player == "Z") {
                    score += 7;
                }
            }

            Console.WriteLine(score);
        }
    }
}
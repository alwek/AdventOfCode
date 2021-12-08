namespace AdventOfCode2021 {
    internal static class Day2 {
        public static void Run(string path) {
            Console.WriteLine("Day Two");
            List<string> input = FileHelper.ReadFileAsList(path);

            PartOne(input);
            PartTwo(input);
        }

        private static void PartOne(List<string> input) {
            Console.WriteLine("Part One");
            int horizontal = 0, depth = 0;

            foreach (var line in input) {
                var splitted = line.Split(' ');

                if (splitted[0].Equals("forward"))
                    horizontal += int.Parse(splitted[1]);
                else if(splitted[0].Equals("down"))
                    depth += int.Parse(splitted[1]);
                else if(splitted[0].Equals("up"))
                    depth -= int.Parse(splitted[1]);
                else
                    Console.WriteLine("Unknown operation");
            }

            Console.WriteLine($"Product is {horizontal * depth}");
        }

        private static void PartTwo(List<string> input) {
            Console.WriteLine("Part Two");
            int horizontal = 0, depth = 0, aim = 0;

            foreach (var line in input) {
                var splitted = line.Split(' ');

                if (splitted[0].Equals("forward")) {
                    horizontal += int.Parse(splitted[1]);
                    depth += aim * int.Parse(splitted[1]);
                }
                else if (splitted[0].Equals("down"))
                    aim += int.Parse(splitted[1]);
                else if (splitted[0].Equals("up"))
                    aim -= int.Parse(splitted[1]);
                else
                    Console.WriteLine("Unknown operation");
            }

            Console.WriteLine($"Product is {horizontal * depth}");
        }
    }
}
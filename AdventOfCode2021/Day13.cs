namespace AdventOfCode2021 {
    internal class Day13 {
        public static void Run(string path) {
            Console.WriteLine("Day 13");
            var input = FileHelper.ReadFileAsList(path);

            Console.WriteLine("Part One");
            Solve(input);
        }

        private static void Solve(List<string> input) {
            var coordinates = GetCoordinates(input);
            var instructions = GetInstructions(input);

            foreach(var instruction in instructions) {
                string axis = instruction.Item1;
                int value = instruction.Item2;
                int maxX = coordinates.Max(x => x.x);
                int maxY = coordinates.Max(y => y.y);

                var list = coordinates.Where(x => axis.Equals("x") ? x.x > value : x.y > value).ToList();
                list.ForEach(x => coordinates.Remove(x));
                for(int i = 0; i < list.Count; i++) {
                    if(axis.Equals("x"))
                        list[i] = (Math.Abs(list[i].x - maxX), list[i].y);
                    else
                        list[i] = (list[i].x, Math.Abs(list[i].y - maxY));
                }

                coordinates.AddRange(list);
                coordinates = coordinates.Distinct().ToList();
                Console.WriteLine($"Number of dots: {coordinates.Count}");
            }

            Console.WriteLine("Part Two");
            for(int i = 0; i < coordinates.Max(x => x.y) + 1; i++) {
                for (int j = 0; j < coordinates.Max(y => y.x) + 1; j++)
                    Console.Write(coordinates.Contains((j, i)) ? "#" : ".");
                Console.Write("\n");
            }
        }

        private static List<(int x, int y)> GetCoordinates(List<string> input) {
            List<(int x, int y)> coordinates = new();

            foreach (var line in input) {
                if (string.IsNullOrWhiteSpace(line))
                    break;

                var splitted = line.Split(',');
                coordinates.Add((int.Parse(splitted[0]), int.Parse(splitted[1])));
            }

            return coordinates;
        }

        private static List<(string, int)> GetInstructions(List<string> input) {
            List<(string, int)> instructions = new();

            foreach(var line in input) {
                if (string.IsNullOrWhiteSpace(line) || !line.Contains('='))
                    continue;

                var splitted = line.Split(' ');
                var instruction = splitted[2].Split('=');
                instructions.Add((instruction[0], int.Parse(instruction[1])));
            }

            return instructions;
        }
    }
}
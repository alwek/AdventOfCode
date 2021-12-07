namespace AdventOfCode2021 {
    internal static class Day7 {
        public static void Run(string path) {
            Console.WriteLine("Day 7");
            var input = FileHelper.ReadFileAsString(path);

            Console.WriteLine("Part One");
            Solve(input, true);
            Console.WriteLine("Part Two");
            Solve(input, false);
        }

        private static void Solve(string input, bool partOne) {
            List<int> positions = input.Split(',').Select(int.Parse).ToList();
            int cost = int.MaxValue, currentcost = 0;
            
            foreach (int position in positions) {
                currentcost = positions.Sum(x => partOne
                    ? Math.Abs(x - position)
                    : (Math.Abs(x - position) + 1) * Math.Abs(x - position) / 2);

                cost = currentcost <= cost ? currentcost : cost;
            }

            Console.WriteLine($"Cheapest horizontal position costs: {cost}");
        }
    }
}
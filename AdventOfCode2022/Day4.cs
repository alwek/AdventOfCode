namespace AdventOfCode2022 {
    internal static class Day4 {
        public static void Run(string path) {
            Console.WriteLine("Day Four");
            List<string> input = FileHelper.ReadFileAsList(path);

            Solve(input);
        }

        private static void Solve(List<string> input) {
            int complete = 0;
            int partial = 0;

            foreach (string line in input) {
                string[][] pairs = line.Split(',').Select(x => x.Split('-')).ToArray();
                IEnumerable<int> range1 = Enumerable.Range(int.Parse(pairs[0][0]), 
                    int.Parse(pairs[0][1]) - int.Parse(pairs[0][0]) + 1);
                IEnumerable<int> range2 = Enumerable.Range(int.Parse(pairs[1][0]), 
                    int.Parse(pairs[1][1]) - int.Parse(pairs[1][0]) + 1);

                if (range2.All(x => range1.Contains(x)) || range1.All(x => range2.Contains(x)))
                    complete++;
                if (range1.Intersect(range2).Any())
                    partial++;
            }

            Console.WriteLine($"Complete overlaps: {complete}");
            Console.WriteLine($"Partial overlaps: {partial}");
        }
    }
}
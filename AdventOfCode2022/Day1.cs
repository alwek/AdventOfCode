namespace AdventOfCode2022 {
    internal static class Day1 {
        public static void Run(string path) {
            Console.WriteLine("Day One");
            string input = FileHelper.ReadFileAsString(path);

            Solve(input);
        } 

        private static void Solve(string input) {
            string[] splitted = input.Split("\n\n");
            List<int> calories = new();

            foreach (var item in splitted)
            {
                var calorie = item
                    .Split("\n", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => int.Parse(x))
                    .Sum();

                calories.Add(calorie);  
            }

            Console.WriteLine($"Max calorie: {calories.Max()}");
            Console.WriteLine($"Top three: {calories.OrderByDescending(x => x).Take(3).Sum()}");
        }
    }
}
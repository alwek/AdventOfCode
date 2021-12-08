namespace AdventOfCode2021 {
    internal static class Day3 {
        public static void Run(string path) {
            Console.WriteLine("Day 3");
            var input = FileHelper.ReadFileAsList(path);
            
            PartOne(input);
            PartTwo(input);
        }

        private static void PartOne(List<string> input) {
            Console.WriteLine("Part One");
            Console.WriteLine($"Power consumption is {PowerConsumption(input)}");
        }

        private static void PartTwo(List<string> input) {
            Console.WriteLine("Part Two");
            int oxygenRating = GetOxygenGeneratorRating(input), co2Rating = GetCO2ScrubberRating(input);

            Console.WriteLine($"Life support rating is {oxygenRating * co2Rating}");
        }

        private static int PowerConsumption(List<string> input) {
            string epsilon = "", gamma = "";

            for (int i = 0; i < input[0].Length; i++) {
                int highcount = 0, lowcount = 0;

                for (int j = 0; j < input.Count; j++) {
                    if (input[j][i].Equals('1'))
                        highcount++;
                    else
                        lowcount++;
                }

                epsilon += highcount > lowcount ? "0" : "1";
                gamma += highcount > lowcount ? "1" : "0";
            }

            return Convert.ToInt32(epsilon, 2) * Convert.ToInt32(gamma, 2);
        }

        private static int GetOxygenGeneratorRating(List<string> oxygen) {
            int index = 0, highcount = 0, lowcount = 0;

            while (oxygen.Count != 1) {
                highcount = oxygen.Count(x => x[index].Equals('1'));
                lowcount = oxygen.Count(x => x[index].Equals('0'));

                if (lowcount > highcount)
                    oxygen = oxygen.Where(x => x[index].Equals('0')).ToList();
                else
                    oxygen = oxygen.Where(x => x[index].Equals('1')).ToList();

                index++;
            }

            return Convert.ToInt32(oxygen.First(), 2);
        }

        private static int GetCO2ScrubberRating(List<string> co2) {
            int index = 0, highcount = 0, lowcount = 0;

            while (co2.Count != 1) {
                highcount = co2.Count(x => x[index].Equals('1'));
                lowcount = co2.Count(x => x[index].Equals('0'));

                if (lowcount > highcount)
                    co2 = co2.Where(x => x[index].Equals('1')).ToList();
                else
                    co2 = co2.Where(x => x[index].Equals('0')).ToList();

                index++;
            }

            return Convert.ToInt32(co2.First(), 2);
        }
    }
}
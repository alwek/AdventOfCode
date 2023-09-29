namespace AdventOfCode2020 {
    internal static class Day2 {
        public static void Run(string path) {
            Console.WriteLine("Day One");
            List<string> input = FileHelper.ReadFileAsList(path);

            PartOne(input);
            PartTwo(input);
        }

        private static void PartOne(List<string> input)
        {
            int validPasswords = 0;
            int max, min, count;
            char letter;
            string word;

            foreach (string line in input)
            {
                string[] split = line.Split(new char[] { ' ', '-', ':' }, StringSplitOptions.RemoveEmptyEntries);
                min = int.Parse(split[0]);
                max = int.Parse(split[1]);
                letter = char.Parse(split[2]);
                word = split[3];
                count = word.Count(x => x.Equals(letter));

                if (count >= min && count <= max)
                    validPasswords++;
            }

            System.Console.WriteLine($"Valid passwords: {validPasswords}");
        }

        private static void PartTwo(List<string> input)
        {
            int validPasswords = 0;
            int x, y;
            char letter;
            char[] word;

            foreach (string line in input)
            {
                string[] split = line.Split(new char[] { ' ', '-', ':' }, StringSplitOptions.RemoveEmptyEntries);
                x = int.Parse(split[0]) - 1;
                y = int.Parse(split[1]) - 1;
                letter = char.Parse(split[2]);
                word = split[3].ToArray();

                if ((word[x].Equals(letter) && !word[y].Equals(letter)) || !word[x].Equals(letter) && word[y].Equals(letter))
                    validPasswords++;
            }

            System.Console.WriteLine($"Valid passwords: {validPasswords}");
        }
    }
}
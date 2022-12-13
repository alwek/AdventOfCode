namespace AdventOfCode2022
{
    internal static partial class Day13
    {
        public static void Run(string path)
        {
            Console.WriteLine("Day Thirteen");
            List<string> input = FileHelper.ReadFileAsList(path);

            Solve(input);
        }

        private static void Solve(List<string> input)
        {
            int[][] pairs = input.Where(line => !string.IsNullOrEmpty(line))
                .Select(line => line.Replace("[", ""))
                .Select(line => line.Replace("]", ""))
                .Select(line => line
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray())
                .ToArray();
            List<int> indices = new();
            int index = 1;

            for (int i = 0; i < pairs.Length; i += 2)
            {
                int[] first = pairs[i];
                int[] second = pairs[i + 1];
                int length = first.Length < second.Length ? first.Length : second.Length;
                bool ordered = false, lengthcheck = true;

                for (int j = 0; j < length; j++)
                {
                    if (first[j] == second[j])
                        continue;

                    ordered = first[j] < second[j];
                    lengthcheck = false;
                    break;
                }

                if (lengthcheck)
                    ordered = first.Length < second.Length;
                if (ordered)
                    indices.Add(index);
                index++;
            }

            Console.WriteLine($"Sum is: {indices.Sum()}");
        }
    }
}
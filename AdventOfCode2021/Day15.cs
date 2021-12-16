using System.Collections.Immutable;

namespace AdventOfCode2021
{
    /// <summary>
    /// Partially working
    /// </summary>
    internal class Day15
    {
        private static int cost = int.MaxValue;

        public static void Run(string path)
        {
            Console.WriteLine("Day 15");
            var input = FileHelper.ReadFileAs2DIntArray(path);

            Console.WriteLine("Part One");
            Solve(input);
            Console.WriteLine("Part Two");
        }

        private static void Solve(int[,] input)
        {
            Stack<(int, int)> stack = new();
            Traverse(input, 0, 0, stack);
            cost -= input[0, 0];
            Console.WriteLine("Lowest cost is: " + cost);
        }

        private static void Traverse(int[,] input, int x, int y, Stack<(int, int)> stack)
        {
            stack.Push((x, y));

            if (x == input.GetLength(0) - 1 && y == input.GetLength(1) - 1)
            {
                int value = 0;

                foreach(var point in stack)
                    value += input[point.Item1, point.Item2];

                if (value < cost)
                    cost = value;

                return;
            }

            if (x < input.GetLength(0) - 1)
                Traverse(input, ++x, y, stack);

            if (y < input.GetLength(1) - 1)
                Traverse(input, x, ++y, stack);

            stack.Pop();
        }
    }
}

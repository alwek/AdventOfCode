namespace AdventOfCode2022 {
    internal static partial class Day18 {

        public static void Run(string path) {
            Console.WriteLine("Day Eightteen");
            List<string> input = FileHelper.ReadFileAsList(path);

            Solve(input);
        }

        private static void Solve(List<string> input) {
            List<(int x, int y, int z)> points = new();
            int sides = 0;

            foreach (string line in input) {
                string[] splitted = line.Split(',');
                points.Add((int.Parse(splitted[0]), int.Parse(splitted[1]), int.Parse(splitted[2])));
            }

            foreach (var point in points) {
                var adjacents = Adjacents(point);
                sides += 6 - points.Intersect(adjacents).Count();
            }

            Console.WriteLine(sides);
        }

        private static (int x, int y, int z)[] Adjacents((int x, int y, int z) point) {
            (int x, int y, int z)[] adjacents = new (int x, int y, int z)[6];

            adjacents[0] = (point.x - 1, point.y, point.z);
            adjacents[1] = (point.x + 1, point.y, point.z);
            adjacents[2] = (point.x, point.y - 1, point.z);
            adjacents[3] = (point.x, point.y + 1, point.z);
            adjacents[4] = (point.x, point.y, point.z - 1);
            adjacents[5] = (point.x, point.y, point.z + 1);

            return adjacents;
        }
    }
}
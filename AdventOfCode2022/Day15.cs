namespace AdventOfCode2022 {
    internal static partial class Day15 {
        public static void Run(string path) {
            Console.WriteLine("Day Fifteen");
            List<string> input = FileHelper.ReadFileAsList(path);

            Solve(input);
        }

        private static readonly int Row = 2000000;

        private static void Solve(List<string> input) {
            string[] seperator = new string[] { " ", ",", "=", ":" };
            Dictionary<((long, long), (long, long)), long> beaconsensors = new();
            List<(long x, long y)> points = new();

            foreach (string line in input) {
                string[] splitted = line.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                long sx = long.Parse(splitted[3]);
                long sy = long.Parse(splitted[5]);
                long bx = long.Parse(splitted[11]);
                long by = long.Parse(splitted[13]);
                long radius = Math.Abs(sx - bx) + Math.Abs(sy - by);

                beaconsensors.Add(((sx, sy), (bx, by)), radius);
            }

            foreach (var bs in beaconsensors) {
                points.Add(bs.Key.Item1);
                points.AddRange(PointsInRow(bs.Key.Item1, bs.Value));
                points.Remove(bs.Key.Item2);
            }

            var positions = points.Where(point => point.y == Row).Distinct().Count();
            Console.WriteLine(positions);
        }

        private static IEnumerable<(long x, long y)> PointsInRow((long x, long y) point, long radius) {
            long distance = point.y < Row ? Row - point.y : point.y - Row;
            long peak = radius - distance;
            List<(long x, long y)> points = new();

            for (long i = point.x - peak; i < point.x + peak + 1; i++)
                points.Add((i, Row));

            return points;
        }

        private static IEnumerable<(long x, long y)> PointsInRadius((long x, long y) point, long radius) {
            bool peak = false;
            long i = 0;

            for (long x = point.x - radius; x < point.x + radius; x++) {
                for (long y = point.y - i; y < point.y + i; y++)
                    yield return (x, y);

                if (i == radius)
                    peak = true;

                if (peak)
                    i--;
                else
                    i++;
            }
        }
    }
}
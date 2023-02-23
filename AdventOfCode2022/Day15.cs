namespace AdventOfCode2022 {
    internal static partial class Day15 {
        public static void Run(string path) {
            Console.WriteLine("Day Fifteen");
            List<string> input = FileHelper.ReadFileAsList(path);

            Solve(input);
        }

        private const int Max = 4000000;
        private static readonly int Row = 2000000;

        private static void Solve(List<string> input) {
            string[] seperator = new string[] { " ", ",", "=", ":" };
            Dictionary<((long, long), (long, long)), long> beaconsensors = new();
            List<(long x, long y)> points = new();
            List<(long x, long y)> perimeter = new();
            List<(long x, long y)> available = new();

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
                points.AddRange(PointsOnRow(bs.Key.Item1, bs.Value));
                points.Remove(bs.Key.Item2);

                perimeter.AddRange(PointsOnPerimeter(bs.Key.Item1, bs.Value));
            }

            perimeter = perimeter.Where(IsWithinBoundries).Distinct().Order().ToList();

            foreach(var pt in perimeter)
            {
                if (beaconsensors.All(bs => !IsWithinRadius(pt, bs.Key.Item1, bs.Value)))
                    available.Add(pt);
            }

            var positions = points.Where(point => point.y == Row).Distinct().Count();
            Console.WriteLine(positions);
            Console.WriteLine(available.First().x * 4000000 + available.First().y);
        }

        private static IEnumerable<(long x, long y)> PointsOnRow((long x, long y) point, long radius) {
            long distance = point.y < Row ? Row - point.y : point.y - Row;
            long peak = radius - distance;
            List<(long x, long y)> points = new();

            for (long i = point.x - peak; i < point.x + peak + 1; i++)
                points.Add((i, Row));

            return points;
        }

        private static IEnumerable<(long x, long y)> PointsOnPerimeter((long x, long y) point, long radius) {
            (long j1, long j2, long j3, long j4) = (point.y, point.y + radius + 1, point.y, point.y - radius - 1);

            for (long i = point.x - radius - 1; i <= point.x + radius + 1; i++) {
                if (i < point.x) {
                    yield return (i, j1++);
                    yield return (i, j3--);
                }
                else if (i > point.x) {
                    yield return (i, j2--);
                    yield return (i, j4++);
                }
                else {
                    yield return (i, j1++);
                    yield return (i, j3--);
                    yield return (i, j2--);
                    yield return (i, j4++);
                }
            }
        }

        private static IEnumerable<(long x, long y)> PointsOnBorder((long x, long y) point, long radius)
        {
            (long j1, long j2, long j3, long j4) = (point.y, point.y + radius, point.y, point.y - radius);

            for (long i = point.x - radius; i <= point.x + radius; i++)
            {
                if (i < point.x)
                {
                    yield return (i, j1++);
                    yield return (i, j3--);
                }
                else if (i > point.x)
                {
                    yield return (i, j2--);
                    yield return (i, j4++);
                }
                else
                {
                    yield return (i, j1++);
                    yield return (i, j3--);
                    yield return (i, j2--);
                    yield return (i, j4++);
                }
            }
        }

        private static bool IsWithinBoundries((long x, long y) point) => 
            point.x >= 0 && point.x <= Max && point.y >= 0 && point.y <= Max;

        private static bool IsWithinRadius((long x, long y) point, (long x, long y) center, long radius)
        {
            long distance = Math.Abs(point.x - center.x) + Math.Abs(point.y - center.y);
            return distance <= radius;
        }
    }
}
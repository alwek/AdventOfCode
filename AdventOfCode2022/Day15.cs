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
                points.AddRange(PointsInRow(bs.Key.Item1, bs.Value));
                points.Remove(bs.Key.Item2);

                perimeter.AddRange(PointsInRadius(bs.Key.Item1, bs.Value));
            }

            foreach(var bs in beaconsensors) {
                (long x, long y) sensor = bs.Key.Item1;
                var c1 = (sensor.x - bs.Value, sensor.y);
                var c2 = (sensor.x + bs.Value, sensor.y);
                var c3 = (sensor.x, sensor.y - bs.Value);
                var c4 = (sensor.x, sensor.y + bs.Value);

                available.AddRange(perimeter.Where(point => !IsPositionInsideRectangle(c1, c2, c3, c4, point) && IsWithinBoundries(point)));
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

        private static bool IsPositionInsideRectangle(
            (long x, long y) c1, 
            (long x, long y) c2, 
            (long x, long y) c3, 
            (long x, long y) c4, 
            (long x, long y) c5) {

            if (c5.x >= Math.Min(c1.x, c4.x) && c5.x <= Math.Max(c2.x, c3.x)) 
                if (c5.y >= Math.Min(c1.y, c2.y) && c5.y <= Math.Max(c3.y, c4.y)) 
                    return true;

            return false;
        }

        private static bool IsWithinBoundries((long x, long y) point) => 
            point.x >= 0 && point.x <= Max && point.y >= 0 && point.y <= 20;
    }
}
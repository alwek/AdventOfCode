namespace AdventOfCode2022 {
    internal static partial class Day15 {
        public static void Run(string path) {
            Console.WriteLine("Day Fifteen");
            List<string> input = FileHelper.ReadFileAsList(path);

            Solve(input);
        }

        private static int Rows = 10;//2000000;

        private static void Solve(List<string> input) {
            string[] seperator = new string[] { " ", ",", "=", ":" };
            List<BeaconSensor> connections = new();
            List<(long x, long y)> points = new();

            foreach(string line in input) {
                string[] splitted = line.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                long sx = long.Parse(splitted[3]);
                long sy = long.Parse(splitted[5]);
                long bx = long.Parse(splitted[11]);
                long by = long.Parse(splitted[13]);
                long radius = Math.Abs(sx - bx) + Math.Abs(sy - by);

                connections.Add(new(sx, sy, bx, by, radius));
            }

            foreach (var connection in connections) {
                points.Add((connection.Sx, connection.Sy));
                points.Add((connection.Bx, connection.By));
            }

            foreach (var connection in connections.Where(x => x.Radius + x.Sy >= Rows && x.Radius - x.Sy <= Rows))
                foreach ((long x, long y) in connection.PointsInRadius())
                    points.Add((x, y));

            var positions = points.Where(point => point.y == Rows).Distinct().Count();
            Console.WriteLine(positions);
        }

        internal record BeaconSensor(long Sx, long Sy, long Bx, long By, long Radius) {
            public IEnumerable<(long x, long y)> PointsInRadius() {
                bool peak = false;
                long i = 0;

                for(long x = Sx - Radius; x < Sx + Radius; x++) {
                    for (long y = Sy - i; y < Sy + i; y++)
                        if (y == Rows)
                            yield return (x, y);

                    if (i == Radius)
                        peak = true;

                    if (peak)
                        i--;
                    else
                        i++;
                }
            }
        }
    }
}
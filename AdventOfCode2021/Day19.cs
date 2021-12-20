namespace AdventOfCode2021
{
    internal static class Day19
    {
        public static void Run(string path)
        {
            Console.WriteLine("Day 19");
            var input = FileHelper.ReadFileAsList(path);

            Console.WriteLine("Part One");
            Solve(input);
        }

        private static void Solve(List<string> input)
        {
            List<Scanner> scanners = new();
            scanners = ParseScannersAndBeacons(input);

            foreach(Scanner scanner in scanners)
                scanner.Pairs = scanner.Beacons
                    .SelectMany((x, i) => scanner.Beacons
                        .Skip(i + 1), (x, y) => new BeaconPair(x, y))
                    .ToList();

            var match = scanners[0].Pairs.Where(first => scanners[1].Pairs.Any(second => second.Distance == first.Distance)).ToList();
            var list = match.DistinctBy(x => x.Distance).ToList();

            List<BeaconPair> pairs = new();
            scanners.ForEach(scanner => pairs.AddRange(scanner.Pairs));

            var matches = pairs.GroupBy(x => x.Distance).Where(x => x.Count() >= 2).OrderBy(x => x.Key).ToList();
        }

        private static List<Scanner> ParseScannersAndBeacons(List<string> input)
        {
            List<Scanner> scanners = new();
            Scanner scanner = null;

            foreach(var item in input)
            {
                if (item.Contains("scanner"))
                    scanner = new(int.Parse(item.Split(' ')[2]), new());
                else if (string.IsNullOrWhiteSpace(item))
                    scanners.Add(scanner);
                else
                {
                    int[] position = item.Split(',').Select(int.Parse).ToArray();
                    scanner.Beacons.Add(new(position[0], position[1], position[2]));
                }
            }

            return scanners;
        }

        private record Scanner(int Id, List<Beacon> Beacons)
        {
            internal int X { get; set; }
            internal int Y { get; set; }
            internal int Z { get; set; }
            internal List<BeaconPair> Pairs { get; set; }
        }

        private record Beacon(int X, int Y, int Z)
        {

        }

        private record BeaconPair(Beacon First, Beacon Second)
        {
            internal double Distance { get; } = CalculateDistance(First, Second);

            private static double CalculateDistance(Beacon first, Beacon second)
            {
                // 3D distance calculation
                double x1 = first.X, y1 = first.Y, z1 = first.Z;
                double x2 = second.X, y2 = second.Y, z2 = second.Z;

                double xsq = Math.Pow(x2 - x1, 2);
                double ysq = Math.Pow(y2 - y1, 2);
                double zsq = Math.Pow(z2 - z1, 2);

                double d = Math.Sqrt(xsq + ysq + zsq);
                return d;
            }
        }
    }
}
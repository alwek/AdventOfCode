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
            List<Scanner> scanners = ParseScanners(input);
            List<int> distances = new();

            foreach (Scanner scanner in scanners)
            {
                var result = Distances(scanner.Beacons);
                foreach(var item in result)
                    if(!distances.Contains(item))
                        distances.Add(item);
            }

            distances = distances.Distinct().ToList();
            distances.Sort();

            List<int> vs = new();
            vs.Add(distances[0]);
            for (int i = 0; i < distances.Count; i++)
            {
                for (int j = 0; j < distances.Count; j++)
                {
                    if (distances[j] - distances[i] < 2)
                        continue;
                    else if(!vs.Contains(distances[j]))
                        vs.Add(distances[j]);
                }
            }
        }

        private static List<int> Distances(List<Beacon> beacons)
        {
            List<int> distances = new();

            for(int i = 0; i < beacons.Count - 1; i++)
                for(int j = i + 1; j < beacons.Count; j++)
                    distances.Add(CalculateDistance(beacons[i], beacons[j]));
            
            return distances;
        }

        private static int CalculateDistance(Beacon first, Beacon second)
        {
            // 3D distance calculation
            int x1 = first.X, y1 = first.Y, z1 = first.Z;
            int x2 = second.X, y2 = second.Y, z2 = second.Z;

            int xsq = (int)Math.Pow(x2 - x1, 2);
            int ysq = (int)Math.Pow(y2 - y1, 2);
            int zsq = (int)Math.Pow(z2 - z1, 2);

            int d = (int)Math.Sqrt(xsq + ysq + zsq);
            return d;
        }

        private static List<Scanner> ParseScanners(List<string> input)
        {
            List<Scanner> scanners = new();
            Scanner scanner = new();

            foreach(string line in input)
            {
                if (line.Contains("scanner"))
                    scanner = new Scanner();
                else if (string.IsNullOrWhiteSpace(line))
                    scanners.Add(scanner);
                else
                {
                    var splitted = line.Split(',').Select(int.Parse).ToArray();
                    scanner.Beacons.Add(new(splitted[0], splitted[1], splitted[2]));
                }
            }

            return scanners;
        }
    }

    internal class Scanner
    {
        internal List<Beacon> Beacons { get; set; } = new();
    }

    internal class Beacon
    {
        internal int X { get; set; }
        internal int Y { get; set; }
        internal int Z { get; set; }

        public Beacon(int x, int y, int z)
        {
            X = x; 
            Y = y; 
            Z = z; 
        }
    }
}



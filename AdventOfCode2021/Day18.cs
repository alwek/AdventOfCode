using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    internal static class Day18
    {
        private static Dictionary<string, Pair> directpairs = new();
        private static Dictionary<string, Pair> indirectpairs = new();
        private static Dictionary<string, Pair> exdirectpairs = new();
        private static int dpId = 0, ipId = 0, epId = 0;

        public static void Run(string path)
        {
            Console.WriteLine("Day 18");
            var input = FileHelper.ReadFileAsList(path);

            Console.WriteLine("Part One");
            Solve(input);
        }

        private static void Solve(List<string> input)
        {
            List<Pair> pairs = new();
            foreach(var item in input)
                pairs.Add(ExtractPairs(item));

            Pair result = pairs[0];
            foreach(var pair in pairs.Skip(1))
                result = Addition(result, pair);

            Console.WriteLine($"Magnitude of pairs is {result.Magnitude()}");
        }

        private static Pair Addition(Pair left, Pair right)
        {
            Pair combined = new(left, right);
            SetNestedLevelForPair(combined, 0);

            //Explode(combined);

            return combined;
        }

        private static (int left, int right) Explode(Pair combined)
        {
            (int left, int right) result = (-1, -1);

            if(combined.Level != 5)
            {
                if (!combined.LeftIsPair() && !combined.RightIsPair())
                    return result;

                if (combined.LeftIsPair())
                {
                    result = Explode(combined.Left);
                    if (result == (-1, -1))
                        return result;

                    combined.Left = 0;

                    if (!combined.RightIsPair())
                    {
                        combined.Right = result.right;
                        return (result.left, -1);
                    }


                }

                if(combined.RightIsPair())
                    result = Explode(combined.Right);
            }
            else
                return (combined.Left, combined.Right);

            return result;
        }

        private static void SetNestedLevelForPair(Pair pair, int level)
        {
            pair.Level = level;
            level++;

            if(pair.LeftIsPair())
                SetNestedLevelForPair(pair.Left, level);
            if(pair.RightIsPair())
                SetNestedLevelForPair(pair.Right, level);
        }

        private static Pair ExtractPairs(string line)
        {
            directpairs = new();
            indirectpairs = new();
            exdirectpairs = new();
            dpId = 0; ipId = 0; epId = 0;

            line = ExtractDirectPairs(line);
            line = ExtractIndirectPairs(line);
            line = ExtractExdirectPairs(line);
            return exdirectpairs.ContainsKey(line) 
                ? exdirectpairs[line]
                : indirectpairs.ContainsKey(line)
                ? indirectpairs[line]
                : directpairs[line];
        }

        public static string ExtractDirectPairs(string line)
        {
            Regex pairs = new(@"\[[0-9],[0-9]\]");
            Regex values = new(@"[0-9],[0-9]");
            MatchCollection matches = pairs.Matches(line);

            foreach (Match match in matches)
            {
                int[] extracted = values
                    .Match(match.Value)
                    .Value
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray();


                directpairs[dpId + "dp"] = new Pair(extracted[0], extracted[1]);
                line = line.Replace(match.Value, dpId + "dp");
                dpId++;
            }

            return line;
        }

        private static string ExtractIndirectPairs(string line)
        {
            Regex pairs = new(@"\[[0-9],[0-9][a-z][a-z]\]|\[[0-9][a-z][a-z],[0-9]\]|\[[0-9][a-z][a-z],[0-9][a-z][a-z]\]");
            Regex values = new(@"[0-9],[0-9][a-z][a-z]|[0-9][a-z][a-z],[0-9]|[0-9][a-z][a-z],[0-9][a-z][a-z]");
            MatchCollection matches = pairs.Matches(line);

            foreach (Match match in matches)
            {
                string[] extracted = values
                    .Match(match.Value)
                    .Value
                    .Split(',');

                var left = extracted[0].Contains("dp")
                    ? directpairs[extracted[0]]
                    : null;
                var right = extracted[1].Contains("dp") 
                    ? directpairs[extracted[1]]
                    : null;
                indirectpairs[ipId + "ip"] = new Pair(
                    left != null ? left : extracted[0],
                    right != null ? right : extracted[1]);

                line = line.Replace(match.Value, ipId + "ip");
                ipId++;
            }

            return line;
        }

        private static string ExtractExdirectPairs(string line)
        {
            while(line.Length > 0)
            {
                Regex pairs = new(@"\[[0-9][a-z][a-z],[0-9][a-z][a-z]\]|\[[0-9],[0-9][a-z][a-z]\]|\[[0-9][a-z][a-z],[0-9]\]");
                Regex values = new(@"[0-9][a-z][a-z],[0-9][a-z][a-z]|[0-9][a-z][a-z],[0-9]|[0-9],[0-9][a-z][a-z]");
                MatchCollection matches = pairs.Matches(line);

                if(matches.Count == 0)
                    break;

                foreach (Match match in matches)
                {
                    string[] extracted = values
                        .Match(match.Value)
                        .Value
                        .Split(',');

                    var left = extracted[0].Contains("ip")
                        ? indirectpairs[extracted[0]]
                        : extracted[0].Contains("dp")
                        ? directpairs[extracted[0]]
                        : extracted[0].Contains("ep")
                        ? exdirectpairs[extracted[0]]
                        : null;
                    var right = extracted[1].Contains("ip") 
                        ? indirectpairs[extracted[1]] 
                        : extracted[1].Contains("dp") 
                        ? directpairs[extracted[1]]
                        : extracted[1].Contains("ep")
                        ? exdirectpairs[extracted[1]]
                        : null;
                    exdirectpairs[epId + "ep"] = new Pair(
                        left != null ? left : extracted[0], 
                        right != null ? right : extracted[1]);

                    line = line.Replace(match.Value, epId + "ep");
                    epId++;
                }
            }

            return line;
        }

        private record Pair
        {
            internal Pair(dynamic left, dynamic right)
            {
                this.Left = left;
                this.Right = right;
            }

            internal dynamic Left { get; set; }

            internal dynamic Right { get; set; }

            internal int Level { get; set; }

            internal bool LeftIsPair() => Left is Pair;

            internal bool RightIsPair() => Right is Pair;

            internal int Magnitude()
            {
                if (LeftIsPair() && RightIsPair())
                    return Left.Magnitude() * 3 + Right.Magnitude() * 2;
                else if (LeftIsPair() && !RightIsPair())
                    return Left.Magnitude() * 3 + Convert.ChangeType(Right, typeof(int)) * 2;
                else if (!LeftIsPair() && RightIsPair())
                    return Convert.ChangeType(Left, typeof(int)) * 3 + Right.Magnitude() * 2;
                else
                    return Convert.ChangeType(Left, typeof(int)) * 3 + Convert.ChangeType(Right, typeof(int)) * 2;
            }
        }
    }
}
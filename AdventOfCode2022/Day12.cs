namespace AdventOfCode2022
{
    internal static partial class Day12
    {
        public static void Run(string path)
        {
            Console.WriteLine("Day Twelve");
            List<string> input = FileHelper.ReadFileAsList(path);

            int partOne = int.MaxValue;
            for (int i = 0; i < input.Count; i++)
                for (int j = 0; j < input[i].Length; j++)
                    if (input[i][j] == 'S')
                        partOne = Solve(input, (j, i));
            Console.WriteLine(partOne);

            int partTwo = int.MaxValue;
            List<(int x, int y)> list = new();
            input.ForEach(x => x = x.Replace('S', 'a'));
            for (int i = 0; i < input.Count; i++)
                for (int j = 0; j < input[i].Length; j++)
                    if (input[i][j] == 'a')
                        list.Add((j, i));

            foreach(var start in list) {
                int steps = Solve(input, start);
                partTwo = steps < partTwo ? steps : partTwo;
            }
            Console.WriteLine(partTwo);
        }

        private static int Solve(List<string> map, (int x, int y) startpoint)
        {
            var start = new Tile {
                Y = startpoint.y,
                X = startpoint.x
            };
            map[start.Y] = map[start.Y].Replace('S', 'a');

            var end = new Tile {
                Y = map.FindIndex(x => x.Contains('E'))
            };
            end.X = map[end.Y].IndexOf("E");
            //map[end.Y] = map[end.Y].Replace('E', '{');

            start.SetDistance(end.X, end.Y);

            var active = new List<Tile>();
            var visited = new List<Tile>();
            active.Add(start);

            while (active.Any()) {
                var current = active.OrderBy(x => x.CostDistance).First();

                if (current.X == end.X && current.Y == end.Y)
                    return BacktrackPath(current);

                visited.Add(current);
                active.Remove(current);

                var walkable = GetWalkableTiles(map, current, end);

                foreach (var tile in walkable) {
                    if (visited.Any(x => x.X == tile.X && x.Y == tile.Y))
                        continue;

                    if (active.Any(x => x.X == tile.X && x.Y == tile.Y)) {
                        var existing = active.First(x => x.X == tile.X && x.Y == tile.Y);
                        if (existing.CostDistance > current.CostDistance) {
                            active.Remove(existing);
                            active.Add(tile);
                        }
                    }
                    else
                        active.Add(tile);
                }
            }

            return int.MaxValue;
        }

        private static int BacktrackPath(Tile current) {
            int steps = 0;
            while (true) {
                steps++;
                current = current.Parent;

                if (current == null) 
                    return steps - 1;
            }
        }

        private static List<Tile> GetWalkableTiles(List<string> map, Tile currentTile, Tile targetTile) {
            var possibleTiles = new List<Tile>() {
                new Tile { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1},
                new Tile { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
            };
            possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

            var maxX = map.First().Length - 1;
            var maxY = map.Count - 1;

            return possibleTiles
                .Where(tile => tile.X >= 0 && tile.X <= maxX)
                .Where(tile => tile.Y >= 0 && tile.Y <= maxY)
                .Where(tile =>
                    (map[tile.Y][tile.X] == 'E' && map[currentTile.Y][currentTile.X] == 'z') || 
                    (char.IsLower(map[tile.Y][tile.X]) && map[tile.Y][tile.X] - map[currentTile.Y][currentTile.X] <= 1))
                .ToList();
        }
    }

    internal class Tile {
        public int X { get; set; }
        public int Y { get; set; }
        public int Cost { get; set; }
        public int Distance { get; set; }
        public Tile Parent { get; set; }

        public int CostDistance => Cost + Distance;

        public void SetDistance(int targetX, int targetY) => 
            Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
    }
}
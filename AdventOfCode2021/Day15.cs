using Graph = System.Collections.Generic.Dictionary<(int, int), Node>;
/// <summary>
/// Not my credit
/// </summary>
internal static class Day15
{
    private static (int, int)[] adjacent = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

    public static void Run(string path)
    {
        var input = File.ReadAllLines(path)
            .SelectMany((line, y) => line.Select((c, x) => ((x, y), new Node(x, y, c - '0'))))
            .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);

        // Part 1
        var width = (int)Math.Sqrt(input.Count);
        Console.WriteLine(Dijkstra(input, input[(width - 1, width - 1)]));

        // Part 2
        var quintupleInput =
            Enumerable.Range(0, 5).SelectMany(i =>
                Enumerable.Range(0, 5).SelectMany(j =>
                    input.Select(kvp =>
                    {
                        (int x, int y) newKey = (kvp.Key.x + width * i, kvp.Key.y + width * j);
                        var newRisk = (kvp.Value.Risk + i + j - 1) % 9 + 1;
                        return (newKey, new Node(newKey.x, newKey.y, newRisk));
                    })))
            .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);

        Console.WriteLine(Dijkstra(quintupleInput, quintupleInput[(5 * width - 1, 5 * width - 1)]));
    }

    private static IEnumerable<Node> GetNeighbors(this Graph graph, Node node)
    {
        foreach ((int i, int j) in adjacent)
        {
            var key = (node.X + i, node.Y + j);
            if (graph.ContainsKey(key) && !graph[key].Visited)
            {
                yield return graph[key];
            }
        }
    }

    private static int Dijkstra(Graph graph, Node target)
    {
        var next = new PriorityQueue<Node, int>();
        graph[(0, 0)].Distance = 0;
        next.Enqueue(graph[(0, 0)], 0);
        while (next.Count > 0)
        {
            var current = next.Dequeue();
            if (current.Visited)
            {
                continue;
            }

            current.Visited = true;

            if (current == target)
            {
                return target.Distance;
            }

            foreach (var neighbor in graph.GetNeighbors(current))
            {
                var alt = current.Distance + neighbor.Risk;
                if (alt < neighbor.Distance)
                {
                    neighbor.Distance = alt;
                }

                if (neighbor.Distance != int.MaxValue)
                {
                    next.Enqueue(neighbor, neighbor.Distance);
                }
            }
        }

        return target.Distance;
    }
}

internal class Node
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Risk { get; set; }
    public int Distance { get; set; } = int.MaxValue;
    public bool Visited { get; set; } = false;

    public Node(int x, int y, int risk)
    {
        X = x;
        Y = y;
        Risk = risk;
    }
}
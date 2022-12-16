namespace AdventOfCode2022 {
    internal static partial class Day16 {
        private static int moves = 30;
        private static int pressure = 0;

        public static void Run(string path) {
            Console.WriteLine("Day Sixteen");
            List<string> input = FileHelper.ReadFileAsList(path);

            Solve(input);
        }

        private static void Solve(List<string> input) {
            List<Valve> valves = new();
            Dictionary<string, string[]> neighbours = new();
            string[] seperator = new string[] { " ", "=", ";", "," };

            foreach (string line in input) {
                string[] splitted = line.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                string name = splitted[1];
                int flowrate = int.Parse(splitted[5]);
                string[] tunnels = new string[] { splitted[^1], splitted[^2], splitted[^3] };

                valves.Add(new Valve(name, flowrate, new List<Valve>()));
                neighbours.Add(name, tunnels);
            }

            foreach(var neighbour in neighbours) {
                var valve = valves.First(x => x.Name == neighbour.Key);
                var tunnels = valves.Where(x => neighbour.Value.Contains(x.Name)).ToList();
                valve.Tunnels.AddRange(tunnels);
            }

            DFS(valves.First());
            Console.WriteLine(pressure);
        }

        internal static void DFS(Valve startNode) {
            // Mark the start node as visited
            startNode.Visited = true;

            moves--;
            if (moves <= 0)
                return;

            // Open the start node's valve
            OpenValve(startNode);

            // Recursively visit all of the start node's neighbors
            foreach (Valve neighbor in startNode.Tunnels)
                if (!neighbor.Visited)
                    DFS(neighbor);

        }

        internal static void OpenValve(Valve valve) {
            // Open the valve and release pressure according to its flow rate
            // Update a total pressure released counter
            if(valve.FlowRate > 0) {
                moves--;
                pressure += valve.FlowRate * moves;
            }
        }
    }

    internal class Valve {
        public string Name { get; set; }
        public int FlowRate { get; set; }
        public bool Visited { get; set; }
        public List<Valve> Tunnels { get; set; }

        public Valve(string name, int flowRate, List<Valve> tunnels) {
            Name = name;
            FlowRate = flowRate;
            Tunnels = tunnels;
        }
    }
}
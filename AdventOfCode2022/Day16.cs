namespace AdventOfCode2022 {
    internal static partial class Day16 {
        public static void Run(string path) {
            Console.WriteLine("Day Sixteen");
            List<string> input = FileHelper.ReadFileAsList(path);
            
            Solve(input);
        }

        private static void Solve(List<string> input)
        {
            string[] seperator = new string[] { " ", "=", ";", "," };
            WeightedGraph<string> graph = new(input.Count);
            Dictionary<string, int> indices = Indices(input);

            for(int i = 0; i < input.Count; i++) {
                string[] splitted = input[i].Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                string name = splitted[1];
                int flowrate = int.Parse(splitted[5]);

                graph.AddVertex(i, name, flowrate);

                for(int j = 10; j < splitted.Length; j++)
                    graph.AddEdge(i, indices[splitted[j]], 1);
            }

            graph.PrintGraph();
        }

        private static Dictionary<string, int> Indices(List<string> input)
        {
            Dictionary<string, int> indices = new();

            for(int i = 0; i < input.Count; i++)
            {
                string[] splitted = input[i].Split(' ');
                indices.Add(splitted[1], i);
            }

            return indices;
        }
    }

    internal class Vertex<T>
    {
        public T Value { get; set; }
        public int Flowrate { get; set; }
        public bool Opened { get; set; }

        public Vertex(T value, int flowrate)
        {
            Value = value;
            Flowrate = flowrate;
            Opened = false;
        }
    }

    internal class WeightedGraph<T>
    {
        public readonly int Count;
        public readonly List<(int, int)>[] Adjacents;
        public readonly Vertex<T>[] Vertices;

        public WeightedGraph(int count) {
            Count = count;
            Adjacents = new List<(int, int)>[Count];
            Vertices = new Vertex<T>[Count];

            for (int i = 0; i < Count; i++)
                Adjacents[i] = new List<(int, int)>();
        }

        public void AddVertex(int index, T value, int flowrate) => 
            Vertices[index] = new Vertex<T>(value, flowrate);

        public void AddEdge(int u, int v, int weight) => 
            Adjacents[u].Add((v, weight));

        public void PrintGraph() {
            for (int i = 0; i < Count; i++) {
                Console.Write("Vertex " + i + " (" + Vertices[i].Value + "): ");

                foreach ((int, int) edge in Adjacents[i])
                    Console.Write("(" + Vertices[edge.Item1].Value + ", " + edge.Item2 + ") ");

                Console.WriteLine();
            }
        }
    }
}
namespace AdventOfCode2022 {
    internal static class Day10 {
        public static void Run(string path) {
            Console.WriteLine("Day Ten");
            List<string> input = FileHelper.ReadFileAsList(path);

            Solve(input);
        }

        private static void Solve(List<string> input) {
            int register = 1;
            int cycle = 1;
            string[,] crt = new string[6, 40];
            var signalstrengths = new Dictionary<int, int>() { 
                { 20, 0 }, { 60, 0 }, { 100, 0 }, { 140, 0 }, { 180, 0 }, { 220, 0 }, 
            };

            foreach (string line in input) {
                string[] splitted = line.Split(' ');
                string operation = splitted[0];
                int time = operation == "noop" ? 1 : 2;
                int value = 0;

                if (operation == "addx")
                    value = int.Parse(splitted[1]);

                for (int start = cycle; cycle < start + time; cycle++) {
                    int row = (cycle - 1) / 40;
                    int column = (cycle - 1) % 40;

                    if (register == column || register == (column - 1) || register == (column + 1))
                        crt[row, column] = "#";
                    else
                        crt[row, column] = ".";

                    if (signalstrengths.ContainsKey(cycle))
                        signalstrengths[cycle] = register * cycle;

                    if (operation == "addx" && cycle == start + time - 1)
                        register += value;
                }
            }

            foreach(var signal in signalstrengths)
                Console.WriteLine($"{signal.Key}th cycle: {signal.Value}");
            Console.WriteLine($"Signal sum: {signalstrengths.Sum(x => x.Value)}");

            for(int i = 0; i < crt.GetLength(0); i++) {
                for (int j = 0; j < crt.GetLength(1); j++)
                    Console.Write(crt[i, j]);
                Console.Write("\n");
            }
        }
    }
}
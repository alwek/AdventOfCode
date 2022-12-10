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
            var history = new Dictionary<int, int>();
            var signalstrengths = new List<int>() { 20, 60, 100,  140, 180, 220};
            string[,] crt = new string[6, 40];

            foreach (string line in input) {
                string[] splitted = line.Split(' ');
                string operation = splitted[0];
                int time = operation == "noop" ? 1 : 2;
                int value = 0;

                if (operation == "addx")
                    value = int.Parse(splitted[1]);

                for (int i = cycle; cycle < i + time; cycle++) {
                    int row = (cycle - 1) / 40;
                    int col = (cycle - 1) % 40;

                    if (register == col || register == (col - 1) || register == (col + 1))
                        crt[row, col] = "#";
                    else
                        crt[row, col] = ".";

                    if (signalstrengths.Contains(cycle))
                        history.Add(cycle, register * cycle);

                    if (operation == "addx" && cycle == i + time - 1)
                        register += value;
                }
            }

            foreach(var item in history)
                Console.WriteLine($"{item.Key}th cycle: {item.Value}");
            Console.WriteLine(history.Sum(x => x.Value));

            for(int i = 0; i < crt.GetLength(0); i++) {
                for (int j = 0; j < crt.GetLength(1); j++) {
                    Console.Write(crt[i, j]);
                }
                Console.Write("\n");
            }
        }
    }
}

namespace AdventOfCode2021 {
    internal class Day21 {
        public static void Run(string path) {
            Console.WriteLine("Day 21");
            var input = FileHelper.ReadFileAsList(path);

            Console.WriteLine("Part One");
            Solve(input);
        }

        private static void Solve(List<string> input) {
            (int position, int score)[] players = { 
                (int.Parse(input[0][^2] + input[0][^1].ToString()), 0), 
                (int.Parse(input[1][^2] + input[1][^1].ToString()), 0) };
            int dice = 0;

            while (players[0].score < 1000 && players[1].score < 1000) {
                dice += 3;
                int steps = (((dice - 2) % 10) + ((dice - 1) % 10) + (dice % 10)) % 10;
                int position = players[(dice - 1) % 2].position + steps;

                players[(dice - 1) % 2].position = position > 10 ? position % 10 : position;
                players[(dice - 1) % 2].score += players[(dice - 1) % 2].position;
            }

            Console.WriteLine($"The product is {players.First(player => player.score < 1000).score * dice}");
        }
    }
}
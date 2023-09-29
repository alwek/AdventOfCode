namespace AdventOfCode2021 {
    internal class Day21 {
        private static List<long>[] PlayerWins = new List<long>[2] {new(), new()};

        public static void Run(string path) {
            Console.WriteLine("Day 21");
            var input = FileHelper.ReadFileAsList(path);

            Console.WriteLine("Part One");
            PartOne(input);
            Console.WriteLine("Part Two");
            PartTwo(input);
        }

        private static void PartOne(List<string> input) {
            (int position, int score)[] players = { 
                (int.Parse(input[0][^2..]), 0), 
                (int.Parse(input[1][^2..]), 0) };
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

        private static void PartTwo(List<string> input) {
            (long position, long score)[] players = {
                (int.Parse(input[0][^2..]), 0),
                (int.Parse(input[1][^2..]), 0) };
            var (p1, p2) = QuantumDice(players, 0);
            Console.WriteLine($"Player one wins {p1} times and player two wins {p2} times");
        }

        private static (long p1, long p2) QuantumDice((long position, long score)[] players, int turn) {
            if (players[0].score >= 21)
                return (1, 0);
            else if (players[1].score >= 21)
                return (0, 1);

            var newPlayers1 = new (long position, long score)[2] { players[0], players[1] };
            newPlayers1[turn].position = (newPlayers1[turn].position + 1) % 10 == 0 
                ? newPlayers1[turn].position 
                : (newPlayers1[turn].position + 1) % 10;
            newPlayers1[turn].score += newPlayers1[turn].position;

            var newPlayers2 = new (long position, long score)[2] { players[0], players[1] };
            newPlayers2[turn].position = (newPlayers2[turn].position + 2) % 10 == 0
                ? newPlayers2[turn].position
                : (newPlayers2[turn].position + 2) % 10;
            newPlayers2[turn].score += newPlayers2[turn].position;

            var newPlayers3 = new (long position, long score)[2] { players[0], players[1] };
            newPlayers3[turn].position = (newPlayers3[turn].position + 3) % 10 == 0
                ? newPlayers3[turn].position
                : (newPlayers3[turn].position + 3) % 10;
            newPlayers3[turn].score += newPlayers3[turn].position;

            turn++;
            var result1 = QuantumDice(newPlayers1, turn % 2);
            var result2 = QuantumDice(newPlayers2, turn % 2);
            var result3 = QuantumDice(newPlayers3, turn % 2);

            return (result1.p1 + result2.p1 + result3.p1, result1.p2 + result2.p2 + result3.p2);
        }
    }
}
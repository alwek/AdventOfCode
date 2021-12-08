namespace AdventOfCode2021 {
    internal static class Day4 {
        public static void Run(string path) {
            Console.WriteLine("Day 4");
            var input = FileHelper.ReadFileAsList(path);

            Console.WriteLine("Part One and Two");
            List<int> draws = ExtractDraws(ref input);
            List<Board> boards = ExtractBoards(ref input);
            int place = 1;

            foreach (var draw in draws) {
                foreach (var board in boards)
                    if (board.HasNode(draw))
                        if (board.HasWon())
                            Console.WriteLine($"#{place++} winner board score is: {board.GetScore(draw)}");

                boards = boards.Where(x => !x.HasWon()).ToList();
            }
        }

        private static List<int> ExtractDraws(ref List<string> input) {
            List<int> values = input[0].Split(',').Select(x => int.Parse(x)).ToList();
            input.RemoveRange(0, 2); // We don't need these lines in input anymore
            return values;
        }

        private static List<Board> ExtractBoards(ref List<string> input) {
            List<Board> boards = new();
            List<int[]> rows = new();
            input.Add(""); // We need to add the last delimiter

            foreach (var line in input) {
                if (string.IsNullOrWhiteSpace(line)) {
                    int[,] table = new int[rows.Count, rows[0].Length];
                    for (int i = 0; i < rows.Count; i++)
                        for (int j = 0; j < rows[i].Length; j++)
                            table[i, j] = rows[i][j];

                    boards.Add(new Board(table));
                    rows = new();
                }
                else {
                    var splitted = line.Split(' ');
                    var trimmed = splitted.Select(x => x.Trim());
                    var cleared = trimmed.Where(x => !string.IsNullOrWhiteSpace(x));
                    var parsed = cleared.Select(x => int.Parse(x));
                    rows.Add(parsed.ToArray());
                }
            }

            return boards;
        }
    }

    internal class Board {
        private Node[,] Table { get; set; }

        public Board(int[,] table) {
            Table = CreateTable(table);
        }

        public bool HasWon() => RowComplete() || ColumnComplete();

        private Node[] GetColumn(int columnNumber) => Enumerable.Range(0, Table.GetLength(0))
                    .Select(x => Table[x, columnNumber])
                    .ToArray();

        private Node[] GetRow(int rowNumber) => Enumerable.Range(0, Table.GetLength(1))
                    .Select(x => Table[rowNumber, x])
                    .ToArray();

        public bool HasNode(int value) {
            for (int i = 0; i < Table.GetLength(0); i++)
                for(int j = 0; j < Table.GetLength(1); j++)
                    if(Table[i, j].Value == value) {
                        Table[i, j].Marked = true;
                        return true;
                    }

            return false;
        }

        private bool RowComplete() {
            for (int i = 0; i < Table.GetLength(0); i++)
                if (GetRow(i).All(x => x.Marked))
                    return true;

            return false;
        }

        private bool ColumnComplete() {
            for (int i = 0; i < Table.GetLength(1); i++)
                if (GetColumn(i).All(x => x.Marked))
                    return true;
            
            return false;
        }

        private static Node[,] CreateTable(int[,] values) {
            var table = new Node[values.GetLength(0), values.GetLength(1)];
            for(int i = 0; i < values.GetLength(0); i++)
                for (int j = 0; j < values.GetLength(1); j++)
                    table[i, j] = new Node(values[i, j], false);
            
            return table;
        }

        public int GetScore(int draw) {
            int score = 0;
            for(int i = 0; i < Table.GetLength(0); i++)
                score += GetRow(i).Where(x => !x.Marked).Select(x => x.Value).Sum();

            return score * draw;
        }
    }

    internal class Node {
        public int Value { get; set; }
        public bool Marked { get; set; }

        public Node(int value, bool marked) {
            this.Value = value;
            this.Marked = marked;
        }
    }
}
namespace AdventOfCode2022 {
    internal static partial class Day20 {

        public static void Run(string path) {
            Console.WriteLine("Day Twenty");
            List<string> input = FileHelper.ReadFileAsList(path);

            Solve(input);
        }

        private static void Solve(List<string> input) {
            List<int> sequence = input.Select(int.Parse).ToList();
            List<int> order = new(sequence);
            int mod = sequence.Count;

            foreach (var item in order) {
                int index = sequence.IndexOf(item);
                //int newIndex = item >= 0
                //    ? Modulo(item + index, mod)
                //    : Modulo(item + index, mod) + index - 2;
                int newIndex = Modulo(item + index, mod);
                //newIndex = newIndex == 0 ? 6 : newIndex;

                sequence.RemoveAt(index);
                sequence.Insert(newIndex, item);
            }

            int start = sequence.IndexOf(0);
            int sum = sequence[(1000 + start) % mod] 
                + sequence[(2000 + start) % mod] 
                + sequence[(3000 + start) % mod];
            Console.WriteLine(sum);
        }

        private static int Modulo(int dividend, int divisor) {
            int remainder = dividend % divisor;
            return dividend > divisor 
                ? remainder + 1 
                : remainder < 0 
                ? remainder + divisor 
                : remainder;
        }
    }
}
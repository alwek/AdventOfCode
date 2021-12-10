namespace AdventOfCode2021 {
    internal static class Day10 {
        public static void Run(string path) {
            Console.WriteLine("Day 10");
            List<string> input = FileHelper.ReadFileAsList(path);

            Solve(input);
        }

        private static void Solve(List<string> input) {
            int corruptPoints = 0;
            List<long> incompletePoints = new();
            Dictionary<char, char> bracketPairs = new() {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' },
                { '<', '>' },
            };

            foreach(string line in input) {
                Stack<char> brackets = new();
                bool incomplete = true;

                foreach (char character in line) {
                    if(bracketPairs.ContainsKey(character))
                        brackets.Push(character);
                    else {
                        char openingBracket = brackets.Pop();
                        if (!bracketPairs[openingBracket].Equals(character)) {
                            corruptPoints += CalculateCorruptPoints(character);
                            incomplete = false;
                        } 
                    }
                }

                if (incomplete) 
                    incompletePoints.Add(CalculateIncompletePoints(brackets));
            }

            incompletePoints.Sort();
            Console.WriteLine("Part One");
            Console.WriteLine($"Sum of corrupt lines is {corruptPoints}");
            Console.WriteLine("Part Two");
            Console.WriteLine($"Middle score of incomplete lines is {incompletePoints[incompletePoints.Count / 2]}");
        }

        private static int CalculateCorruptPoints(char character) => 
            character.Equals(')') ? 3 : 
            character.Equals(']') ? 57 : 
            character.Equals('}') ? 1197 : 
            25137;

        private static long CalculateIncompletePoints(Stack<char> brackets){
            long points = 0;
            while(brackets.Count > 0) {
                char character = brackets.Pop();
                points = points * 5 + (character.Equals('(') ? 1 :
                    character.Equals('[') ? 2 :
                    character.Equals('{') ? 3 : 4);
            }

            return points;
        }
    }
}
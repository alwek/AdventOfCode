using System.Text.RegularExpressions;

namespace AdventOfCode2021 {
    internal static class Day22 {
        internal static void Run(string path) {
            Console.WriteLine("Day 22");
            List<string> input = FileHelper.ReadFileAsList(path);

            Console.WriteLine("Part One");
            Solve(input, false);
            Console.WriteLine("Part Two");
            Solve(input, true);
        }

        private static void Solve(List<string> input, bool partTwo) {
            List<Instruction> instructions = ParseInstructions(input);
            Dictionary<(int x, int y, int z), bool> cube = new();
            instructions = !partTwo ? instructions.Where(i => !i.AxisOutsideInitArea).ToList() : instructions;

            foreach (Instruction instruction in instructions)
                for(int i = instruction.FromX; i < (instruction.NegativeXDirection ? instruction.ToX - 1 : instruction.ToX + 1); i = instruction.NegativeXDirection ? i - 1 : i + 1)
                    for(int j = instruction.FromY; j < (instruction.NegativeYDirection ? instruction.ToY - 1 : instruction.ToY + 1); j = instruction.NegativeYDirection ? j - 1 : j + 1)
                        for(int k = instruction.FromZ; k < (instruction.NegativeZDirection ? instruction.ToZ - 1 : instruction.ToZ + 1); k = instruction.NegativeZDirection ? k - 1 : k + 1)
                            cube[(i, j, k)] = instruction.TurnOn;

            Console.WriteLine($"Cubes turned on is {cube.Count(c => c.Value)}");
        }

        private static List<Instruction> ParseInstructions(List<string> input) {
            List<Instruction> instructions = new();

            foreach (var instruction in input) {
                bool turnOn = instruction.StartsWith("on");
                MatchCollection matches = Regex.Matches(instruction, @"-?\b\d+");
                instructions.Add(new Instruction(turnOn,
                    int.Parse(matches[0].Value),
                    int.Parse(matches[1].Value),
                    int.Parse(matches[2].Value),
                    int.Parse(matches[3].Value),
                    int.Parse(matches[4].Value),
                    int.Parse(matches[5].Value)));
            }

            return instructions;
        }

        private record Instruction(bool TurnOn, int FromX, int ToX, int FromY, int ToY, int FromZ, int ToZ) {
            internal bool AxisOutsideInitArea {
                get {
                    return ToX > 50 || ToX < -50 || FromX > 50 || FromX < -50 ||
                        ToY > 50 || ToY < -50 || FromY > 50 || FromY < -50 ||
                        ToZ > 50 || ToY < -50 || FromY > 50 || FromY < -50;
                }
            }

            internal bool NegativeXDirection { get { return ToX < FromX; } }
            internal bool NegativeYDirection { get { return ToY < FromY; } }
            internal bool NegativeZDirection { get { return ToZ < FromZ; } }
        }
    }
}
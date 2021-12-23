using System.Text.RegularExpressions;

namespace AdventOfCode2021 {
    internal static class Day22 {
        internal static void Run(string path) {
            Console.WriteLine("Day 22");
            List<string> input = FileHelper.ReadFileAsList(path);

            Console.WriteLine("Part One");
            PartOne(input, false);
            Console.WriteLine("Part Two");
            PartTwo(input);
        }

        private static void PartOne(List<string> input, bool partTwo) {
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

        private static void PartTwo(List<string> input)
        {
            List<Instruction> instructions = ParseInstructions(input);
            List<Instruction> overlaps = new();

            foreach (var b1 in instructions)
            {
                overlaps.AddRange(
                    overlaps
                        .Select(b2 => Combine(b1, b2))
                        .Where(b => Overlaps(b))
                        .ToList());

                if (b1.TurnOn)
                    overlaps.Add(b1);
            }

            long sum = overlaps.Sum(b => Volume(b) * (b.TurnOn ? 1 : -1));
            Console.WriteLine(sum);
        }

        private static bool Overlaps(Instruction instruction) => 
            instruction.FromX <= instruction.ToX 
            && instruction.FromY <= instruction.ToY 
            && instruction.FromZ <= instruction.ToZ;

        private static long Volume(Instruction instruction) =>
            (instruction.ToX - instruction.FromX + 1L)
            * (instruction.ToY - instruction.FromY + 1)
            * (instruction.ToZ - instruction.FromZ + 1);

        private static Instruction Combine(Instruction first, Instruction second) => 
            new(!second.TurnOn,
                Math.Min(first.FromX, second.FromX),
                Math.Max(first.ToX, second.ToX),
                Math.Min(first.FromY, second.FromY),
                Math.Max(first.ToY, second.ToY),
                Math.Min(first.FromZ, second.FromZ),
                Math.Max(first.ToZ, second.ToZ));

        private static List<Instruction> ParseInstructions(List<string> input)
        {
            List<Instruction> instructions = new();
            foreach (var instruction in input)
            {
                bool turnOn = instruction.StartsWith("on");
                var matches = Regex.Matches(instruction, @"-?\b\d+")
                    .Select(x => int.Parse(x.Value)).ToList();

                instructions.Add(new Instruction(turnOn,
                    matches.GetRange(0, 2).Min(),
                    matches.GetRange(0, 2).Max(),
                    matches.GetRange(2, 2).Min(),
                    matches.GetRange(2, 2).Max(),
                    matches.GetRange(4, 2).Min(),
                    matches.GetRange(4, 2).Max()));
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
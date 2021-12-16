namespace AdventOfCode2021 {
    internal class Day16 {
		public static void Run(string path) {
			Console.WriteLine("Day 16");
			var input = FileHelper.ReadFileAsString(path);

			Solve(input);
		}

		private static void Solve(string input) {
			string bits = new("");
			foreach (char bit in input)
				bits += Hex.GetValueOrDefault(bit);

			(Packet packet, int _) = Parse(bits);

            Console.WriteLine("Part One");
			Console.WriteLine(packet.SumVersions());
            Console.WriteLine("Part Two");
			Console.WriteLine(packet.Evaluate());
		}

		private static (Packet packet, int offset) Parse(string bits) {
			int index = 0;
            List<Packet> subPackets = new();

			int version = Convert.ToInt32(bits[0..(index += 3)], 2);
			int typeId = Convert.ToInt32(bits[index..(index += 3)], 2);

			if (typeId == 4)
				return LiteralValue(bits, ref index, version, typeId);

			int lengthTypeId = Convert.ToInt32(bits[index..(index += 1)], 2);
			if(lengthTypeId == 0)
				index = TotalLengthInBits(ref bits, ref index, ref subPackets);
			else
				index = NumberOfSubPackets(ref bits, ref index, ref subPackets);

            Packet packet = new(version, typeId, 0, subPackets);
			return (packet, index);
		}

		private static (Packet packet, int offset) LiteralValue(string bits, ref int index, int version, int typeId) {
			string literalBits = "";
			foreach (char[] chunk in bits.Skip(index).Chunk(5)) {
				literalBits += new string(chunk[1..5]);
				index += 5;
				if (chunk[0] == '0')
					break;
			}

			long literalValue = Convert.ToInt64(literalBits, 2);
            Packet literalPacket = new(version, typeId, literalValue, new());
			return (literalPacket, index);
		}

		private static int TotalLengthInBits(ref string bits, ref int index, ref List<Packet> children) {
			int lengthInBits = Convert.ToInt32(bits[index..(index += 15)], 2);
			while (lengthInBits > 0) {
				(Packet subPacket, int offset) = Parse(bits[index..(index + lengthInBits)]);
				children.Add(subPacket);
				lengthInBits -= offset;
				index += offset;
			}

			return index;
		}

		private static int NumberOfSubPackets(ref string bits, ref int index, ref List<Packet> children) {
			int numberOfPackets = Convert.ToInt32(bits[index..(index += 11)], 2);
			for (int i = 0; i < numberOfPackets; ++i) {
				(Packet subPacket, int offset) = Parse(bits[index..]);
				children.Add(subPacket);
				index += offset;
			}
			
			return index;
		}

		private record Packet(int Version, int TypeId, long Value, List<Packet> Packets) {
			public long SumVersions() => Version + Packets.Sum(c => c.SumVersions());

			public long Evaluate() => TypeId switch {
				0 => Packets.Sum(i => i.Evaluate()),
				1 => Packets.Aggregate(1L, (p, n) => p * n.Evaluate()),
				2 => Packets.Min(i => i.Evaluate()),
				3 => Packets.Max(i => i.Evaluate()),
				4 => Value,
				5 => Packets[0].Evaluate() > Packets[1].Evaluate() ? 1 : 0,
				6 => Packets[0].Evaluate() < Packets[1].Evaluate() ? 1 : 0,
				7 => Packets[0].Evaluate() == Packets[1].Evaluate() ? 1 : 0,
				_ => throw new ArgumentException()
			};
		}

		private static readonly Dictionary<char, string> Hex = new() {
            { '0', "0000" },
            { '1', "0001" },
            { '2', "0010" },
            { '3', "0011" },
            { '4', "0100" },
            { '5', "0101" },
            { '6', "0110" },
            { '7', "0111" },
            { '8', "1000" },
            { '9', "1001" },
            { 'A', "1010" },
            { 'B', "1011" },
            { 'C', "1100" },
            { 'D', "1101" },
            { 'E', "1110" },
            { 'F', "1111" }
        };
    }
}
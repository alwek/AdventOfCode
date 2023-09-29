namespace AdventOfCode2021 {
	internal class Day17 {
		public static void Run(string path) {
			Console.WriteLine("Day 17");
			var input = FileHelper.ReadFileAsString(path);

			Solve(input);
		}

		private static void Solve(string input) {
			TargetArea target = GetTargetArea(input);
			List<Probe> probes = new(), hits = new();

			for(int i = 0; i < 1000; i++)
				for(int j = -500; j < 500; j++)
					probes.Add(new Probe(i, j));

			foreach(var probe in probes) {
				bool hit = false, miss = false;

				while (!hit && !miss) {
					probe.Move();

					if (target.IsWithinTarget(probe.Position))
						hit = true;
					else if (target.HasMissedTarget(probe.Position))
						miss = true;
				}

				if (hit) 
					hits.Add(probe);
			}

            Console.WriteLine("Part One");
            Console.WriteLine($"Highest reached height is {hits.Max(x => x.Height)}");
            Console.WriteLine("Part Two");
			Console.WriteLine($"Number of probes landing on target area is {hits.Count}");
		}

		private static TargetArea GetTargetArea(string input) {
			var matches = System.Text.RegularExpressions.Regex
				.Matches(input, @"([0-9+-])\w+")
				.Select(x => int.Parse(x.Value))
				.ToArray();

			return new(matches[0], 
				matches[1], 
				matches[2], 
				matches[3]);
		}

		private record TargetArea(int FirstX, int LastX, int FirstY, int LastY) {
			internal bool IsWithinTarget((int x, int y) position) =>
				FirstX <= position.x && position.x <= LastX &&
				FirstY <= position.y && position.y <= LastY;

			internal bool HasMissedTarget((int x, int y) position) =>
				position.y < FirstY || position.x > LastX;
		}

		private record Probe(int VelocityX, int VelocityY) {
			private int x, y;
			private int gravity, drag;

			internal (int x, int y) Position { get { return (x, y); } }
			internal int Height { get; set; }

			internal void Move() {
				y += VelocityY - gravity;
				x += VelocityX - drag;

				gravity++;
				drag += drag != VelocityX ? 1: 0;

				if(y > Height)
					Height = y;
            }
        }
	}
}
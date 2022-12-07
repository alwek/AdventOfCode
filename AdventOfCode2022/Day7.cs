namespace AdventOfCode2022 {
    internal static class Day7 {
        public static void Run(string path) {
            Console.WriteLine("Day Seven");
            List<string> input = FileHelper.ReadFileAsList(path);

            Solve(input);
        }

        private static void Solve(List<string> input) {
            Directory currentDir = new("/", null);
            List<Directory> dir = new();

            foreach (string line in input.Skip(1)) {
                if (line == "$ ls")
                    continue;
                
                string[] splitted = line.Split(' ');
                if (line.Contains("$ cd")) {
                    if (splitted[2] == "..") {
                        dir.Add(currentDir);
                        currentDir = currentDir.Parent;
                    }
                    else 
                        currentDir = currentDir.Directories.First(x => x.Name == splitted[2]);
                }
                else {
                    if (line.Contains("dir")) 
                        currentDir.Directories.Add(new Directory(splitted[1], currentDir));
                    else 
                        currentDir.Files.Add(new File(splitted[1], int.Parse(splitted[0])));                   
                }
            }

            while (true) {
                if (currentDir.Parent != null)
                    currentDir = currentDir.Parent;
                else
                    break;
            }
            
            long sum = dir.Where(x => x.TotalSize() <= 100000).Sum(x => x.TotalSize());
            Console.WriteLine(sum);

            dir = dir.DistinctBy(x => x.Name).ToList();
            long required = 30000000 - (70000000 - currentDir.TotalSize());
            long min = dir.Where(x => x.TotalSize() > required).Min(x => x.TotalSize());
            Console.WriteLine(min);
        }

        private record Directory(string Name, Directory Parent) {
            internal List<File> Files = new();
            internal List<Directory> Directories = new();

            internal long TotalSize() => Files.Sum(x => x.Size) + Directories.Sum(x => x.TotalSize());
        }

        private record File(string Name, int Size) { }
    }
}
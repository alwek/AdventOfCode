namespace AdventOfCode2020
{
    internal static class FileHelper
    {
        public static string GetInputPath(int day) => $"C:/Users/alibir/source/repos/AdventOfCode/input/2020/day{day}.txt";
        public static string GetTestInputPath(int day) => $"C:/Users/alibir/source/repos/AdventOfCode/input/2020/day{day}test.txt";
        public static string ReadFileAsString(string path) => File.ReadAllText(path);
        public static List<string> ReadFileAsList(string path) => File.ReadAllLines(path).ToList();
    }
}

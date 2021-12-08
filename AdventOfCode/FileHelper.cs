namespace AdventOfCode
{
    internal static class FileHelper
    {
        public static string GetInputPath(int year, int day) => $"C:/Users/alibir/source/repos/AdventOfCode/input/{year}/day{day}.txt";
        public static string GetTestInputPath(int year, int day) => $"C:/Users/alibir/source/repos/AdventOfCode/input/{year}/day{day}test.txt";
        public static string ReadFileAsString(string path) => File.ReadAllText(path);
        public static List<string> ReadFileAsList(string path) => File.ReadAllLines(path).ToList();
        public static string[] ReadFileAsArray(string path) => File.ReadAllLines(path);
        public static string[,] ReadFileAs2DArray(string path)
        {
            string[] lines = File.ReadAllLines(path);
            string[,] position = new string[lines.Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] chars = lines[i].Select(x => x.ToString()).ToArray(); ;
                for (int j = 0; j < chars.Length; j++)
                {
                    position[i, j] = chars[j];
                }
            }

            return position;
        }
    }
}

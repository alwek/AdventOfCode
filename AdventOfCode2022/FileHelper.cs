namespace AdventOfCode2022
{
    internal static class FileHelper
    {
        public static string GetInputPath(int day) => $"C:/Users/alica/source/repos/AdventOfCode/input/2022/day{day}.txt";
        public static string GetTestInputPath() => $"C:/Users/alica/source/repos/AdventOfCode/input/2022/test.txt";
        public static string ReadFileAsString(string path) => File.ReadAllText(path);
        public static List<string> ReadFileAsList(string path) => File.ReadAllLines(path).ToList();
        public static string[] ReadFileAsArray(string path) => File.ReadAllLines(path);
        public static int[] ReadFileAsIntArray(string path) => File.ReadAllLines(path).Select(int.Parse).ToArray();
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
        public static char[,] ReadFileAs2DCharArray(string path)
        {
            string[] lines = File.ReadAllLines(path);
            char[,] position = new char[lines.Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                char[] chars = lines[i].Select(x => x).ToArray(); ;
                for (int j = 0; j < chars.Length; j++)
                {
                    position[i, j] = chars[j];
                }
            }

            return position;
        }
        public static int[,] ReadFileAs2DIntArray(string path)
        {
            string[] lines = File.ReadAllLines(path);
            int[,] position = new int[lines.Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                int[] number = lines[i].Select(x => int.Parse(x.ToString())).ToArray(); ;
                for (int j = 0; j < number.Length; j++)
                {
                    position[i, j] = number[j];
                }
            }

            return position;
        }
    }
}
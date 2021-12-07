using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal static class FileHelper
    {
        public static string GetInputPath(int day) => $"C:/Users/alibir/source/repos/AdventOfCode/input/day{day}.txt";
        public static string GetTestInputPath(int day) => $"C:/Users/alibir/source/repos/AdventOfCode/input/day{day}test.txt";
        public static string ReadFileAsString(string path) => File.ReadAllText(path);
        public static List<string> ReadFileAsList(string path) => File.ReadAllLines(path).ToList();
    }
}

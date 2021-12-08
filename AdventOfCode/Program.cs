// See https://aka.ms/new-console-template for more information
using AdventOfCode;
using AdventOfCode2021;
using AdventOfCode2020;
using System.Reflection;

Console.WriteLine("Advent of Code - Alican Bircan");
while (true)
{
    Console.WriteLine("Choose year: ");
    while (int.TryParse(Console.ReadLine(), out int year))
    {
        Console.WriteLine("Choose day:");
        while (int.TryParse(Console.ReadLine(), out int day))
        {
            Type type = Type.GetType($"AdventOfCode{year}.Day{day},AdventOfCode{year}");
            MethodInfo method = type.GetMethod("Run", new Type[] { typeof(string) });
            method.Invoke(null, new object[] { FileHelper.GetInputPath(year, day) });
        } 
    }
}


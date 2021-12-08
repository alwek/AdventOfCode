// See https://aka.ms/new-console-template for more information
using AdventOfCode2020;

Console.WriteLine("Advent of Code 2020 - Alican Bircan");
Console.WriteLine("Choose day:");

while(int.TryParse(Console.ReadLine(), out int day)) {
    switch (day) {
        case 1:
            Day1.Run(FileHelper.GetInputPath(day));
            continue;
        case 2:
            Day2.Run(FileHelper.GetInputPath(day));
            break;
        case 3:
            Day3.Run(FileHelper.GetInputPath(day));
            continue;
        case 4:
            Day4.Run(FileHelper.GetInputPath(day));
            continue;
        case 5:
            Day5.Run(FileHelper.GetInputPath(day));
            continue;
        case 6:
            Day6.Run(FileHelper.GetInputPath(day));
            continue;
        case 7:
            Day7.Run(FileHelper.GetInputPath(day));
            continue;
        case 8:
            Day8.Run(FileHelper.GetInputPath(day));
            continue;
        case 9:
            Day9.Run(FileHelper.GetInputPath(day));
            continue;
        case 10:
            Day10.Run(FileHelper.GetInputPath(day));
            continue;
        default:
            Console.WriteLine("Invalid day chosen, somehow..");
            continue;
    }
}

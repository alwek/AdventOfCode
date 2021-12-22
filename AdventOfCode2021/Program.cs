// See https://aka.ms/new-console-template for more information
using AdventOfCode2021;

Console.WriteLine("Advent of Code 2021 - Alican Bircan");
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
        case 11:
            Day11.Run(FileHelper.GetInputPath(day));
            break;
        case 12:
            Day12.Run(FileHelper.GetInputPath(day));
            break;
        case 13:
            Day13.Run(FileHelper.GetInputPath(day));
            break;
        case 14:
            Day14.Run(FileHelper.GetInputPath(day));
            break;
        case 15:
            Day15.Run(FileHelper.GetInputPath(day));
            break;
        case 16:
            Day16.Run(FileHelper.GetInputPath(day));
            break;
        case 17:
            Day17.Run(FileHelper.GetInputPath(day));
            break;
        case 18:
            Day18.Run(FileHelper.GetTestInputPath(day));
            break;
        case 19:
            Day19.Run(FileHelper.GetTestInputPath(day));
            break;
        case 20:
            Day20.Run(FileHelper.GetInputPath(day));
            break;
        case 21:
            Day21.Run(FileHelper.GetInputPath(day));
            break;
        case 22:
            Day22.Run(FileHelper.GetInputPath(day));
            break;
        case 0:
            //Test.Main(FileHelper.GetInputPath(15));
            break;
        default:
            Console.WriteLine("Invalid day chosen, somehow..");
            continue;
    }
}

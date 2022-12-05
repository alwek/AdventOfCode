using AdventOfCode2022;

Console.WriteLine("Advent of Code 2022 - Alican Bircan");
Console.WriteLine("Choose day:");

while(int.TryParse(Console.ReadLine(), out int day)) {
    switch (day) {
        case 1:
            Day1.Run(FileHelper.GetInputPath(day));
            continue;
        case 2:
            Day2.Run(FileHelper.GetInputPath(day));
            continue;
        case 3:
            Day3.Run(FileHelper.GetInputPath(day));
            continue;
        case 4:
            Day4.Run(FileHelper.GetInputPath(day));
            continue;
        case 5:
            Day5.Run(FileHelper.GetInputPath(day));
            continue;
        default:
            Console.WriteLine("Invalid day chosen, somehow..");
            continue;
    }
}
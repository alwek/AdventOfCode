open System;

printfn "Advent of Code 2022 - Alican Bircan"
printfn "Choose day:"

while true do
    let input = Console.ReadLine()
    
    match Int32.TryParse input with
    | true, day -> 
        match day with 
        | 1 -> Day1.Solve("C:/Users/alica/source/repos/AdventOfCode/input/2022/day1.txt")
        | 2 -> Day2.Solve("C:/Users/alica/source/repos/AdventOfCode/input/2022/day2.txt")
        | 3 -> Day3.Solve("C:/Users/alica/source/repos/AdventOfCode/input/2022/day3.txt")
        | 4 -> Day4.Solve("C:/Users/alica/source/repos/AdventOfCode/input/2022/day4.txt")
        | _ -> ()
    | _ -> exit 0
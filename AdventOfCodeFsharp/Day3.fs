module Day3

open System.IO
open System

let priorityValue(character: char) = 
    match character with
    | lowercase when Char.IsLower(lowercase) -> int lowercase - 96
    | uppercase -> int uppercase - 38

let Solve(path: string) =
    printfn "Day Three"

    let input = File.ReadAllLines(path)
    let mutable priorities = 0

    for line in input do
        let half = line.Length / 2
        let first = line.Substring(0, half)
        let second = line.Substring(half)

        let common = first |> Seq.filter(fun x -> second.Contains(x)) |> Seq.find(fun _ -> true)
        priorities <- priorities + priorityValue common

    printfn "%i" priorities
    priorities <- 0

    for i in 0 .. 3 .. input.Length - 3 do
        let common = input[i] |> Seq.filter(fun x -> input[i + 1].Contains(x) && input[i + 2].Contains(x)) |> Seq.find(fun _ -> true)
        priorities <- priorities + priorityValue common
        
    printfn "%i" priorities
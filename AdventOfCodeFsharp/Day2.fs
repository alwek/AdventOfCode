module Day2

open System.IO
open System

let partOne opponent player score = 
    match opponent, player with
    | "A", "X" -> score + 4
    | "A", "Y" -> score + 8
    | "A", "Z" -> score + 3
    | "B", "X" -> score + 1
    | "B", "Y" -> score + 5
    | "B", "Z" -> score + 9
    | "C", "X" -> score + 7
    | "C", "Y" -> score + 2
    | "C", "Z" -> score + 6
    | _ -> score

let partTwo opponent player score = 
    match opponent, player with
    | "A", "X" -> score + 3
    | "A", "Y" -> score + 4
    | "A", "Z" -> score + 8
    | "B", "X" -> score + 1
    | "B", "Y" -> score + 5
    | "B", "Z" -> score + 9
    | "C", "X" -> score + 2
    | "C", "Y" -> score + 6
    | "C", "Z" -> score + 7
    | _ -> score

let Solve(path: string) =
    printfn "Day Two"

    let input = File.ReadAllLines(path) |> Seq.map(fun x -> x.Split(" ", StringSplitOptions.RemoveEmptyEntries))
    let mutable first = 0
    let mutable second = 0

    for item in input do
        let opponent = item[0]
        let player = item[1]

        first <- partOne opponent player first
        second <- partTwo opponent player second

    printfn "%i" first
    printfn "%i" second
module Day1

open System.IO
open System

let Solve(path: string) =
    printfn "Day One"

    let calories = 
        File.ReadAllText(path).Split("\n\n") 
        |> Seq.map(fun x -> x.Split("\n", StringSplitOptions.RemoveEmptyEntries) |> Seq.map(int)) 
        |> Seq.map(Seq.sum)
    
    let max = calories |> Seq.max
    let top = calories |> Seq.sortDescending |> Seq.take(3) |> Seq.sum

    printfn "Max calorie: %i" max
    printfn "Top calories: %i" top
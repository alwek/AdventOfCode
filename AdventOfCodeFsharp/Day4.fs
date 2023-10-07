module Day4

open System.IO
open System

let Solve(path: string) =
    printfn "Day Four"
    let input = File.ReadAllLines(path)
    let mutable complete = 0
    let mutable partial = 0

    for line in input do
        let ranges = line.Split([| ','; '-' |], StringSplitOptions.RemoveEmptyEntries) |> Seq.map(int) |> Array.ofSeq
        let first = seq { ranges[0] .. 1 .. ranges[1] }
        let second = seq { ranges[2] .. 1 .. ranges[3] }

        if Seq.forall(fun x -> Seq.contains(x) second) first || Seq.forall(fun x -> Seq.contains(x) first) second
            then complete <- complete + 1
        if Set.intersect (Set.ofSeq first) (Set.ofSeq second) |> Set.isEmpty |> not
            then partial <- partial + 1

    printfn "%i" complete
    printfn "%i" partial
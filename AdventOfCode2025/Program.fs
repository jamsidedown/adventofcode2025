let solutions = Map.ofList [
    (1, AdventOfCode2025.Solutions.Day01.run)
    (2, AdventOfCode2025.Solutions.Day02.run)
    (3, AdventOfCode2025.Solutions.Day03.run)
    (4, AdventOfCode2025.Solutions.Day04.run)
    (5, AdventOfCode2025.Solutions.Day05.run)
    (6, AdventOfCode2025.Solutions.Day06.run)
    (7, AdventOfCode2025.Solutions.Day07.run)
    (8, AdventOfCode2025.Solutions.Day08.run)
    (9, AdventOfCode2025.Solutions.Day09.run)
    (10, AdventOfCode2025.Solutions.Day10.run)
    (11, AdventOfCode2025.Solutions.Day11.run)
    (12, AdventOfCode2025.Solutions.Day12.run)
]

[<EntryPoint>]
let main (args: string array) =
    let chosenDays =
        match args with
        | [||] ->
            solutions
            |> Seq.map _.Key
            |> Seq.sortDescending
            |> Seq.head
            |> fun h -> [h]
        | [| "--all"  |] ->
            solutions
            |> Seq.map _.Key
            |> List.ofSeq
        | _ ->
            args
            |> Array.map int
            |> List.ofArray
    
    for day in List.sort chosenDays do
        match solutions.TryFind day with
        | Some func ->
            printfn $"Day %i{day}"
            func()
        | None ->
            printfn $"Can't find day %i{day}"
    
    0

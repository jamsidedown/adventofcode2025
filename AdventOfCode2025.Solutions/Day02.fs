module AdventOfCode2025.Solutions.Day02

open System.IO
open AdventOfCode2025.Core.FileHelpers

type Range = { start: int64; stop: int64 }

let read (): string =
    getFilePath 2
    |> File.ReadAllText
    |> _.Trim()

let parse (input: string): Range list =
    input.Split ','
    |> List.ofArray
    |> List.map (fun pair ->
        match pair.Split '-' with
        | [| start; stop |] -> Some { start = int64 start; stop = int64 stop }
        | _ -> None)
    |> List.choose id
    
let isRepeated (n: int64): bool =
    let s = $"%i{n}"
    if s.Length % 2 <> 0 then false else
    let half = s.Length / 2
    let first = s[..(half - 1)]
    let second = s[half..]
    first = second
    
let getInitial (start: int64): int64 =
    let s = $"%i{start}"
    let half = s.Length / 2
    if half = 0 then start else int64 s[..(half - 1)]
    
let findRepeats (start: int64) (stop: int64): int64 list =
    let rec loop (current: int64): int64 list =
        let n = $"%i{current}%i{current}" |> int64
        if n < start then loop (current + 1L) else
        match n <= stop with
        | true -> n :: loop (current + 1L)
        | false -> []
    let initial = getInitial start
    loop initial
    
let partOne (ranges: Range list): int64 =
    ranges
    |> List.map (fun range -> findRepeats range.start range.stop |> List.sum)
    |> List.sum

let run () =
    let input = read() |> parse
    printfn $"Part 1: %i{partOne(input)}"
    printfn "Part 2:"

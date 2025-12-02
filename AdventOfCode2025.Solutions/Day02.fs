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
    
let getInitial (start: int64) (parts: int): int64 =
    let s = $"%i{start}"
    let sub = s.Length / parts
    if sub = 0 then 1 else int64 s[..(sub - 1)]
    
let findDoubles (start: int64) (stop: int64): int64 list =
    let rec loop (current: int64): int64 list =
        let n = $"%i{current}%i{current}" |> int64
        if n < start then loop (current + 1L) else
        match n <= stop with
        | true -> n :: loop (current + 1L)
        | false -> []
    let initial = getInitial start 2
    loop initial
    
let partOne (ranges: Range list): int64 =
    ranges
    |> List.map (fun range -> findDoubles range.start range.stop |> List.sum)
    |> List.sum

let findAllRepeats (start: int64) (stop: int64): int64 Set =
    let stopLength = string stop |> _.Length
    let rec loop (current: int64) (repeats: int): int64 list =
        let n =
            string current
            |> Array.replicate repeats
            |> String.concat ""
            |> int64
        if n < start then loop (current + 1L) repeats else
        match n <= stop with
        | true -> n :: loop (current + 1L) repeats
        | false -> []
    let rec loopRepeats (parts: int): int64 Set =
        if parts > stopLength then Set.empty
        else
            let initial = getInitial start parts
            loop initial parts
            |> Set
            |> Set.union (loopRepeats (parts + 1))
    loopRepeats 2

let partTwo (ranges: Range list): int64 =
    ranges
    |> List.map (fun range -> findAllRepeats range.start range.stop |> Seq.sum)
    |> List.sum

let run () =
    let input = read() |> parse
    printfn $"Part 1: %i{partOne(input)}"
    printfn $"Part 2: %i{partTwo(input)}"

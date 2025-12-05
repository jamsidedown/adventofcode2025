module AdventOfCode2025.Solutions.Day05

open System.IO
open AdventOfCode2025.Core.FileHelpers
open AdventOfCode2025.Core.Range

let read (): string list * string list =
    let lines = getFilePath 5 |> File.ReadAllLines |> List.ofArray
    let rec loop (acc: string list) (remaining: string list): string list * string list =
        match remaining with
        | "" :: tail -> (List.rev acc), tail
        | head :: tail -> loop (head :: acc) tail
        | [] -> ([], [])
    loop [] lines
    
let parseRanges (lines: string list): Range list =
    lines
    |> List.map (fun line ->
        match line.Split '-' with
        | [| start; stop |] -> Some { start = int64 start; stop = int64 stop }
        | _ -> None)
    |> List.choose id

let parseIds (lines: string list): int64 list =
    lines |> List.map int64

let rec isFresh (ranges: Range list) (ingredient: int64): bool =
    match ranges with
    | head :: tail ->
        if Range.contains head ingredient
        then true
        else isFresh tail ingredient
    | [] -> false

let partOne (ranges: Range list) (ids: int64 list): int =
    ids
    |> List.filter (isFresh ranges)
    |> List.length

let run () =
    let (rangeInput, idInput) = read()
    let ranges = parseRanges rangeInput
    let ids = parseIds idInput
    printfn $"Part 1: %i{partOne ranges ids}"

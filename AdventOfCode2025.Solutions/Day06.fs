module AdventOfCode2025.Solutions.Day06

open System
open System.IO
open AdventOfCode2025.Core.FileHelpers

type Problem = Add of int64 list | Multiply of int64 list

let read(): string list =
    getFilePath 6
    |> File.ReadAllLines
    |> List.ofArray
    
let rec parseProblem (acc: int64 list) (remaining: string list): Problem option =
    match remaining with
    | ["+"] -> List.rev acc |> Add |> Some
    | ["*"] -> List.rev acc |> Multiply |> Some
    | head :: tail -> parseProblem (int64 head :: acc) tail
    | _ -> None
    
let parse(lines: string list): Problem list =
    lines
    |> List.collect (fun line ->
        line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
        |> Array.mapi (fun i s -> (i, s))
        |> List.ofArray)
    |> List.groupBy fst
    |> List.map snd
    |> List.map (List.map snd)
    |> List.map (parseProblem [])
    |> List.choose id

let partOne (problems: Problem list): int64 =
    let rec loop (acc: int64) (remaining: Problem list): int64 =
        match remaining with
        | Add lst :: tail -> loop (acc + List.sum lst) tail
        | Multiply lst :: tail ->
            let product = List.reduce (*) lst
            loop (acc + product) tail
        | [] -> acc
    loop 0L problems

let run () =
    let input = read() |> parse
    printfn $"Part 1: %i{partOne input}"

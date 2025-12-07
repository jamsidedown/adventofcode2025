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
    
let parseOne (lines: string list): Problem list =
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

let resolveProblems (problems: Problem list): int64 =
    let rec loop (acc: int64) (remaining: Problem list): int64 =
        match remaining with
        | Add lst :: tail -> loop (acc + List.sum lst) tail
        | Multiply lst :: tail ->
            let product = List.reduce (*) lst
            loop (acc + product) tail
        | [] -> acc
    loop 0L problems

let getNumbers (numbers: int64 list) (chars: char list): int64 list =
    match chars with
    | [] -> numbers
    | ls -> (List.rev ls |> Array.ofList |> String |> int64) :: numbers

let parseTwo (lines: string list) : Problem list =
    let rec loop (numbers: int64 list) (acc: char list) (remainingLines: char list list) (remainingChars: char list): Problem list =
        match remainingChars with
        | ['+'] ->
            let add = Add (getNumbers numbers acc |> List.rev)
            match remainingLines with
            | head :: tail -> add :: loop [] [] tail head
            | [] -> [ add ]
        | ['*'] ->
            let multiply = Multiply (getNumbers numbers acc |> List.rev)
            match remainingLines with
            | head :: tail -> multiply :: loop [] [] tail head
            | [] -> [ multiply ]
        | ' ' :: tail -> loop numbers acc remainingLines tail
        | c :: tail -> loop numbers (c :: acc) remainingLines tail
        | [] ->
            match remainingLines with
            | head :: tail -> loop (getNumbers numbers acc) [] tail head
            | [] -> []
    
    let allCharacters =
        Array.ofList lines
        |> Array.map Array.ofSeq
        |> Array.transpose
        |> Array.rev
        |> Array.map List.ofArray
        |> List.ofArray
    loop [] [] allCharacters []

let run () =
    let inputOne = read() |> parseOne
    printfn $"Part 1: %i{resolveProblems inputOne}"
    let inputTwo = read() |> parseTwo
    printfn $"Part 2: %i{resolveProblems inputTwo}"

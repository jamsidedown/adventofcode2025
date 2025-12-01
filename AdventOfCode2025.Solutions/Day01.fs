module AdventOfCode2025.Solutions.Day01

open System.IO
open AdventOfCode2025.Core.FileHelpers

type Turn = Left of int | Right of int

let read (): string list =
    getFilePath 1
    |> File.ReadAllLines
    |> List.ofArray
    
let intOfChars: char list -> int = Array.ofList >> System.String >> int

let parse (lines: string list): Turn list =
    lines
    |> List.map (fun line ->
        let chars = List.ofSeq line
        match chars with
        | 'L' :: tail -> intOfChars tail |> Left |> Some
        | 'R' :: tail -> intOfChars tail |> Right |> Some
        | _ -> None)
    |> List.choose id
    
let (%%) (left: int) (right: int): int = ((left % right) + right) % right
    
let rec partOne (input: Turn list): int =
    let rec loop (acc: int) (remaining: Turn list): int =
        match remaining with
        | Left n :: tail ->
            let next = (acc - n) %% 100
            (if next = 0 then 1 else 0) + loop next tail
        | Right n :: tail ->
            let next = (acc + n) %% 100
            (if next = 0 then 1 else 0) + loop next tail
        | [] -> 0
    loop 50 input
    
let countZeroes (m: int) (start: int) (turn: Turn): int =
    let rec loop (acc: int) (current: Turn): int =
        match current with
        | Left 0 | Right 0 -> 0
        | Left n ->
            let next = (acc - 1) %% m
            (if next = 0 then 1 else 0) + loop next (Left (n - 1))
        | Right n ->
            let next = (acc + 1) %% m
            (if next = 0 then 1 else 0) + loop next (Right (n - 1))
    match turn with
    | Right n -> (n / m) + loop start (Right (n %% m))
    | Left n -> (n / m) + loop start (Left (n %% m))
    
let rec partTwo (input: Turn list): int =
    let count = countZeroes 100
    let rec loop (acc: int) (remaining: Turn list): int =
        match remaining with
        | [] -> 0
        | head :: tail ->
            let next =
                match head with
                | Left n -> (acc - n) %% 100
                | Right n -> (acc + n) %% 100
            (count acc head) + loop next tail
    loop 50 input

let run () =
    let lines = read()
    let input = parse lines
    printfn $"Part 1: %i{partOne input}"
    printfn $"Part 2: %i{partTwo input}"

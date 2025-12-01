module AdventOfCode2025.Solutions.Day01

open System.IO
open AdventOfCode2025.Core.FileHelpers

type Turn =
    | Left of int
    | Right of int

let read (): string list =
    getFilePath 1
    |> File.ReadAllLines
    |> List.ofArray
    
let intOfChars (chars: char list): int =
    System.String(Array.ofList chars) |> int

let parse (lines: string list): Turn list =
    lines
    |> List.map (fun line ->
        let chars = List.ofSeq line
        match chars with
        | 'L' :: tail -> intOfChars tail |> Left |> Some
        | 'R' :: tail -> intOfChars tail |> Right |> Some
        | _ -> None)
    |> List.choose id
    
let modulo (b: int) (x: int): int =
    let rec loop (y: int): int =
        if y < 0 then loop (y + b) else y
    loop (x % b)
    
let rec partOne (input: Turn list): int =
    let rec loop (acc: int) (remaining: Turn list): int =
        match remaining with
        | Left n :: tail ->
            let next = acc - n |> modulo 100
            (if next = 0 then 1 else 0) + loop next tail
        | Right n :: tail ->
            let next = acc + n |> modulo 100
            (if next = 0 then 1 else 0) + loop next tail
        | [] -> 0
    loop 50 input
    
let countZeroes (m: int) (start: int) (turn: Turn): int =
    let rec loop (acc: int) (current: Turn): int =
        match current with
        | Left 0 | Right 0 -> 0
        | Left n ->
            let next = modulo m (acc - 1)
            (if next = 0 then 1 else 0) + loop next (Left (n - 1))
        | Right n ->
            let next = modulo m (acc + 1)
            (if next = 0 then 1 else 0) + loop next (Right (n - 1))
    match turn with
    | Right n -> (n / m) + loop start (Right (modulo m n))
    | Left n -> (n / m) + loop start (Left (modulo m n))
    
let rec partTwo (input: Turn list): int =
    let count = countZeroes 100
    let rec loop (acc: int) (remaining: Turn list): int =
        match remaining with
        | [] -> 0
        | head :: tail ->
            let next =
                match head with
                | Left n -> modulo 100 (acc - n)
                | Right n -> modulo 100 (acc + n)
            (count acc head) + loop next tail
    loop 50 input

let run () =
    let lines = read()
    let input = parse lines
    printfn $"Part 1: %i{partOne input}"
    printfn $"Part 2: %i{partTwo input}"

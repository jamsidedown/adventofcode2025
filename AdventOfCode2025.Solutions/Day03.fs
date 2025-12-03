module AdventOfCode2025.Solutions.Day03

open System.IO
open AdventOfCode2025.Core.FileHelpers

let read () =
    getFilePath 3
    |> File.ReadAllLines
    |> List.ofArray

let parse (lines: string list): int list list =
    lines
    |> List.map (fun line ->
        List.ofSeq line
        |> List.map (fun c -> c |> string |> int))

let largestJoltage (batteries: int list): int =
    let rec getLargest (first: int) (second: int) (remaining: int list): int * int =
        match remaining with
        | h1 :: h2 :: tail when h1 > first -> getLargest h1 h2 (h2 :: tail)
        | head :: tail when head > second -> getLargest first head tail
        | _ :: tail -> getLargest first second tail
        | [] -> first, second

    let first, second =
        match batteries with
        | one :: two :: tail -> getLargest one two (two :: tail)
        | [head] -> 0, head
        | [] -> 0, 0

    (first * 10) + second

let partOne (batteries: int list list) =
    batteries
    |> List.map largestJoltage
    |> List.sum

let run () =
    let input = read() |> parse
    printfn $"Part 1: %i{partOne input}"

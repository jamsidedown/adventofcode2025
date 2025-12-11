module AdventOfCode2025.Solutions.Day11

open System.Collections.Generic
open System.IO
open AdventOfCode2025.Core.FileHelpers

type Rack = Map<string, string list>

let read (): string list =
    getFilePath 11
    |> File.ReadAllLines
    |> List.ofArray

let parse (lines: string list): Rack =
    lines
    |> List.map (fun line ->
        match line.Split ':' with
        | [| key; value |] ->
            let attached =
                value.Trim()
                |> _.Split(' ')
                |> List.ofArray
            Some (key, attached)
        | _ -> None)
    |> List.choose id
    |> Map.ofList

let partOne (serverRack: Rack): int64 =
    let cache = Dictionary<string, int64>()
    
    let rec loop (current: string): int64 =
        if current = "out" then 1L else
        if cache.ContainsKey current then cache[current] else
        let result =
            serverRack[current]
            |> List.map loop
            |> List.sum
        cache[current] <- result
        result
    
    loop "you"
    
let partTwo (serverRack: Rack): int64 =
    let cache = Dictionary<(string*bool*bool), int64>()
    
    let rec loop (dac: bool) (fft: bool) (current: string): int64 =
        if current = "out" then
            if dac && fft then 1 else 0
        else
        let key = (current, dac, fft)
        if cache.ContainsKey key then cache[key] else
        let result =
            serverRack[current]
            |> List.map (fun connection ->
                match connection with
                | "dac" -> loop true fft connection
                | "fft" -> loop dac true connection
                | _ -> loop dac fft connection)
            |> List.sum
        cache[key] <- result
        result
    
    loop false false "svr"

let run() =
    let input = read() |> parse
    printfn $"Part 1: %i{partOne input}"
    printfn $"Part 2: %i{partTwo input}"

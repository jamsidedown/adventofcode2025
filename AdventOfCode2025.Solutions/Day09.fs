module AdventOfCode2025.Solutions.Day09

open System.Collections.Generic
open System.IO
open AdventOfCode2025.Core.FileHelpers
open AdventOfCode2025.Core.XyPair

let read() =
    getFilePath 9
    |> File.ReadAllLines
    
let parse(lines: string array): XyPair array =
    lines
    |> Array.map (fun line ->
        match line.Split ',' with
        | [| x; y |] -> Some { x = int64 x; y = int64 y }
        | _ -> None)
    |> Array.choose id
    
let partOne (coords: XyPair array): int64 =
    let areas = List<(XyPair * XyPair) * int64>()
    
    for i in 0..(coords.Length - 2) do
        let a = coords[i]
        for j in (i + 1)..(coords.Length - 1) do
            let b = coords[j]
            let area = XyPair.areaInclusive a b
            areas.Add ((a, b), area)
    
    areas
    |> Seq.maxBy snd
    |> snd

let run () =
    let input = read() |> parse
    printfn $"Part 1: %i{partOne input}"

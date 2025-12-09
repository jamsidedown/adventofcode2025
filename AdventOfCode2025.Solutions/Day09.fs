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
    
let partTwo (coords: XyPair array): int64 =
    // there are two odd coordinates at (94634, 50269) and (94634,48484) in my input
    // I assume they're important
    let high = { x = 94634; y = 50269 }
    let low = { x = 94634; y = 48484 }
    
    let highCoords =
        coords
        |> Array.filter (fun xy -> xy.y >= high.y)
        
    let lowCoords =
        coords
        |> Array.filter (fun xy -> xy.y <= low.y)
        
    let maxHigh =
        highCoords
        |> Array.where (fun xy ->
            highCoords
            |> Array.where (XyPair.inside high xy)
            |> Array.length = 0)
        |> Array.map (XyPair.areaInclusive high)
        |> Array.max
        
    let maxLow =
        lowCoords
        |> Array.where (fun xy ->
            lowCoords
            |> Array.where (XyPair.inside low xy)
            |> Array.length = 0)
        |> Array.map (XyPair.areaInclusive low)
        |> Array.max
        
    max maxHigh maxLow

let run () =
    let input = read() |> parse
    printfn $"Part 1: %i{partOne input}"
    printfn $"Part 2: %i{partTwo input}"

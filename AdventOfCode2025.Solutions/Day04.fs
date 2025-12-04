module AdventOfCode2025.Solutions.Day04

open System.IO
open AdventOfCode2025.Core.FileHelpers
open AdventOfCode2025.Core.XyPair

type Cell = Paper | Empty
type Grid = Map<XyPair, Cell>

let read () =
    getFilePath 4
    |> File.ReadAllLines
    |> List.ofArray
    
let parse (lines: string list): Grid =
    lines
    |> List.mapi (fun y line ->
        List.ofSeq line
        |> List.mapi (fun x cell ->
            match cell with
            | '@' -> Some ({ x = x; y = y }, Paper)
            | _ -> None))
    |> List.collect id
    |> List.choose id
    |> Map.ofList
   
let neighbours (map: Grid) (coord: XyPair): XyPair list =
    [ -1..1 ]
    |> List.collect (fun y ->
        [ -1..1 ]
        |> List.map (fun x -> XyPair.add coord { x = x; y = y }))
    |> List.filter (fun xy -> xy <> coord )
    |> List.filter (fun xy -> Map.containsKey xy map)

let countAdjacent (map: Grid) (coord: XyPair): int =
    neighbours map coord
    |> List.map (fun c -> Map.containsKey c map)
    |> List.length
    
let partOne (map: Grid): int =
    Map.keys map
    |> Seq.map (countAdjacent map)
    |> Seq.filter (fun count -> count < 4)
    |> Seq.length

let run () =
    let input = read() |> parse
    printfn $"Part 1: {partOne input}"

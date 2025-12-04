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
    
let forkliftable (map: Grid): XyPair list =
    Map.keys map
    |> Seq.filter (fun coord -> countAdjacent map coord < 4)
    |> List.ofSeq
    
let partOne (map: Grid): int =
    forkliftable map
    |> List.length

let partTwo (map: Grid): int =
    let rec remove (acc: Grid) (coords: XyPair list): Grid =
        match coords with
        | head :: tail -> remove (Map.remove head acc) tail
        | [] -> acc
    let rec loop (current: Grid): Grid =
        match forkliftable current with
        | [] -> current
        | ls -> ls |> remove current |> loop
    let final = loop map
    Map.count map - Map.count final

let run () =
    let input = read() |> parse
    printfn $"Part 1: {partOne input}"
    printfn $"Part 2: {partTwo input}"

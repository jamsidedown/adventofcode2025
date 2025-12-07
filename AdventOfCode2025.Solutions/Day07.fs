module AdventOfCode2025.Solutions.Day07

open System.Collections.Generic
open System.IO
open AdventOfCode2025.Core.FileHelpers
open AdventOfCode2025.Core.XyPair

type Cell = Start | Beam | Splitter
type Grid = Map<XyPair, Cell>

let down = { x = 0; y = 1 }
let left = { x = -1; y = 0 }
let right = { x = 1; y = 0 }

let read(): string list =
    getFilePath 7
    |> File.ReadAllLines
    |> List.ofArray

let parseCell (cell: char): Cell option =
    match cell with
    | 'S' -> Some Start
    | '|' -> Some Beam
    | '^' -> Some Splitter
    | _ -> None

let parse (lines: string list): Grid =
    lines
    |> List.mapi (fun row line ->
        List.ofSeq line
        |> List.mapi (fun col c ->
            let xy = { x = col; y = row }
            match c with
            | 'S' -> Some (xy, Start)
            | '^' -> Some (xy, Splitter)
            | _ -> None)
        |> List.choose id)
    |> List.collect id
    |> Map.ofList

let findStart (grid: Grid): XyPair =
    grid
    |> Map.filter (fun _ value -> value = Start)
    |> Map.keys
    |> Seq.head
    
let getMaxRow (grid: Grid): int64 =
    Map.keys grid
    |> Seq.map (fun xy -> xy.y)
    |> Seq.max

let partOne (grid: Grid): int =
    let start = findStart grid
    let maxRow = (getMaxRow grid) + 1L
    let hitSplitters = HashSet<XyPair>()
    
    let rec loop (pos: XyPair) =
        let next = XyPair.add pos down
        if next.y > maxRow then () else
        match Map.tryFind next grid with
        | Some Splitter ->
            if hitSplitters.Contains next then () else
            hitSplitters.Add next |> ignore
            loop (XyPair.add next left)
            loop (XyPair.add next right)
        | _ -> loop next
        
    loop start
    hitSplitters.Count
    
let partTwo (grid: Grid): int64 =
    let start = findStart grid
    let maxRow = (getMaxRow grid) + 1L
    let hitSplitters = Dictionary<XyPair, int64>()
    
    let rec loop (pos: XyPair): int64 =
        let next = XyPair.add pos down
        if next.y > maxRow then 1L else
        match Map.tryFind next grid with
        | Some Splitter ->
            match hitSplitters.TryGetValue next with
            | true, value -> value
            | false, _ ->
                let leftValue = loop (XyPair.add next left)
                let rightValue = loop (XyPair.add next right)
                let value = leftValue + rightValue
                hitSplitters.Add(next, value)
                value
        | _ -> loop next

    loop start    
    
let run () =
    let input = read() |> parse
    printfn $"Part 1: %i{partOne input}"
    printfn $"Part 2: %i{partTwo input}"

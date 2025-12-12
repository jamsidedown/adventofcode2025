module AdventOfCode2025.Solutions.Day12

open System
open System.IO
open AdventOfCode2025.Core.XyPair
open AdventOfCode2025.Core.FileHelpers

type Present = XyPair Set
type Region = { size: XyPair; quantities: int array }

let read () =
    getFilePath 12
    |> File.ReadAllLines
    
let rec parsePresent (lines: string list): Present =
    let rec loop (acc: Present) (row: int) (remaining: string list): Present =
        match remaining with
        | [] | [_] -> acc
        | head :: tail ->
            let line =
                head
                |> Seq.mapi (fun i c ->
                    match c with
                    | '#' -> Some { x = i; y = row }
                    | _ -> None)
                |> Seq.choose id
                |> Set.ofSeq
            loop (Set.union acc line) (row + 1) tail
    
    lines
    |> List.skip 1
    |> loop Set.empty 0
    
let parsePresents (lines: string array): Present array =
    lines
    |> Array.chunkBySize 5
    |> Array.map List.ofArray
    |> Array.map parsePresent
    
let parseRegion (line: string): Region option =
    match line.Split ':' with
    | [| dimensions; quantities |] ->
        match dimensions.Split 'x' with
        | [| x; y |] ->
            let size = { x = int x; y = int y }
            let quantities =
                quantities.Trim().Split ' '
                |> Array.map int
            Some { size = size; quantities = quantities }
        | _ -> None
    | _ -> None
    
let parseRegions (lines: string array): Region array =
    lines
    |> Array.map parseRegion
    |> Array.choose id
    
let parse (lines: string array): Present array * Region array =
    let presents = lines |> Array.take 30 |> parsePresents
    let regions = lines |> Array.skip 30 |> parseRegions
    presents, regions

let dimensions (presents: Present): XyPair =
    let rec loop (minX: int64) (minY: int64) (maxX: int64) (maxY: int64) (remaining: XyPair list): XyPair =
        match remaining with
        | head :: tail -> loop (min minX head.x) (min minY head.y) (max maxX head.x) (max maxY head.y) tail
        | [] -> { x = maxX - minX; y = maxY - minY }
    presents
    |> List.ofSeq
    |> loop Int64.MaxValue Int64.MaxValue Int64.MinValue Int64.MinValue

let rec rotateCw (coord: XyPair): XyPair =
    // all shapes are 3x3
    match coord.x, coord.y with
    | 0L, 0L -> { x = 2; y = 0 }
    | 1L, 0L -> { x = 2; y = 1 }
    | 2L, 0L -> { x = 2; y = 2 }
    | 0L, 1L -> { x = 1; y = 0 }
    | 2L, 1L -> { x = 1; y = 2 }
    | 0L, 2L -> { x = 0; y = 0 }
    | 1L, 2L -> { x = 0; y = 1 }
    | 2L, 2L -> { x = 0; y = 2 }
    | _ -> { x = 1; y = 1 }
    
let flipHorizontal (coord: XyPair): XyPair =
    { x = 2L - coord.x; y = coord.y }

let rotate (present: Present): Present =
    present |> Set.map rotateCw
    
let flip (present: Present): Present =
    present |> Set.map flipHorizontal

let canFit (presents: Present array) (region: Region): bool =
    let area = region.size.x * region.size.y
    let presentsMinArea =
        region.quantities
        |> Array.zip presents
        |> Array.map (fun (present, count) -> (Set.count present) * count |> int64)
        |> Array.sum
    // so so sneaky
    // I was cutting out little paper tiles to see how they most efficiently fit together
    // in each region there's hundreds of square regions of wiggle room
    presentsMinArea < area
    
let partOne (presents: Present array) (regions: Region array): int =
    regions
    |> Array.filter (canFit presents)
    |> Array.length

let run() =
    let presents, regions = read() |> parse
    printfn $"Part 1: %i{partOne presents regions}"

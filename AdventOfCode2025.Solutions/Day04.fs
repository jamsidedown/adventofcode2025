module AdventOfCode2025.Solutions.Day04

open System.IO
open AdventOfCode2025.Core.FileHelpers
open AdventOfCode2025.Core.XyPair

type Paper = Set<XyPair>

let read () =
    getFilePath 4
    |> File.ReadAllLines
    |> List.ofArray
    
let parse (lines: string list): Paper =
    lines
    |> List.mapi (fun y line ->
        List.ofSeq line
        |> List.mapi (fun x cell ->
            match cell with
            | '@' -> Some { x = x; y = y }
            | _ -> None))
    |> List.collect id
    |> List.choose id
    |> Set.ofList
   
let neighbours (paper: Paper) (coord: XyPair): Paper =
    [ -1..1 ]
    |> List.collect (fun y ->
        [ -1..1 ]
        |> List.map (fun x -> XyPair.add coord { x = x; y = y }))
    |> List.filter (fun xy -> xy <> coord )
    |> List.filter (fun xy -> Set.contains xy paper)
    |> Set.ofList

let canLift (paper: Paper) (coord: XyPair): bool =
    let count = neighbours paper coord |> Set.count
    count < 4
    
let forkliftable (paper: Paper) (check: Paper): Paper =
    Set.filter (canLift paper) check
    
let partOne (paper: Paper): int =
    forkliftable paper paper |> Set.count

let partTwo (paper: Paper): int =
    let rec loop (acc: Paper) (check: Paper): Paper =
        let liftable = forkliftable acc check
        if Set.isEmpty liftable then acc else
        let accNext = liftable |> Set.difference acc
        let checkNext = liftable |> Seq.map (neighbours accNext) |> Set.unionMany
        loop accNext checkNext
        
    let final = loop paper paper
    Set.count paper - Set.count final

let run () =
    let input = read() |> parse
    printfn $"Part 1: {partOne input}"
    printfn $"Part 2: {partTwo input}"

module AdventOfCode2025.Solutions.Day08

open System.Collections.Generic
open System.IO
open System.Linq
open AdventOfCode2025.Core.FileHelpers
open AdventOfCode2025.Core.Xyz

type AllDistances = Dictionary<Xyz, Dictionary<Xyz, double>>
type Distances = ((Xyz * Xyz) * double) list
type Connections = Dictionary<Xyz, HashSet<Xyz>>

let read (): string list =
    getFilePath 8
    |> File.ReadAllLines
    |> List.ofArray

let parse (lines: string list): Xyz array =
    lines
    |> Array.ofList
    |> Array.map (fun line ->
        line.Split ','
        |> Array.map int64
        |> function
            | [| x; y; z |] -> Some { x = x; y = y; z = z }
            | _ -> None)
    |> Array.choose id
    
let getDistances (coords: Xyz array): Distances =        
    let distances = List<(Xyz*Xyz)*double>()
    
    for i in 0..(coords.Length - 2) do
        let a = coords[i]
        for j in (i + 1)..(coords.Length - 1) do
            let b = coords[j]
            let distance = Xyz.distance a b
            distances.Add((a, b), distance)
    
    List.ofSeq distances
    |> List.sortBy snd
    
let getConnections (distances: Distances): Connections =
    let connections = Connections()
    
    for (a, b), _ in distances do
        match connections.TryGetValue a with
        | true, set -> set.Add b |> ignore
        | false, _ ->
            let set = HashSet<Xyz>()
            set.Add b |> ignore
            connections.Add(a, set)
        
        match connections.TryGetValue b with
        | true, set -> set.Add a |> ignore
        | false, _ ->
            let set = HashSet<Xyz>()
            set.Add a |> ignore
            connections.Add(b, set)
            
    connections
    
let getCircuits (connections: Connections): HashSet<Xyz> list =
    let seen = HashSet<Xyz>()
    let circuits = Connections()
    
    let rec loop (start: Xyz) (current: Xyz) =
        if seen.Contains current then () else
        seen.Add current |> ignore
        match circuits.TryGetValue start with
        | true, set -> set.Add current |> ignore
        | false, _ ->
            let set = HashSet<Xyz>()
            set.Add start |> ignore
            set.Add current |> ignore
            circuits.Add(start, set)

        for conn in connections[current] do
            loop start conn
            
    while seen.Count < connections.Count do
        let start = connections.Keys.Except seen |> Seq.head
        seen.Add start |> ignore
        for conn in connections[start] do
            loop start conn
        
    circuits.Values
    |> List.ofSeq

let getCircuit (start: Xyz) (connections: Connections): HashSet<Xyz> =
    let circuit = HashSet<Xyz>()
    
    let rec loop (current: Xyz) =
        if circuit.Contains current then () else
        circuit.Add current |> ignore
        
        for conn in connections[current] do
            loop conn
    
    loop start
    circuit

let getLastCircuit (coords: Xyz array) (distances: Distances): Xyz * Xyz =
    let inCircuit = HashSet<Xyz>()
    let connections = Connections()
    for coord in coords do
        connections.Add(coord, HashSet<Xyz>())
    
    let (a, b), _ = List.head distances
    inCircuit.Add a |> ignore
    inCircuit.Add b |> ignore
    
    connections[a].Add b |> ignore
    connections[b].Add a |> ignore
    
    let rec addConnections (current: Xyz) =
        inCircuit.Add current |> ignore
        for conn in connections[current] do
            if inCircuit.Contains conn then ()
            else addConnections conn
    
    let rec loop (remaining: Distances): Xyz * Xyz =
        let (a, b), _ = List.head remaining
        let tail = List.tail remaining

        match inCircuit.Contains a, inCircuit.Contains b with
        | true, true -> loop tail
        | true, false ->
            addConnections b
            if inCircuit.Count = coords.Length then (a, b) else loop tail
        | false, true ->
            addConnections a
            if inCircuit.Count = coords.Length then (a, b) else loop tail
        | false, false ->
            connections[a].Add b |> ignore
            connections[b].Add a |> ignore
            loop tail
            
    loop (List.tail distances)
    
let partOne (pairs: int) (distances: Distances): int64 =
    let connections = getConnections (List.take pairs distances)
    let circuits = getCircuits connections
    circuits
    |> List.map _.Count
    |> List.map int64
    |> List.sortDescending
    |> List.take 3
    |> List.reduce (*)
    
let partTwo (coords: Xyz array) (distances: Distances): int64 =
    let lastCircuits = getLastCircuit coords distances
    let a, b = lastCircuits
    a.x * b.x
    
let run () =
    let input = read() |> parse
    let distances = getDistances input
    printfn $"Part 1: %i{partOne 1000 distances}"
    printfn $"Part 2: %i{partTwo input distances}"

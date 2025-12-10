module AdventOfCode2025.Solutions.Day10

open System
open System.IO
open System.Text.RegularExpressions
open AdventOfCode2025.Core.FileHelpers
open AdventOfCode2025.Core.List

type Machine = {
    indicatorLights: int
    buttons: int list
    joltages: int list
}

let machinePattern = Regex(@"^\[([.#]+)\] (.*) \{([\d,]+)\}$", RegexOptions.Compiled)

let read() =
    getFilePath 10
    |> File.ReadAllLines
    |> List.ofArray

let rec parseInt (bits: int list): int =
    match bits with
    | head :: tail -> (1 <<< head) ^^^ (parseInt tail)
    | [] -> 0
    
let parseIndicators (indicators: string): int =
    List.ofSeq indicators
    |> List.mapi (fun i c ->
        if c = '#' then Some i else None)
    |> List.choose id
    |> parseInt
    
let isNumeric (c: char): bool = c >= '0' && c <= '9'

let asString (chars: char list): string =
    chars |> Array.ofList |> String
    
let parseButtons (buttons: string): int list =
    buttons.Split ' '
    |> List.ofArray
    |> List.map (fun group ->
        group.Split ','
        |> Array.map List.ofSeq
        |> Array.map (List.filter isNumeric)
        |> Array.map asString
        |> Array.map int
        |> List.ofArray
        |> parseInt)
    
let parseJoltages (joltages: string): int list =
    joltages.Split ','
    |> Array.map List.ofSeq
    |> Array.map (List.filter isNumeric)
    |> Array.map asString
    |> Array.map int
    |> List.ofArray

let parseLine (line: string): Machine option =
    let matches = machinePattern.Match line
    let groups = matches.Groups |> List.ofSeq
    match groups with
    | _ :: indicators :: buttons :: joltages :: _ ->
        Some {
            indicatorLights = parseIndicators indicators.Value
            buttons = parseButtons buttons.Value
            joltages = parseJoltages joltages.Value
        }
    | _ -> None

let parse (lines: string list): Machine list =
    lines |> List.map parseLine |> List.choose id

let optimiseMachine (machine: Machine): int list =
    let limit = List.length machine.buttons
    
    let rec loop (iter: int): int * int list =
        if (iter > limit) then (0, []) else
        
        let coms =
            machine.buttons
            |> combinations iter
        
        let results =
            coms
            |> List.map (fun buttons ->
                let lights = buttons |> List.reduce (^^^)
                (lights, buttons))
        
        let solutions =
            results
            |> List.filter (fun (lights, _) ->
                lights = machine.indicatorLights)
        
        if not (List.isEmpty solutions) then
            List.head solutions
        else
            loop (iter + 1)
            
    loop 1 |> snd

let partOne (machines: Machine list): int =
    machines
    |> List.map optimiseMachine
    |> List.map List.length
    |> List.sum

let run () =
    let input = read() |> parse
    printfn $"Part 1: {partOne input}"

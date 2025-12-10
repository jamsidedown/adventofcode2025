module AdventOfCode2025.Solutions.Day10

open System
open System.IO
open System.Text.RegularExpressions
open AdventOfCode2025.Core.FileHelpers
open AdventOfCode2025.Core.List

type Machine = {
    indicatorLights: int array
    buttons: int array list
    joltages: int array
} with
    static member pressLights (buttons: int array) (lights: int array): int array =
        Array.zip buttons lights
        |> Array.map (fun (button, light) -> if button = 0 then light else light ^^^ 1)
    
    static member pressJoltages (buttons: int array) (joltages: int array): int array =
        Array.zip buttons joltages
        |> Array.map (fun (button, joltage) -> if button = 0 then joltage else joltage + 1)

let machinePattern = Regex(@"^\[([.#]+)\] (.*) \{([\d,]+)\}$", RegexOptions.Compiled)

let read() =
    getFilePath 10
    |> File.ReadAllLines
    |> List.ofArray

let rec parseInt (bits: int list): int =
    match bits with
    | head :: tail -> (1 <<< head) ^^^ (parseInt tail)
    | [] -> 0
    
let parseIndicators (indicators: string): int array =
    Array.ofSeq indicators
    |> Array.map (fun c -> if c = '#' then 1 else 0)
    
let isNumeric (c: char): bool = c >= '0' && c <= '9'

let asString (chars: char list): string =
    chars |> Array.ofList |> String
    
let createArray (length: int) (switches: int Set): int array =
    [| 0..(length - 1) |]
    |> Array.map (fun i ->
        if Set.contains i switches then 1 else 0)
    
let parseButtons (lightCount: int) (buttons: string): int array list =
    buttons.Split ' '
    |> List.ofArray
    |> List.map (fun group ->
        group.Split ','
        |> Array.map List.ofSeq
        |> Array.map (List.filter isNumeric)
        |> Array.map asString
        |> Array.map int)
    |> List.map Set.ofArray
    |> List.map (createArray lightCount)
    
let parseJoltages (joltages: string): int array =
    joltages.Split ','
    |> Array.map List.ofSeq
    |> Array.map (List.filter isNumeric)
    |> Array.map asString
    |> Array.map int

let parseLine (line: string): Machine option =
    let matches = machinePattern.Match line
    let groups = matches.Groups |> List.ofSeq
    match groups with
    | _ :: indicators :: buttons :: joltages :: _ ->
        let parsedIndicators = parseIndicators indicators.Value
        Some {
            indicatorLights = parsedIndicators
            buttons = parseButtons parsedIndicators.Length buttons.Value
            joltages = parseJoltages joltages.Value
        }
    | _ -> None

let parse (lines: string list): Machine list =
    lines |> List.map parseLine |> List.choose id

let optimiseMachine (machine: Machine): int array list =
    let limit = List.length machine.buttons
    
    let rec loop (iter: int): (int array) * (int array list) =
        if (iter > limit) then ([||], []) else
        
        let coms =
            machine.buttons
            |> combinations iter
        
        let results =
            coms
            |> List.map (fun buttons ->
                let lights = buttons |> List.reduce Machine.pressLights
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

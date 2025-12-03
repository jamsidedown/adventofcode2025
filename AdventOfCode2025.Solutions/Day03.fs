module AdventOfCode2025.Solutions.Day03

open System.IO
open AdventOfCode2025.Core.FileHelpers

exception BatteryException of string

let read () =
    getFilePath 3
    |> File.ReadAllLines
    |> List.ofArray

let parse (lines: string list): int list list =
    lines
    |> List.map (fun line ->
        List.ofSeq line
        |> List.map (fun c -> c |> string |> int))

let getPositions (batteries: int list): Map<int, int list> =
    let rec loop (acc: Map<int, int list>) (pos: int) (remaining: int list): Map<int, int list> =
        match remaining with
        | head :: tail ->
            match Map.tryFind head acc with
            | Some ls -> loop (Map.add head (pos :: ls) acc) (pos + 1) tail
            | None -> loop (Map.add head [pos] acc) (pos + 1) tail
        | [] -> acc

    loop Map.empty 0 batteries
    |> Map.map (fun _ -> List.sort)

let largestJoltage (totalPositions: int) (batteries: int list): int64 =
    let batteryValues = Array.ofList batteries |> Array.map int64
    let batteryCount = Array.length batteryValues
    let positions = getPositions batteries
    let digits = Map.keys positions |> Seq.sortDescending |> Seq.toList

    let rec getDigitIndex (startIndex: int) (endIndex: int) (remainingDigits: int list): int =
        match remainingDigits with
        | digit :: rem ->
            let ls = Map.find digit positions
            match List.tryFind (fun x -> x >= startIndex && x <= endIndex) ls with
            | Some index -> index
            | None -> getDigitIndex startIndex endIndex rem
        | [] -> raise (BatteryException "Should always be remaining digits")

    let rec loopDigits (acc: int64) (digitsLeft: int) (startIndex: int): int64 =
        if digitsLeft = 0 then acc else
        let index = getDigitIndex startIndex (batteryCount - digitsLeft) digits
        let current = (acc * 10L) + batteryValues[index]
        loopDigits current (digitsLeft - 1) (index + 1)

    loopDigits 0L totalPositions 0

let partOne (batteries: int list list): int64 =
    batteries
    |> List.map (largestJoltage 2)
    |> List.sum

let partTwo (batteries: int list list): int64 =
    batteries
    |> List.map (largestJoltage 12)
    |> List.sum

let run () =
    let input = read() |> parse
    printfn $"Part 1: %i{partOne input}"
    printfn $"Part 2: %i{partTwo input}"

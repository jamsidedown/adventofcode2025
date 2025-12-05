module Day04Tests

open Xunit
open AdventOfCode2025.Solutions.Day04

let exampleInput = [
    "..@@.@@@@.";
    "@@@.@.@.@@";
    "@@@@@.@.@@";
    "@.@@@@..@.";
    "@@.@@@@.@@";
    ".@@@@@@@.@";
    ".@.@.@.@@@";
    "@.@@@.@@@@";
    ".@@@@@@@@.";
    "@.@.@@@.@.";
]

[<Fact>]
let ``Can get result for part one`` () =
    let parsed = parse exampleInput
    let result = partOne parsed
    Assert.Equal(13, result)

[<Fact>]
let ``Can get result for part two`` () =
    let parsed = parse exampleInput
    let result = partTwo parsed
    Assert.Equal(43, result)

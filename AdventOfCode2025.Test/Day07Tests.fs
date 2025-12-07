module Day07Tests

open Xunit
open AdventOfCode2025.Solutions.Day07

let exampleInput = [
    ".......S.......";
    "...............";
    ".......^.......";
    "...............";
    "......^.^......";
    "...............";
    ".....^.^.^.....";
    "...............";
    "....^.^...^....";
    "...............";
    "...^.^...^.^...";
    "...............";
    "..^...^.....^..";
    "...............";
    ".^.^.^.^.^...^.";
    "...............";
]

[<Fact>]
let ``Can solve part one`` () =
    let input = parse exampleInput
    let result = partOne input
    Assert.Equal(21, result)

[<Fact>]
let ``Can solve part two`` () =
    let input = parse exampleInput
    let result = partTwo input
    Assert.Equal(40L, result)

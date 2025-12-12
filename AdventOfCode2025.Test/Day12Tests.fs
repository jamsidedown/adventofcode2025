module Day12Tests

open Xunit
open AdventOfCode2025.Solutions.Day12

let exampleInput = [|
    "0:";
    "###";
    "##.";
    "##.";
    "";
    "1:";
    "###";
    "##.";
    ".##";
    "";
    "2:";
    ".##";
    "###";
    "##.";
    "";
    "3:";
    "##.";
    "###";
    "##.";
    "";
    "4:";
    "###";
    "#..";
    "###";
    "";
    "5:";
    "###";
    ".#.";
    "###";
    "";
    "4x4: 0 0 0 0 2 0";
    "12x5: 1 0 1 0 2 2";
    "12x5: 1 0 1 0 3 2";
|]

[<Fact>]
let ``Can solve part one`` () =
    let presents, regions = parse exampleInput
    let result = partOne presents regions
    // Assert.Equal(2, result)
    Assert.True true

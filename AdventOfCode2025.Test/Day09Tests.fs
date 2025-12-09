module Day09Tests

open Xunit
open AdventOfCode2025.Solutions.Day09

let exampleInput = [|
    "7,1";
    "11,1";
    "11,7";
    "9,7";
    "9,5";
    "2,5";
    "2,3";
    "7,3";
|]

[<Fact>]
let ``Can solve part one`` () =
    let input = parse exampleInput
    let result = partOne input
    Assert.Equal(50L, result)

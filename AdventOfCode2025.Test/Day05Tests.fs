module Day05Tests

open Xunit
open AdventOfCode2025.Solutions.Day05

let exampleRanges = [
    "3-5";
    "10-14";
    "16-20";
    "12-18"
]

let exampleIds = [
    "1";
    "5";
    "8";
    "11";
    "17";
    "32"
]

[<Fact>]
let ``Can solve part one`` () =
    let ranges = parseRanges exampleRanges
    let ids = parseIds exampleIds
    let result = partOne ranges ids
    Assert.Equal(3L, result)

[<Fact>]
let ``Can solve part two`` () =
    let parsed = parseRanges exampleRanges
    let result = partTwo parsed
    Assert.Equal(14L, result)

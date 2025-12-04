module Day04Tests

open Xunit
open AdventOfCode2025.Core.XyPair
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
let ``Can get neighbour coordinates of (2, 3)`` () =
   let result = neighbourCoords { x = 2; y = 3 }
   let expected = [
       { x = 1; y = 2 };
       { x = 2; y = 2 };
       { x = 3; y = 2 };
       { x = 1; y = 3 };
       { x = 3; y = 3 };
       { x = 1; y = 4 };
       { x = 2; y = 4 };
       { x = 3; y = 4 };
   ]
   Assert.Equal<XyPair>(expected, result)

[<Fact>]
let ``Can get result for part one`` () =
    let parsed = parse exampleInput
    let result = partOne parsed
    Assert.Equal(13, result)

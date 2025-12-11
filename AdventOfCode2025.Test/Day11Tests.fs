module Day11Tests

open Xunit
open AdventOfCode2025.Solutions.Day11

let exampleInput = [
    "aaa: you hhh";
    "you: bbb ccc";
    "bbb: ddd eee";
    "ccc: ddd eee fff";
    "ddd: ggg";
    "eee: out";
    "fff: out";
    "ggg: out";
    "hhh: ccc fff iii";
    "iii: out";
]

[<Fact>]
let ``Can solve part one`` () =
    let input = parse exampleInput
    let result = partOne input
    Assert.Equal(5L, result)

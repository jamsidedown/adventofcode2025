module Day08Tests

open Xunit
open AdventOfCode2025.Solutions.Day08

let exampleInput = [
    "162,817,812";
    "57,618,57";
    "906,360,560";
    "592,479,940";
    "352,342,300";
    "466,668,158";
    "542,29,236";
    "431,825,988";
    "739,650,466";
    "52,470,668";
    "216,146,977";
    "819,987,18";
    "117,168,530";
    "805,96,715";
    "346,949,466";
    "970,615,88";
    "941,993,340";
    "862,61,35";
    "984,92,344";
    "425,690,689";
]

[<Fact>]
let ``Can solve part one`` () =
    let input = parse exampleInput
    let distances = getDistances input
    let result = partOne 10 distances
    Assert.Equal(40L, result)

[<Fact>]
let ``Can solve part two`` () =
    let input = parse exampleInput
    let distances = getDistances input
    let result = partTwo input distances
    Assert.Equal(25272L, result)

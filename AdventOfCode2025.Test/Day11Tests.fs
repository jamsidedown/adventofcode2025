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

let exampleInputTwo = [
    "svr: aaa bbb";
    "aaa: fft";
    "fft: ccc";
    "bbb: tty";
    "tty: ccc";
    "ccc: ddd eee";
    "ddd: hub";
    "hub: fff";
    "eee: dac";
    "dac: fff";
    "fff: ggg hhh";
    "ggg: out";
    "hhh: out";
]

[<Fact>]
let ``Can solve part one`` () =
    let input = parse exampleInput
    let result = partOne input
    Assert.Equal(5L, result)

[<Fact>]
let ``Can solve part two`` () =
    let input = parse exampleInputTwo
    let result = partTwo input
    Assert.Equal(2L, result)

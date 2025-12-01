module Day01Tests

open System
open Xunit
open AdventOfCode2025.Solutions.Day01

let exampleInput = ["L68"; "L30"; "R48"; "L5"; "R60"; "L55"; "L1"; "L99"; "R14"; "L82"]

[<Fact>]
let ``Test can parse example input`` () =
    let parsed = parse exampleInput
    let expected = [ Left 68; Left 30; Right 48; Left 5; Right 60; Left 55; Left 1; Left 99; Right 14; Left 82 ]
    Assert.Equal<Turn>(expected, parsed)

[<Fact>]
let ``Test part one input returns 3 for example input`` () =
    let parsed = parse exampleInput
    let result = partOne parsed
    Assert.Equal(3, result)
    
[<Fact>]
let ``Test part two input returns 6 for example input`` () =
    let parsed = parse exampleInput
    let result = partTwo parsed
    Assert.Equal(6, result)

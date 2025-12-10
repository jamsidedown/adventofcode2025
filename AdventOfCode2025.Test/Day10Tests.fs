module Day10Tests

open Xunit
open AdventOfCode2025.Solutions.Day10

let exampleInput = [|
    "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}";
    "[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}";
    "[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}";
|]

[<Fact>]
let ``Can parse the first example`` () =
    let parsed = parseLine exampleInput[0]
    let expected = { indicatorLights = 6; buttons = [ 8; 10; 4; 12; 5; 3 ]; joltages = [ 3; 5; 4; 7 ] }
    match parsed with
    | Some machine -> Assert.Equal(expected, machine)
    | None -> Assert.Fail()
    
[<Fact>]
let ``Can solve part one`` () =
    let parsed = parse (List.ofArray exampleInput)
    let result = partOne parsed
    Assert.Equal(7, result)

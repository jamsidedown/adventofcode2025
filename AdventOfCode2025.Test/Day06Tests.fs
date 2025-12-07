module Day06Tests

open Xunit
open AdventOfCode2025.Solutions.Day06

let exampleInput = [
    "123 328  51 64 ";
    " 45 64  387 23 ";
    "  6 98  215 314";
    "*   +   *   +  "
]

[<Fact>]
let ``Can parse example input`` () =
    let parsed = parseOne exampleInput |> Array.ofList
    Assert.Equal(Multiply [ 123; 45; 6 ], parsed[0])
    Assert.Equal(Add [ 328; 64; 98 ], parsed[1])
    Assert.Equal(Multiply [ 51; 387; 215 ], parsed[2])
    Assert.Equal(Add [ 64; 23; 314 ], parsed[3])
    
[<Fact>]
let ``Can solve part one`` () =
    let parsed = parseOne exampleInput
    let result = resolveProblems parsed
    Assert.Equal(4277556L, result)
    
[<Fact>]
let ``Can parse the example input for part 2`` () =
    let parsed = parseTwo exampleInput |> Array.ofList
    Assert.Equal(Add [ 4; 431; 623 ], parsed[0])
    Assert.Equal(Multiply [ 175; 581; 32 ], parsed[1])
    Assert.Equal(Add [ 8; 248; 369 ], parsed[2])
    Assert.Equal(Multiply [ 356; 24; 1 ], parsed[3])

[<Fact>]
let ``Can solve part two`` () =
    let parsed = parseTwo exampleInput
    let result = resolveProblems parsed
    Assert.Equal(3263827L, result)

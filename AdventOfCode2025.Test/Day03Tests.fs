module Day03Tests

open Xunit
open AdventOfCode2025.Solutions.Day03

let exampleInput = [
    "987654321111111";
    "811111111111119";
    "234234234234278";
    "818181911112111";
]

[<Fact>]
let ``Can get maximum joltage for first example input`` () =
   let input = parse exampleInput
   let result = largestJoltage input[0]
   Assert.Equal(98, result)
   
[<Fact>]
let ``Can get maximum joltage for second example input`` () =
   let input = parse exampleInput
   let result = largestJoltage input[1]
   Assert.Equal(89, result)
   
[<Fact>]
let ``Can get maximum joltage for third example input`` () =
   let input = parse exampleInput
   let result = largestJoltage input[2]
   Assert.Equal(78, result)

[<Fact>]
let ``Can get maximum joltage for fourth example input`` () =
   let input = parse exampleInput
   let result = largestJoltage input[3]
   Assert.Equal(92, result)

[<Fact>]
let ``Can solve part 1 for the example input`` () =
   let input = parse exampleInput
   let result = partOne input
   Assert.Equal(357, result)
   
[<Fact>]
let ``Can get correct joltage of 89 for 1789`` () =
   let input = [ 1; 7; 8; 9 ]
   let result = largestJoltage input
   Assert.Equal(89, result)

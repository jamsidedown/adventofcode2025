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
let ``Can get maximum joltage for first example input for part one`` () =
   let input = parse exampleInput
   let result = largestJoltage 2 input[0]
   Assert.Equal(98L, result)
   
[<Fact>]
let ``Can get maximum joltage for second example input for part one`` () =
   let input = parse exampleInput
   let result = largestJoltage 2 input[1]
   Assert.Equal(89L, result)
   
[<Fact>]
let ``Can get maximum joltage for third example input for part one`` () =
   let input = parse exampleInput
   let result = largestJoltage 2 input[2]
   Assert.Equal(78L, result)

[<Fact>]
let ``Can get maximum joltage for fourth example input for part one`` () =
   let input = parse exampleInput
   let result = largestJoltage 2 input[3]
   Assert.Equal(92L, result)

[<Fact>]
let ``Can solve part one for the example input`` () =
   let input = parse exampleInput
   let result = partOne input
   Assert.Equal(357L, result)
   
[<Fact>]
let ``Can get correct joltage of 89 for 1789`` () =
   let input = [ 1; 7; 8; 9 ]
   let result = largestJoltage 2 input
   Assert.Equal(89L, result)

[<Fact>]
let ``Can create mapping for first example input`` () =
   let input = parse exampleInput
   let map = getPositions input[0]
   Assert.Equal<int>([ 0 ], Map.find 9 map)
   Assert.Equal<int>([ 1 ], Map.find 8 map)
   Assert.Equal<int>([ 2 ], Map.find 7 map)
   Assert.Equal<int>([ 3 ], Map.find 6 map)
   Assert.Equal<int>([ 4 ], Map.find 5 map)
   Assert.Equal<int>([ 5 ], Map.find 4 map)
   Assert.Equal<int>([ 6 ], Map.find 3 map)
   Assert.Equal<int>([ 7 ], Map.find 2 map)
   Assert.Equal<int>([ 8; 9; 10; 11; 12; 13; 14 ], Map.find 1 map)

[<Fact>]
let ``Can create mapping for third example input`` () =
   let input = parse exampleInput
   let map = getPositions input[2]
   Assert.False(Map.containsKey 9 map)
   Assert.Equal<int>([ 14 ], Map.find 8 map)
   Assert.Equal<int>([ 13 ], Map.find 7 map)
   Assert.False(Map.containsKey 6 map)
   Assert.False(Map.containsKey 5 map)
   Assert.Equal<int>([ 2; 5; 8; 11 ], Map.find 4 map)
   Assert.Equal<int>([ 1; 4; 7; 10 ], Map.find 3 map)
   Assert.Equal<int>([ 0; 3; 6; 9; 12 ], Map.find 2 map)
   Assert.False(Map.containsKey 1 map)
   
[<Fact>]
let ``Can get maximum joltage for first example for part two`` () =
   let input = parse exampleInput
   let result = largestJoltage 12 input[0]
   Assert.Equal(987654321111L, result)

[<Fact>]
let ``Can get maximum joltage for second example for part two`` () =
   let input = parse exampleInput
   let result = largestJoltage 12 input[1]
   Assert.Equal(811111111119L, result)
   
[<Fact>]
let ``Can get maximum joltage for third example for part two`` () =
   let input = parse exampleInput
   let result = largestJoltage 12 input[2]
   Assert.Equal(434234234278L, result)
   
[<Fact>]
let ``Can get maximum joltage for fourth example for part two`` () =
   let input = parse exampleInput
   let result = largestJoltage 12 input[3]
   Assert.Equal(888911112111L, result)

[<Fact>]
let ``Can get correct answer for part two`` () =
   let input = parse exampleInput
   let result = partTwo input
   Assert.Equal(3121910778619L, result)

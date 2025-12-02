module Day02Tests

open Xunit
open AdventOfCode2025.Solutions.Day02

let exampleInput = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,
1698522-1698528,446443-446449,38593856-38593862,565653-565659,
824824821-824824827,2121212118-2121212124"

[<Fact>]
let ``Test can parse example input`` () =
    let parsed = parse(exampleInput)
    let expected = [
        { start = 11; stop = 22 };
        { start = 95; stop = 115 };
        { start = 998; stop = 1012 };
        { start = 1188511880; stop = 1188511890 };
        { start = 222220; stop = 222224 };
        { start = 1698522; stop = 1698528 };
        { start = 446443; stop = 446449 };
        { start = 38593856; stop = 38593862 };
        { start = 565653; stop = 565659 };
        { start = 824824821; stop = 824824827 };
        { start = 2121212118; stop = 2121212124 };
    ]
    Assert.Equal<Range>(expected, parsed)
    
[<Fact>]
let ``Can find example repeats`` () =
    Assert.Equal<int64>([ 11L; 22L ], findDoubles 11 22)
    Assert.Equal<int64>([ 99L ], findDoubles 95 115)
    Assert.Equal<int64>([ 1010L ], findDoubles 998 1012)
    Assert.Equal<int64>([ 1188511885L ], findDoubles 1188511880 1188511890)
    Assert.Equal<int64>([ 222222L ], findDoubles 222220 222224)
    Assert.Equal<int64>([], findDoubles 1698522 1698528)
    Assert.Equal<int64>([ 446446L ], findDoubles 446443 446449)
    Assert.Equal<int64>([ 38593859L ], findDoubles 38593856 38593862)
    
[<Fact>]
let ``Can solve part 1 for the example input`` () =
    let parsed = parse(exampleInput)
    let result = partOne(parsed)
    Assert.Equal(1227775554L, result)

[<Fact>]
let ``Can find all example repeats`` () =
    Assert.Equal<int64>([ 11L; 22L ], findAllRepeats 11 22)
    Assert.Equal<int64>([ 99L; 111L ], findAllRepeats 95 115)
    Assert.Equal<int64>([ 999L; 1010L ], findAllRepeats 998 1012)
    Assert.Equal<int64>([ 1188511885L ], findAllRepeats 1188511880 1188511890)
    Assert.Equal<int64>([ 222222L ], findAllRepeats 222220 222224)
    Assert.Equal<int64>([], findAllRepeats 1698522 1698528)
    Assert.Equal<int64>([ 446446L ], findAllRepeats 446443 446449)
    Assert.Equal<int64>([ 38593859L ], findAllRepeats 38593856 38593862)
    Assert.Equal<int64>([ 565656L ], findAllRepeats 565653 565659)
    Assert.Equal<int64>([ 824824824L ], findAllRepeats 824824821 824824827)
    Assert.Equal<int64>([ 2121212121L ], findAllRepeats 2121212118 2121212124)

[<Fact>]
let ``Can solve part 2 for the example input`` () =
    let parsed = parse(exampleInput)
    let result = partTwo(parsed)
    Assert.Equal(4174379265L, result)

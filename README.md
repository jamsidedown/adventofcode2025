# Advent of Code 2025

F# solutions for Advent of Code 2025 for as many days as I can manage

## Requirements

- [.NET 10.0 SDK](https://dotnet.microsoft.com/en-us/download)
- [Microsoft.Z3 4.15.4](https://github.com/Z3Prover/z3/releases/tag/z3-4.15.4)

## Setup

Input files should be put into `./input` and named `day01.txt`, `day02.txt`, ..., `day12.txt`

Microsoft.Z3 needs to be installed in `./packages` for the solutions to Day 10 to work, this can be done on arm64 MacOS devices using the `fetch.sh` script in that directory.

```sh
$ cd packages

$ ./fetch.sh
  % Total    % Received % Xferd  Average Speed   Time    Time     Time  Current
                                 Dload  Upload   Total   Spent    Left  Speed
  0     0    0     0    0     0      0      0 --:--:--  0:00:09 --:--:--     0
100 45.5M  100 45.5M    0     0  4624k      0  0:00:10  0:00:10 --:--:-- 82.0M
  % Total    % Received % Xferd  Average Speed   Time    Time     Time  Current
                                 Dload  Upload   Total   Spent    Left  Speed
  0     0    0     0    0     0      0      0 --:--:--  0:00:05 --:--:--     0
100 35.3M  100 35.3M    0     0  5526k      0  0:00:06  0:00:06 --:--:-- 5526k
Archive:  z3-4.15.4-arm64-osx-13.7.6.zip
  inflating: z3-4.15.4-arm64-osx-13.7.6/LICENSE.txt
  inflating: z3-4.15.4-arm64-osx-13.7.6/bin/z3
  inflating: z3-4.15.4-arm64-osx-13.7.6/bin/Microsoft.Z3.xml
  inflating: z3-4.15.4-arm64-osx-13.7.6/bin/libz3java.dylib
  inflating: z3-4.15.4-arm64-osx-13.7.6/bin/libz3.a
  inflating: z3-4.15.4-arm64-osx-13.7.6/bin/com.microsoft.z3.jar
  inflating: z3-4.15.4-arm64-osx-13.7.6/bin/Microsoft.Z3.deps.json
  inflating: z3-4.15.4-arm64-osx-13.7.6/bin/Microsoft.Z3.dll
  ...
```

For other platforms, download the appropriate `z3-4.15.4-<platform>-13.7.6.zip` from [here](https://github.com/Z3Prover/z3/releases/tag/z3-4.15.4) and change the `AdventOfCode2025.Solutions.fsproj` project file to point to the new z3 dll for your platform.

At this time the `Microsoft.Z3` package on Nuget hasn't been updated for over 2 years, and doesn't support arm64.

## Testing and running

### To run unit tests

```sh
$ dotnet test
Restore complete (0.4s)
  AdventOfCode2025.Core net10.0 succeeded (0.0s) → AdventOfCode2025.Core/bin/Debug/net10.0/AdventOfCode2025.Core.dll
  AdventOfCode2025.Solutions net10.0 succeeded (0.0s) → AdventOfCode2025.Solutions/bin/Debug/net10.0/AdventOfCode2025.Solutions.dll
  AdventOfCode2025.Test net10.0 succeeded (0.1s) → AdventOfCode2025.Test/bin/Debug/net10.0/AdventOfCode2025.Test.dll
[xUnit.net 00:00:00.00] xUnit.net VSTest Adapter v3.1.4+50e68bbb8b (64-bit .NET 10.0.0)
[xUnit.net 00:00:00.04]   Discovering: AdventOfCode2025.Test
[xUnit.net 00:00:00.06]   Discovered:  AdventOfCode2025.Test
[xUnit.net 00:00:00.07]   Starting:    AdventOfCode2025.Test
[xUnit.net 00:00:00.10]   Finished:    AdventOfCode2025.Test
  AdventOfCode2025.Test test net10.0 succeeded (0.6s)

Test summary: total: 3, failed: 0, succeeded: 3, skipped: 0, duration: 0.6s
Build succeeded in 1.3s
```

### To run solutions

```sh
# run the latest day
$ dotnet run --project AdventOfCode2025
Day 1
Part 1: 1234
Part 2: 5678

# run all days
$ dotnet run --project AdventOfCode2025 --all
Day 1
Part 1: 1234
Part 2: 5678
...

# run specific days
$ dotnet run --project AdventOfCode2025 1 2 3
Day 1
Part 1: 1234
Part 2: 5678
...
```

### To benchmark

I've been using [hyperfine](https://github.com/sharkdp/hyperfine) to benchmark my solutions

```sh
# change into exe directory
$ cd AdventOfCode2025

# publish as release
$ dotnet publish -c Release
Restore complete (0.5s)
  AdventOfCode2025.Core net10.0 succeeded (0.1s) → ~/Documents/code/aoc/adventofcode2025/AdventOfCode2025.Core/bin/Release/net10.0/AdventOfCode2025.Core.dll
  AdventOfCode2025.Solutions net10.0 succeeded (0.2s) → ~/Documents/code/aoc/adventofcode2025/AdventOfCode2025.Solutions/bin/Release/net10.0/AdventOfCode2025.Solutions.dll
  AdventOfCode2025 net10.0 linux-x64 succeeded (0.5s) → bin/Release/net10.0/linux-x64/publish/

Build succeeded in 1.7s

# run release version (assuming linux x64)
$ ./bin/Release/net10.0/linux-x64/publish/AdventOfCode2025 --all

# benchmark specific day using hyperfine
$ hyperfine --warmup 3 './bin/Release/net10.0/linux-x64/publish/AdventOfCode2025 1'
Benchmark 1: ./bin/Release/net10.0/linux-x64/publish/AdventOfCode2025 1
  Time (mean ± σ):      21.5 ms ±   0.3 ms    [User: 17.9 ms, System: 3.8 ms]
  Range (min … max):    20.9 ms …  22.8 ms    131 runs
  
# benchmark all days using hyperfine
$ hyperfine --warmup 3 './bin/Release/net10.0/linux-x64/publish/AdventOfCode2025 --all'
...
```

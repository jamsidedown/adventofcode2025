# Advent of Code 2025

F# solutions for Advent of Code 2025 for as many days as I can manage

## Requirements

```sh
$ dotnet --version
10.0.100
```

## Setup

Input files should be put into `./input` and named `day01.txt`, `day02.txt`, ..., `day12.txt`

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

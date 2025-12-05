module AdventOfCode2025.Core.Range

type Range = { start: int64; stop: int64 } with
    
    static member contains (range: Range) (value: int64): bool =
        value >= range.start && value <= range.stop

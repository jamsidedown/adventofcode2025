module AdventOfCode2025.Core.XyPair

type XyPair = { x: int64; y: int64 } with

    static member add (left: XyPair) (right: XyPair): XyPair =
        { x = left.x + right.x; y = left.y + right.y }

    static member addScalar (left: XyPair) (right: int64): XyPair =
        { x = left.x + right; y = left.y + right }

    static member zero = { x = 0; y = 0 }

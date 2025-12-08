module AdventOfCode2025.Core.Xyz

open System

type Xyz = { x: int64; y: int64; z: int64 } with
    
    static member add (left: Xyz) (right: Xyz): Xyz =
        {
            x = left.x + right.x
            y = left.y + right.y
            z = left.z + right.z
        }
    
    static member sub (left: Xyz) (right: Xyz): Xyz =
        {
            x = left.x - right.x
            y = left.y - right.y
            z = left.z - right.z
        }
        
    static member mul (left: Xyz) (right: Xyz): Xyz =
        {
            x = left.x * right.x
            y = left.y * right.y
            z = left.z * right.z
        }
        
    static member distance (left: Xyz) (right: Xyz): double =
        let diff = Xyz.sub left right
        let diffSquared = Xyz.mul diff diff
        let distSquared = diffSquared.x + diffSquared.y + diffSquared.z
        Math.Sqrt(double distSquared)
    
    static member zero = { x = 0; y = 0; z = 0 }
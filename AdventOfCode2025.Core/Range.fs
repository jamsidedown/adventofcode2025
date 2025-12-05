module AdventOfCode2025.Core.Range

type Range = { start: int64; stop: int64 } with
    
    static member contains (range: Range) (value: int64): bool =
        value >= range.start && value <= range.stop

    static member length (range: Range): int64 =
        range.stop - range.start + 1L
        
    static member neighbours (first: Range) (second: Range) : bool =
        first.stop = (second.start - 1L) || second.stop = (first.start - 1L)
    
    static member overlaps (first: Range) (second: Range): bool =
        (first.start >= second.start && first.start <= second.stop)
        || (second.start >= first.start && second.start <= first.stop)
        
    static member combine (first: Range) (second: Range): Range option =
        if Range.overlaps first second || Range.neighbours first second
        then Some { start = (min first.start second.start); stop = (max first.stop second.stop) }
        else None

    static member rangeCompare (left: Range) (right: Range): int =
        left.start.CompareTo right.start
        
    static member reduce (ranges: Range list): Range list =
        let rec loop (current: Range list): Range list =
            let initialLength = List.length current
            
            let rec recurse (remaining: Range list): Range list =
                match remaining with
                | a :: b :: tail ->
                    match Range.combine a b with
                    | Some c -> c :: recurse tail
                    | None -> a :: recurse (b :: tail)
                | _ -> remaining
                
            let reduced = current |> List.sortWith Range.rangeCompare |> recurse

            if List.length reduced = initialLength
            then reduced
            else loop reduced
        loop ranges

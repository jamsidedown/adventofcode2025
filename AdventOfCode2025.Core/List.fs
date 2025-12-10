module AdventOfCode2025.Core.List

let rec each (remaining: 'a list): ('a * 'a list) list =
    match remaining with
    | head :: tail -> (head, tail) :: each tail
    | [] -> []

let combinations (count: int) (lst: 'a list): 'a list list =    
    let rec loop (iter: int) (acc: 'a list) (remaining: 'a list): 'a list list =
        if iter = count then [ acc ] else
        if List.isEmpty remaining then [] else
        each remaining
        |> List.collect (fun (head, tail) ->
            loop (iter + 1) (head :: acc) tail)

    loop 0 [] lst
    |> List.filter (fun ls -> (List.length ls) = count)
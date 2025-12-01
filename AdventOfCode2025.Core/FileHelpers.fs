module AdventOfCode2025.Core.FileHelpers

open System.IO

let getFilePath (day: int): string =
    let filename = $"input/day{day:D2}.txt"
    
    let rec loopDir (current: DirectoryInfo): string =
        let filepath = Path.Join [| current.FullName; filename |]
        if File.Exists filepath then
            filepath
        else
            if current.Parent <> null then
                loopDir current.Parent
            else
                raise (FileNotFoundException $"Can't find %s{filename}")
    
    Directory.GetCurrentDirectory()
    |> DirectoryInfo
    |> loopDir

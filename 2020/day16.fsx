open System
open System.Linq

let readLines filePath = System.IO.File.ReadAllText(filePath)

let testFile =
    readLines @"D:\code\bjorndaniel\adventofcode\2020\day16-test.txt"

let file =
    readLines @"D:\code\bjorndaniel\adventofcode\2020\day16.txt"

let testChunks =
    testFile.Split([| "\r\n\r\n" |], StringSplitOptions.RemoveEmptyEntries)

let chunks =
    file.Split([| "\r\n\r\n" |], StringSplitOptions.RemoveEmptyEntries)

let testRangeLines =
    testChunks.[0].Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.toList

let rangeLines =
    chunks.[0].Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.toList

let myTestTicket =
    testChunks.[1].Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries).[1].Split(",")
    |> Seq.map (fun a -> a |> int)
    |> Seq.toList

let myTicket =
    chunks.[1].Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries).[1].Split(",")
    |> Seq.map (fun a -> a |> int)
    |> Seq.toList

let testTickets =
    testChunks.[2].Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries).[1..]
    |> Seq.map (fun a ->
        a.Split(",")
        |> Seq.map (fun a -> a |> int)
        |> Seq.toList)
    |> Seq.toList

let tickets =
    chunks.[2].Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries).[1..]
    |> Seq.map (fun a ->
        a.Split(",")
        |> Seq.map (fun a -> a |> int)
        |> Seq.toList)
    |> Seq.toList

let rec findRanges (l: List<string>) r =
    match l with
    | [] -> r
    | h :: t ->
        let rowRanges =
            h.Split(":", StringSplitOptions.RemoveEmptyEntries)

        let row =
            rowRanges.[1].Split(" ", StringSplitOptions.RemoveEmptyEntries)

        let r1S = row.[0].Split("-").[0] |> int
        let r1E = row.[0].Split("-").[1] |> int
        let range1 = [ r1S .. r1E ]
        let r2S = row.[2].Split("-").[0] |> int
        let r2E = row.[2].Split("-").[1] |> int
        let range2 = [ r2S .. r2E ]
        findRanges t r @ range1 @ range2

let testRanges = (findRanges testRangeLines [])
let ranges = (findRanges rangeLines [])

let rec findErrors (ranges: List<int>) (tickets: List<List<int>>) sum =
    match tickets with
    | [] -> sum
    | h :: t ->
        let missing = Linq.Enumerable.Except(h, ranges).Sum()
        findErrors ranges t (sum + missing)

let testResult = (findErrors testRanges testTickets 0)

printfn "Testresult %A" testResult
let result = (findErrors ranges tickets 0)
printfn "Result %A" result

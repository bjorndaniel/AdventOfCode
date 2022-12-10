open System
open System.Linq
open System.Text.RegularExpressions

let readLines filePath = System.IO.File.ReadAllText(filePath)

let testLines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day10-test.txt"

let ta =
    testLines.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.toList
    |> Seq.map (fun x -> x |> int)
    |> Seq.sort
    |> Seq.toList

let lines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day10.txt"

let a =
    lines.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.toList
    |> Seq.map (fun x -> x |> int)
    |> Seq.sort
    |> Seq.toList

let testAdapters = 0 :: ta
let adapters = 0 :: a

let rec chainAdapters l p v =
    match l with
    | [] ->
        let newV = (fst (v), snd (v) + 1)
        newV
    | h :: t ->
        if h - p < 2 then
            let newV = (fst (v) + 1, snd (v))
            chainAdapters t h newV
        else
            let newV = (fst (v), snd (v) + 1)
            chainAdapters t h newV

let testDifferences = (chainAdapters ta 0 (0, 0))

let testResult =
    fst (testDifferences) * snd (testDifferences)

printfn "Testresult part 1: %A" testResult

let differences = (chainAdapters a 0 (0, 0))

let result = fst (differences) * snd (differences)

printfn "Result part 1: %A" result
//Part 2
//FROM: https://stackoverflow.com/questions/1222185/most-elegant-combinations-of-elements-in-f
let rec comb n l =
    match n, l with
    | 0, _ -> [ [] ]
    | _, [] -> []
    | k, (x :: xs) -> List.map ((@) [ x ]) (comb (k - 1) xs) @ comb k xs

let isValid (c: List<int>) =
    let valid = abs (c.[0] - c.[1]) < 4
    valid

type Edge =
    { Node: int option
      Children: int list }

let rec createGraph (l: List<(int * int)>) (g: List<Edge>) =
    match l with
    | [] -> g
    | h :: t ->
        let edge =
            g
            |> List.choose (fun e -> if e.Node = Some(fst h) then (Some e) else None)

        match edge with
        | [] ->
            let newEdge =
                { Node = Some(fst h)
                  Children = (snd h :: []) }

            createGraph t (newEdge :: g)
        | e ->
            let x = List.head e

            let newEdge =
                { Node = x.Node
                  Children = (snd h :: x.Children) }

            let newG =
                g
                |> Seq.map (fun a -> if a.Node = newEdge.Node then newEdge else a)
                |> Seq.toList

            createGraph t newG



let testCombos =
    (comb 2 testAdapters)
    |> List.filter (fun x -> (isValid x))
    |> List.map (fun x -> (x.[0], x.[1]))

let combos =
    (comb 2 adapters)
    |> List.filter (fun x -> (isValid x))
    |> List.map (fun x -> (x.[0], x.[1]))
// printfn "%A" testCombos

let tg = createGraph testCombos []

let g = createGraph combos []
printfn "%A" tg

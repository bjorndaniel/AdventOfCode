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

let testCombos =
    (comb 2 testAdapters)
    |> List.filter (fun x -> (isValid x))
    |> List.map (fun x -> (x.[0], x.[1]))

let combos =
    (comb 2 adapters)
    |> List.filter (fun x -> (isValid x))
    |> List.map (fun x -> (x.[0], x.[1]))
// printfn "%A" testCombos

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

let tg = createGraph testCombos []
let g = createGraph combos []
// printfn "%A" g

let rec countPaths s d g v (c: int64) =
    if c % (1000000 |> int64) = (0 |> int64) then (printfn "%A %A" c (System.DateTime.Now.ToLongTimeString()))
    let mutable newCount = c
    if s.Node = d.Node then
        newCount <- newCount + (1 |> int64)
    else
        for n in s.Children do
            let edge =
                g
                |> List.choose (fun e -> if e.Node = Some n then (Some e) else None)

            let found =
                v
                |> List.choose (fun e -> if e.Node = Some n then (Some e) else None)

            match found with
            | [] ->
                match edge with
                | [] -> newCount <- countPaths d d g v newCount
                | e -> newCount <- countPaths (List.head e) d g v newCount
            | _ -> newCount <- countPaths d d g v newCount
    newCount

let rec findStart g (s: Edge) =
    match g with
    | [] -> s
    | h :: t -> if h.Node < s.Node then (findStart t h) else (findStart t s)

let rec findEnd g (s: Edge) =
    match g with
    | [] -> s
    | h :: t -> if h.Node > s.Node then (findEnd t h) else (findEnd t s)

let testStart =
    (findStart
        tg
         ({ Node = Some Int32.MaxValue
            Children = [] }))

let testDest =
    (findEnd
        tg
         ({ Node = (Some Int32.MinValue)
            Children = [] }))

let testResultP2 =
    (countPaths testStart testDest tg [ testStart ] (0 |> int64))

printfn "Testresult P2 %A" testResultP2

let start =
    (findStart
        g
         ({ Node = Some Int32.MaxValue
            Children = [] }))

let dest =
    (findEnd
        g
         ({ Node = (Some Int32.MinValue)
            Children = [] }))

let resultP2 =
    (countPaths start dest g [ start ] (0 |> int64))

printfn "Result P2 %A" resultP2

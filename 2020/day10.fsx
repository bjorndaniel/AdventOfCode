open System
open System.Linq
open System.Text.RegularExpressions

let readLines filePath = System.IO.File.ReadAllText(filePath)

let testLines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day10-test.txt"

let testAdapters =
    testLines.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.toList
    |> Seq.map (fun x -> x |> int)
    |> Seq.sort
    |> Seq.toList

let lines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day10.txt"

let adapters =
    lines.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.toList
    |> Seq.map (fun x -> x |> int)
    |> Seq.sort
    |> Seq.toList

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

let testDifferences = (chainAdapters testAdapters 0 (0, 0))

let testResult =
    fst (testDifferences) * snd (testDifferences)

printfn "Testresult part 1: %A" testResult

let differences = (chainAdapters adapters 0 (0, 0))

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
    let valid =
        abs (c.[0] - c.[1]) = 1
        || abs ((c.[0] - c.[1])) = 3

    valid

let combos =
    (comb 2 testAdapters)
    |> List.filter (fun x -> (isValid x))
    |> List.map (fun x -> (x.[0], x.[1]))

printfn "%A" combos
type Edge = { Node : int option; Children : int list }

// let addToGraph (v:(int*int)) (g:List<Graph>) =
//     match g with
//     | Some x -> 
//         let edge = fst v
//         let newNode = {Node = Some edge ; Children = (snd v::g.Children)} 
//         let newG = g |> Seq.map (fun x -> if x.Node = fst(v) then newNode else x) 
//         newG
//     | None -> 
//         let newG = {Node = fst(v), Children= snd(v)::[]}
//         newG
        
let rec createGraph l g = 
    match l with
    | [] -> g
    | h::t -> 
       let edge = g |> List.choose (fun e -> if e.Node = fst h then Some e else None)
       printfn "%A" edge
       match edge with
       | x ->
            let newEdge = {Node = x.Node ; Children = snd v :: x.Children}
            let newG = g |> Seq.map (fun a -> if a.Node = fst h then newEdge else h ) |> Seq.toList
            createGraph t newG
       | None -> 
            let newG = {Node = fst v; Children = snd v :: []}
            createGraph t newG
let g = createGraph combos []
printfn "Graph %A" g


// type 'a AdjacencyGraph = 'a Node list

// let paths start finish (g : 'a AdjacencyGraph) =
//         let map = g |> Map.ofList
//       let rec loop route visited =
//          [        let current = List.head route
//              if current = finish then
//               yield List.rev route
//          else
//             for next in map.[current] do
//                          if visited |> Set.contains next |> not then
//                                   yield! loop (next::route) (Set.add next visited)     ]
//                   loop [start] <| Set.singleton start

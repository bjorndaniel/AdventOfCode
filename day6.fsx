#r "../packages/fsharp.data/3.3.2/lib/net45/Fsharp.Data.dll"
open FSharp.Data

type InputData = JsonProvider<""" {"input": ["asd","asdf","asdf"] } """>

let rec createTuples (result: list<string * string>) (orbits: list<string>) =
    let o = List.head orbits
    let sides = o.Split ')'
    let tup = (sides.[0], sides.[1])
    let newRes = List.append result [ tup ]
    if (List.length orbits) = 1 then newRes
    else createTuples newRes (List.tail orbits)

//Test
let testProgram = (Array.toList (InputData.Load("day6.json").Input))
let paths = createTuples [] testProgram

//Adapted from https://cockneycoder.wordpress.com/2015/06/14/determining-graph-sizes-efficiently-with-f/
let network =
    paths
    |> List.collect(fun (a,b) -> [ a,b; b,a ]) // make bi-directional
    |> Seq.groupBy fst // group up by node
    |> Seq.toArray
    |> Array.map(fun (a, rels) -> a, rels |> Seq.toArray |> Array.map snd)
    |> Map.ofArray

let rec getNetworkSize count (previous, location) =
    // avoid infinitely cycling back on ourselves.
    if location = "YOU" then
        let santaOrbit = count - 2
        printfn("FOUND YOU %A") santaOrbit
        count
    else
        match network.[location] |> Array.filter((<>) previous) with
        | [| |] -> count
        | destinations ->
            let largestHop =
                destinations
                |> Array.map(fun dest -> getNetworkSize (count + 1) (location, dest))
            (Array.sum largestHop) + count



let size = getNetworkSize 0 ("", "COM")
getNetworkSize 0 ("", "SAN")
printfn("%A") size



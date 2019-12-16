#r @"../packages/fsharp.data/3.3.2/lib/net45/Fsharp.Data.dll"
open System
open FSharp.Data
type InputData = JsonProvider<""" {"data": "123wer" } """>

let rec splitBy result length input =
    match input with 
    | _ when (String.length input) <= 25 -> List.append [input] result
    | _ -> splitBy (List.append [input.[..(length-1)]] result) length input.[25..]

let rec countChar current character (input:String) = 
    match input with
    | _ when (String.length input) = 0 -> current
    | _ -> 
        if input.[0] = character then 
            countChar (current + 1) character input.[1..]
        else countChar current character input.[1..]        

let rec minZeros currentMin input =
    match input with
    | [] -> currentMin
    | _ ->
        let newVal = countChar 0 '0' (List.head input)
        if newVal < (fst currentMin) then 
            let newMin = (newVal,List.head input)
            minZeros newMin (List.tail input)
        else minZeros currentMin (List.tail input)        

let rec layers output input = 
    match input with
    | [] -> output
    | _ ->
        let layer = input.[..5]
        let newOutput = List.append [layer] output
        layers newOutput input.[6..]

let rec concatLayers output (input:list<list<string>>) =
   match input with 
   | [] -> output
   | _ -> 
    let layer = String.concat "" (List.head input)
    let newOutput = List.append [layer] output
    concatLayers newOutput (List.tail input)

let checksum input = 
    let minz = minZeros (30,"") input
    let ones = countChar 0 '1'  (snd minz)
    let twos = countChar 0 '2' (snd minz)
    ones * twos

let input =  InputData.Load("day8.json").Data
let strings =  (splitBy [] 25 input)
let lrs = layers [] strings
let cncLayers = concatLayers [] lrs
printfn("%A") (checksum cncLayers)

//Part two
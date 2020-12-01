#r @"../packages/fsharp.data/3.3.2/lib/net45/Fsharp.Data.dll"
open System
open FSharp.Data
type InputData = JsonProvider<""" {"data": "123wer" } """>

let rec splitBy result length input =
    match input with 
    | _ when (String.length input) <= length -> List.append [input] result
    | _ -> splitBy (List.append [input.[..(length-1)]] result) length input.[length..]

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

let rec layers output (height:int) input = 
    match input with
    | [] -> output
    | _ ->
        let arrLength = height - 1
        let layer = List.rev input.[..arrLength]
        let newOutput = List.append [layer] output
        layers newOutput height input.[height..]

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
let lrs = layers [] 6 strings
let cncLayers = concatLayers [] lrs
printfn("%A") (checksum cncLayers)

//Part two
let rec processPixel output index (layers:list<string>) = 
    match layers with
    | [] -> 
        output
    | _ -> 
        let layer = List.head layers
        let next = int (layer.[index..index])
        if next <> 2 then processPixel next index (List.tail layers)
        else processPixel output index (List.tail layers)

let processImage (layers:list<string>) =
    let layerLength = (String.length (List.head layers)) - 1
    let mutable output = ""
    for i = 0 to layerLength do
        let number = processPixel 6 i layers
        let str = if number = 1 then "X" else " "
        output <- output + str
    output     

let printImage rowLength input =
    let rows = List.rev (splitBy [] rowLength input)
    for i = 0 to (List.length rows) - 1 do
        printfn("%A") rows.[i..i]

let testImage = "0222112222120000"
let rows = (splitBy [] 25 input)
let pixelLayers = concatLayers [] (layers [] 6 rows)
let image = (processImage pixelLayers)
printImage 25 image

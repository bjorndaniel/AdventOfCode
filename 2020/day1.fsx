#r "nuget: FSharp.Data"
open System
open FSharp.Data

type InputData = JsonProvider<""" {"numbers": [1,3,123] } """>

let day1TestInput =
    InputData.Load("C:\\Projects\\bjorndaniel\\AdventOfCode\\2020\\day1.json")

//FROM: https://stackoverflow.com/questions/1222185/most-elegant-combinations-of-elements-in-f
let rec comb n l =
    match n, l with
    | 0, _ -> [ [] ]
    | _, [] -> []
    | k, (x :: xs) -> List.map ((@) [ x ]) (comb (k - 1) xs) @ comb k xs

let rec sum l =
    match l with
    | [] -> 0
    | [ a; b ] :: _ when a + b = 2020 -> a * b
    | _ -> sum (List.tail l)

let rec sum3 l =
    match l with
    | [] -> 0
    | [ a; b; c ] :: _ when a + b + c = 2020 -> a * b * c
    | _ -> sum3 (List.tail l)


//printfn "%A" (comb 3 (Array.toList day1TestInput.Numbers))
printfn "%A" (sum3 (comb 3 (Array.toList day1TestInput.Numbers)))

open System
open System.Linq
open System.Text.RegularExpressions

let readLines filePath = System.IO.File.ReadAllText(filePath)

let testLines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day9-test.txt"


let lines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day9.txt"

let testCipher =
    testLines.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.toList
    |> Seq.map (fun x -> x |> int64)
    |> Seq.toList

let cipher =
    lines.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.toList
    |> Seq.map (fun x -> x |> int64)
    |> Seq.toList

//FROM: https://stackoverflow.com/questions/1222185/most-elegant-combinations-of-elements-in-f
let rec comb n l =
    match n, l with
    | 0, _ -> [ [] ]
    | _, [] -> []
    | k, (x :: xs) -> List.map ((@) [ x ]) (comb (k - 1) xs) @ comb k xs


let containsSum l n =
    let pairs = comb 2 l

    let sums =
        pairs |> List.map (fun x -> x.[0] + x.[1])

    if List.contains n sums then true else false

let rec findInvalidNumber (l: List<int64>) (p: int) (i: int): int64 =
    match i with
    | x when x = (List.length l) -> -1 |> int64
    | _ ->
        let numbers =
            l |> Seq.skip i |> Seq.take p |> Seq.toList

        let currentValue = l.[i + p]
        let c = containsSum numbers currentValue
        if c then findInvalidNumber l p (i + 1) else l.[i + p]

let testResult = findInvalidNumber testCipher 5 0
printfn "Testresult part 1: %A" testResult
let result = findInvalidNumber cipher 25 0
printfn "Result part 1: %A" result


//Part 2
let rec findSum (l: List<int64>) i s n =
    match i with
    | x when x = (List.length l) -> (false, [])
    | _ ->
        let sum = s + l.[i]
        if sum = n then (true, (List.take i l))
        else if sum > n then (false, [])
        else findSum l (i + 1) sum n

let rec findWeakness (l: List<int64>) i n: int64 =
    match i with
    | x when x = (List.length l) -> -1 |> int64
    | _ ->
        let foundSum =
            findSum (l |> Seq.skip i |> Seq.toList) 0 (0 |> int64) n

        if fst (foundSum) then
            let numbers = snd (foundSum)
            let smallest = numbers |> List.min
            let largest = numbers |> List.max
            largest + smallest
        else
            findWeakness l (i + 1) n

let testResultP2 = findWeakness testCipher 0 testResult
printfn "Testresult part 2: %A" testResultP2
let resultP2 = findWeakness cipher 0 result
printfn "Result part 2: %A" resultP2

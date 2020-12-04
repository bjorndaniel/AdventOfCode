open System

let readLines filePath = System.IO.File.ReadLines(filePath)

let testLines =
    readLines @"C:\\Projects\\bjorndaniel\\AdventOfCode\\2020\\day3-test.txt"

let lines =
    readLines @"C:\\Projects\\bjorndaniel\\AdventOfCode\\2020\\day3.txt"

let paths =
    [ (1, 1)
      (3, 1)
      (5, 1)
      (7, 1)
      (1, 2) ]

let rec duplicate s l =
    let newLine = String.replicate 2 s
    if String.length newLine < l then duplicate newLine l else newLine


let rec travel forest position step jump count =
    match forest with
    | [] -> count
    | forest when List.length forest = 1 && jump ->
        let lastLine = (List.head forest)

        let line =
            if String.length lastLine <= position then duplicate lastLine (position + 1) else lastLine

        match line.[position] with
        | '#' -> count + 1
        | _ -> count

    | h :: t ->
        let line =
            if String.length h <= position then duplicate h (position + 1) else h

        let nextPos = position + step
        let tailLength = List.length t
        match line.[position] with
        | '#' ->
            if jump
            then travel (List.tail t) nextPos step jump (count + 1)
            else travel t nextPos step jump (count + 1)
        | _ ->
            if jump
            then travel (List.tail t) nextPos step jump count
            else travel t nextPos step jump count

let forest = (List.tail (Seq.toList lines))

let testForest = (List.tail (Seq.toList testLines))

let result = travel forest 3 3 false 0
printfn "Result part 1: %A" result

let testResult = travel forest 1 1 true 0

printfn "Testresult %A" testResult
let mutable sum = (int64) 1

for p in paths do
    let nextForest =
        if (snd (p) > 1) then (List.tail forest) else forest

    printfn "%A" (List.length nextForest)

    let result =
        travel nextForest (fst (p)) (fst (p)) (snd (p) > 1) 0

    printfn "%A" result
    sum <- sum * (int64) result

printfn "Result part 2: %A" sum

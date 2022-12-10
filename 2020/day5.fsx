let readLines filePath = System.IO.File.ReadLines(filePath)

let testLines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day5-test.txt"

let lines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day5.txt"

let rec findPlace l n r =
    // printfn "%A %A" l n
    match l with
    | [] -> -1
    | l when List.length l = 1 ->
        let h = List.head l
        match h with
        | x when x = 'F' || x = 'L' -> fst (n)
        | x when x = 'B' || x = 'R' -> snd (n)
        | _ -> -1
    | h :: t ->
        match h with
        | x when x = 'F' || x = 'L' ->
            let half = (snd (n) - fst (n)) / 2
            findPlace t (fst (n), fst (n) + half) r
        | x when x = 'B' || x = 'R' ->
            let half = (snd (n) - fst (n)) / 2
            findPlace t (fst (n) + half + 1, snd (n)) r

        | _ -> -1

let getSeatID (card: string) (id: int) =
    let row =
        (findPlace (Seq.toList card.[..6]) (0, 127) -1)

    let seat =
        (findPlace (Seq.toList card.[7..]) (0, 7) -1)

    row * 8 + seat

let rec findMaxRow l r =
    match l with
    | [] -> r
    | h :: t ->
        let seatId = getSeatID h 0
        let high = if seatId > r then seatId else r
        findMaxRow t high

let tResult = findMaxRow (Seq.toList testLines) 0

printfn "Part 1 testresult: %A" tResult

let result = findMaxRow (Seq.toList lines) 0

printfn "Part 1 result: %A" result

let rec getIds s r =
    match s with
    | [] -> r
    | h :: t -> getIds t ((getSeatID h 0) :: r)

let allIds =
    (getIds (Seq.toList lines) [])
    |> List.sortBy (fun x -> x)

//FROM: https://stackoverflow.com/questions/1175056/value-of-the-last-element-of-a-list
let rec last l =
    match l with
    | hd :: [] -> hd
    | hd :: tl -> last tl
    | _ -> failwith "Empty list."

let high = last allIds
let low = List.head allIds

let allInts = Seq.toList (seq { low .. high })
let sumAll = List.sum allInts
let sumIds = List.sum allIds
let missing = sumAll - sumIds
printfn "Result part 2 %A" missing

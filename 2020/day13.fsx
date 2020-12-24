open System

let testInput = (939, "7,13,x,x,59,x,31,19")

let input =
    (1008713,
     "13,x,x,41,x,x,x,x,x,x,x,x,x,467,x,x,x,x,x,x,x,x,x,x,x,19,x,x,x,x,17,x,x,x,x,x,x,x,x,x,x,x,29,x,353,x,x,x,x,x,37,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,23")

let validBussesTest =
    snd(testInput).Split(",")
    |> Seq.filter (fun x -> x <> "x")
    |> Seq.map (fun x -> x |> int)
    |> Seq.toList

let validBusses =
    snd(input).Split(",")
    |> Seq.filter (fun x -> x <> "x")
    |> Seq.map (fun x -> x |> int)
    |> Seq.toList

let testTimetable = (fst (testInput), validBussesTest)
let timeTable = (fst (input), validBusses)


let rec nextValidBus startTime busses departure =
    match busses with
    | [] -> departure
    | h :: t ->
        let time = startTime % h
        let nextDeparture = (startTime + h - time) - startTime

        if nextDeparture < snd (departure)
        then nextValidBus startTime t (h, nextDeparture)
        else nextValidBus startTime t departure

// let testResult =
//     (nextValidBus (fst (testTimetable)) (snd (testTimetable)) (0, Int32.MaxValue))

// printfn "Testresult %A" ((fst (testResult) * (snd (testResult))))

// let result =
//     (nextValidBus (fst (timeTable)) (snd (timeTable)) (0, Int32.MaxValue))

// printfn "Result %A" ((fst (result) * (snd (result))))

//Part 2
//Adapted from C# https://rosettacode.org/wiki/Chinese_remainder_theorem#C.23
let rec ModMultInverse a m x =
    let b = a % m

    match x with
    | i when (i * b) % m = (1 |> int64) -> i
    | i when x >= m -> (1 |> int64)
    | _ -> ModMultInverse a m (x + (1 |> int64))

let CRT (a: int64 list) (b: int64 list) =
    let prod = List.reduce (*) a
    let mutable sm = 0 |> int64
    let mutable p = 0 |> int64
    for i = 0 to ((Seq.length a) - 1) do
        p <- prod / a.[i]

        sm <-
            sm
            + (b.[i] * (ModMultInverse p a.[i]) (1 |> int64))
              * p

    (sm % prod)

let testPart2 =
    snd(testInput).Split(",")
    |> Seq.mapi (fun i x -> (x, (if x <> "x" then (x |> int) - i else i)))
    |> Seq.filter (fun a -> fst (a) <> "x")
    |> Seq.map (fun a -> (fst (a) |> int64), snd (a) |> int64)

let testPart2B =
    testPart2
    |> Seq.map (fun a -> fst (a))
    |> Seq.toList

let testPart2I =
    testPart2
    |> Seq.map (fun a -> snd (a))
    |> Seq.toList

let part2 =
    snd(input).Split(",")
    |> Seq.mapi (fun i x -> (x, (if x <> "x" then (x |> int) - i else i)))
    |> Seq.filter (fun a -> fst (a) <> "x")
    |> Seq.map (fun a -> (fst (a) |> int64), snd (a) |> int64)

let part2B =
    part2 |> Seq.map (fun a -> fst (a)) |> Seq.toList

let part2I =
    part2 |> Seq.map (fun a -> snd (a)) |> Seq.toList

let testResultP2 = CRT testPart2B testPart2I
printfn "Testresult part 2 %A" testResultP2
let resultP2 = CRT part2B part2I
printfn "Result part 2 %A" resultP2

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

// let rec checkBusses busses (startTime: int64) stillValid =
//     if not stillValid then
//         false
//     else
//         match busses with
//         | [] -> stillValid
//         | h :: t ->
//             match h with
//             | "x" ->
//                 let newStartTime = (startTime + (1 |> int64))
//                 checkBusses t newStartTime stillValid
//             | _ ->
//                 let newStartTime = (startTime + (1 |> int64))
//                 checkBusses t newStartTime (startTime % (h |> int64) = (0 |> int64))

let matchBusses (b: List<string>) (s: int64): bool =
    let departures =
        b
        |> List.indexed
        |> List.map (fun (i, x) -> s + (i |> int64))

    let valid =
        b
        |> List.indexed
        |> List.map (fun (i, x) ->
            if x = "x"
            then true
            else ((departures.[i] |> int64) % (x |> int64) = (0 |> int64)))

    valid |> List.contains false |> not

let rec validStart b (s: int64) d =
    if (s % (100000000 |> int64) = (0 |> int64))
       && (s > (0 |> int64)) then
        printfn "%A %A" s (DateTime.Now.ToLongTimeString())
    match d with
    | true -> s
    | _ ->
        let fValid =
            s % (List.head b |> int64) = (0 |> int64)

        if fValid then
            match matchBusses b s with
            | true -> s
            | _ -> validStart b (s + (1 |> int64)) false
        else
            validStart b (s + (1 |> int64)) false

let testBusses =
    ("1789,37,47,1889").Split(',') |> Seq.toList

let testPart2_1 = validStart testBusses (1 |> int64) false

printfn "First testresult %A" testPart2_1
// let busses =
//     ("13,x,x,41,x,x,x,x,x,x,x,x,x,467,x,x,x,x,x,x,x,x,x,x,x,19,x,x,x,x,17,x,x,x,x,x,x,x,x,x,x,x,29,x,353,x,x,x,x,x,37,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,23")
//         .Split(',')
//     |> Seq.toList
// let part2 =
//     validStart busses ("100000000000000" |> int64) false
// printfn "Resutl part 2 %A" part2

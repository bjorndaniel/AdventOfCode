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

let testResult =
    (nextValidBus (fst (testTimetable)) (snd (testTimetable)) (0, Int32.MaxValue))

printfn "Testresult %A" ((fst (testResult) * (snd (testResult))))

let result =
    (nextValidBus (fst (timeTable)) (snd (timeTable)) (0, Int32.MaxValue))

printfn "Result %A" ((fst (result) * (snd (result))))

#r "../packages/fsharp.data/3.3.2/lib/net45/Fsharp.Data.dll"
open System
open FSharp.Data

type InputData = JsonProvider<""" {"program": [1,3,123] } """>
let day5Input = InputData.Load("day5.json")

let replaceAt (i: int) (v: int) (l: list<int>): list<int> =
    if i = 0 then
        v :: List.tail l
    else
        let s, e = List.splitAt i l
        let newS = v :: List.tail e
        List.append s newS
//from https://stackoverflow.com/questions/42820232/f-convert-a-char-to-int
let inline charToInt c = int c - int '0'

let getOpCode input =
    let inputString =
        match input with
        | input when input > 9999 -> string input
        | input when input > 999 -> "0" + string input
        | input when input > 99 -> "00" + string input
        | _ -> string input

    let thirdMode = inputString.[0] |> charToInt
    let opCode = int inputString.[3..]
    let firstMode = inputString.[2] |> charToInt
    let secondMode = inputString.[1] |> charToInt
    [ thirdMode
      secondMode
      firstMode
      opCode ]


let rec execute (p: list<int>) (i: int): list<int> =
    let instruction = List.item i p

    let opCodeList =
        if instruction > 10 && instruction <> 99 then getOpCode instruction else [ 0; 0; 0; instruction ]

    let opCode = List.item 3 opCodeList
    let itemsLeft = List.skip i p
    let count = List.length itemsLeft
    match opCode with
    | 3 ->
        Console.WriteLine("Input desired value and press enter:")
        let input = Console.ReadLine()

        let pModified =
            replaceAt (List.item (i + 1) p) (int input) p

        execute pModified (i + 2)
    | 4 ->
        let output = List.item (List.item (i + 1) p) p
        Console.WriteLine(sprintf "Output value %A" output)
        execute p (i + 2)
    | 5 ->
        let f = List.skip i p |> List.truncate 3

        let operand1 =
            if (List.item 2 opCodeList) = 0 then List.item (List.item 1 f) p else List.item 1 f

        let operand2 =
            if (List.item 1 opCodeList) = 0 then List.item (List.item 2 f) p else List.item 2 f

        match operand1 with
        | 0 -> execute p (i + 3)
        | _ -> execute p operand2
    | 6 ->
        let f = List.skip i p |> List.truncate 3

        let operand1 =
            if (List.item 2 opCodeList) = 0 then List.item (List.item 1 f) p else List.item 1 f

        let operand2 =
            if (List.item 1 opCodeList) = 0 then List.item (List.item 2 f) p else List.item 2 f

        match operand1 with
        | 0 -> execute p operand2
        | _ -> execute p (i + 3)
    | 7 ->
        let f = List.skip i p |> List.truncate 4

        let operand1 =
            if (List.item 2 opCodeList) = 0 then List.item (List.item 1 f) p else List.item 1 f

        let operand2 =
            if (List.item 1 opCodeList) = 0 then List.item (List.item 2 f) p else List.item 2 f

        let position =
            if (List.item 0 opCodeList) = 0 then List.item 3 f else List.item (List.item 3 f) p

        let value = if operand1 < operand2 then 1 else 0
        let pModified = replaceAt position value p
        execute pModified (i + 4)
    | 8 ->
        let f = List.skip i p |> List.truncate 4

        let operand1 =
            if (List.item 2 opCodeList) = 0 then List.item (List.item 1 f) p else List.item 1 f

        let operand2 =
            if (List.item 1 opCodeList) = 0 then List.item (List.item 2 f) p else List.item 2 f

        let position =
            if (List.item 0 opCodeList) = 0 then List.item 3 f else List.item (List.item 3 f) p

        let value = if operand1 = operand2 then 1 else 0
        let pModified = replaceAt position value p
        execute pModified (i + 4)
    | 99 ->
        Console.WriteLine("Received end opcode")
        p
    | _ when count < 4 -> p
    | _ ->
        let f = List.skip i p |> List.truncate 4

        let operand1 =
            if (List.item 2 opCodeList) = 0 then List.item (List.item 1 f) p else List.item 1 f

        let operand2 =
            if (List.item 1 opCodeList) = 0 then List.item (List.item 2 f) p else List.item 2 f

        let position =
            if (List.item 0 opCodeList) = 0 then List.item 3 f else List.item (List.item 3 f) p

        let nextPosition = i + 4
        if opCode = 1 then
            let pModified =
                replaceAt position (operand1 + operand2) p

            execute pModified (i + 4)
        else
            let pModified =
                replaceAt position (operand1 * operand2) p

            execute pModified (nextPosition)

let result =
    execute (Array.toList day5Input.Program) 0

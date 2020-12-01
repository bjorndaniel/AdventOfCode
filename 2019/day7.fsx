#r @"../packages/fsharp.data/3.3.2/lib/net45/Fsharp.Data.dll"
open System
open FSharp.Data
type InputData = JsonProvider<""" {"program": [1,3,123] } """>

let replaceAt i v l =
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
    [thirdMode;secondMode;firstMode;opCode]

let rec execute (programstate:list<int>) inputs inputcount output autorun (p: list<int>) (i: int) : list<int> =
    let instruction = List.item i p
    let opCodeList = if instruction > 10 && instruction <> 99 then getOpCode instruction else [0;0;0;instruction]
    let opCode = List.item 3 opCodeList
    let itemsLeft = List.skip i p
    let count = List.length itemsLeft
    match opCode with  
    | 3 -> 
        match inputcount with
        | 0 -> 
            let input = List.item inputcount inputs
            let newInputCount = inputcount + 1
            let pModified = replaceAt (List.item (i+1) p) input p
            execute pModified inputs newInputCount 0 autorun pModified (i+2)
        | 1 ->         
            let input = List.item inputcount inputs
            let newInputCount = inputcount + 1
            let pModified = replaceAt (List.item (i+1) p) input p
            execute pModified inputs newInputCount 0 autorun pModified (i+2)
        | _ ->
            Console.WriteLine("Input desired value and press enter:");
            let input = Console.ReadLine();
            let pModified = replaceAt (List.item (i+1) p) (int input) p
            execute pModified inputs inputcount output autorun pModified (i+2)
    | 4 -> 
        if autorun then
            let output = List.item (List.item (i+1) p) p 
            // printfn("Received output code %A") output
            List.append [output;0] programstate
        else
            let output =  List.item (List.item (i+1) p) p 
            Console.WriteLine(sprintf "Output value %A" output)
            execute p inputs inputcount 0 autorun p (i+2)
    | 5 -> 
        let f = List.skip i p |> List.truncate 3
        let operand1 = if (List.item 2 opCodeList) = 0 then List.item (List.item 1 f) p else List.item 1 f
        let operand2 = if (List.item 1 opCodeList) = 0 then List.item (List.item 2 f) p else List.item 2 f
        match operand1 with
        | 0 -> execute p inputs inputcount 0 autorun p (i+3)
        | _ -> execute p inputs inputcount 0 autorun p operand2
    | 6 -> 
        let f = List.skip i p |> List.truncate 3
        let operand1 = if (List.item 2 opCodeList) = 0 then List.item (List.item 1 f) p else List.item 1 f
        let operand2 = if (List.item 1 opCodeList) = 0 then List.item (List.item 2 f) p else List.item 2 f
        match operand1 with
        | 0 -> execute p inputs inputcount 0 autorun p operand2
        | _ -> execute p inputs inputcount 0 autorun p (i+3)
    | 7 -> 
        let f = List.skip i p |> List.truncate 4
        let operand1 = if (List.item 2 opCodeList) = 0 then List.item (List.item 1 f) p else List.item 1 f
        let operand2 = if (List.item 1 opCodeList) = 0 then List.item (List.item 2 f) p else List.item 2 f
        let position = if (List.item 0 opCodeList) = 0 then List.item 3 f else List.item (List.item 3 f) p
        let value = if operand1 < operand2 then 1 else 0
        let pModified = replaceAt position value p
        execute pModified inputs inputcount 0 autorun pModified (i + 4)
    | 8 -> 
        let f = List.skip i p |> List.truncate 4
        let operand1 = if (List.item 2 opCodeList) = 0 then List.item (List.item 1 f) p else List.item 1 f
        let operand2 = if (List.item 1 opCodeList) = 0 then List.item (List.item 2 f) p else List.item 2 f
        let position = if (List.item 0 opCodeList) = 0 then List.item 3 f else List.item (List.item 3 f) p
        let value = if operand1 = operand2 then 1 else 0
        let pModified = replaceAt position value p
        execute pModified inputs inputcount 0 autorun pModified (i + 4)
    | 99 -> 
        if autorun then 
            let opcode = [output;99]
            List.append opcode programstate
        else
            Console.WriteLine("Received end opcode")
            List.append [0;99] programstate
    | _ when count < 4 -> List.append [0;0] programstate
    | _ ->
        let f = List.skip i p |> List.truncate 4
        let operand1 = if (List.item 2 opCodeList) = 0 then List.item (List.item 1 f) p else List.item 1 f
        let operand2 = if (List.item 1 opCodeList) = 0 then List.item (List.item 2 f) p else List.item 2 f
        let position = if (List.item 0 opCodeList) = 0 then List.item 3 f else List.item (List.item 3 f) p
        let nextPosition = i + 4
        if opCode = 1 then
            let pModified = replaceAt position (operand1 + operand2) p
            execute pModified inputs inputcount 0 autorun pModified (i + 4)
        else
            let pModified = replaceAt position (operand1 * operand2) p
            execute pModified inputs inputcount 0 autorun pModified (nextPosition)

let rec phaser inputs output program =
    match inputs with 
    | [] -> output
    | _ -> 
        let newInputs = List.tail inputs
        let inputValue = List.head inputs
        let newOutput = execute program [inputValue;List.head output] 0 0 true program 0
        phaser newInputs newOutput program

let rec maximize maxValue combinations program =
    printfn("inputmaximize %A") maxValue
    match combinations with
    | [] -> maxValue
    | _ -> 
        let remainingCombinations = List.tail combinations
        let phaserOutput = phaser (List.head combinations) [0] program
        if (List.head phaserOutput) > (fst maxValue) then
            let newMax = (List.head phaserOutput, List.item 1 phaserOutput)
            maximize newMax remainingCombinations  program
        else
            let newMax = (fst maxValue, List.item 1 phaserOutput) 
            maximize newMax remainingCombinations program

//from https://stackoverflow.com/questions/286427/calculating-permutations-in-f    
let rec permutations list taken = 
  seq { if Set.count taken = List.length list then yield [] else
        for l in list do
          if not (Set.contains l taken) then 
            for perm in permutations list (Set.add l taken)  do
              yield l::perm }

let combinations = permutations [0;1;2;3;4] Set.empty

let testprogram1 = InputData.Load("day7.json")

// let x = maximize (0,0) (Seq.toList combinations) (Array.toList testprogram1.Program)
// printfn("%A") x

//Part 2
 
let feedbackLoop output combinations program  = 
    let mutable continueLoop = true
    let mutable value = 0
    let mutable opcode = 0
    while continueLoop do
        let round = maximize (value, opcode) combinations program
        printfn("%A") round
        value <- fst round
        opcode <- snd round
        continueLoop <- opcode <> 99
    value

let combinations2 = permutations [5;6;7;8;9] Set.empty
let testprogram2 = InputData.Load("day7part2test1.json")
let output = feedbackLoop 0 (Seq.toList combinations2) (Array.toList testprogram2.Program)
// let output = maximize (0,0) [[9;8;7;6;5]] (Array.toList testprogram2.Program)
printfn("%A") output
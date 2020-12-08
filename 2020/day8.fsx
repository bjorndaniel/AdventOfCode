open System
open System.Linq
open System.Text.RegularExpressions

let readLines filePath = System.IO.File.ReadAllText(filePath)

let testLines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day8-test.txt"


let lines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day8.txt"

let testProgram =
    testLines.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.toList

let program =
    lines.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.toList

let createRegister (l: List<string>) =
    l
    |> Seq.map (fun a -> (a.Split(" ").[0], a.Split(" ").[1], 0))
    |> Seq.toList

let getAcc (n: string) a =
    let sign = n.[0]
    let steps = n.[1..] |> int
    match sign with
    | '+' -> a + steps
    | _ -> a - steps

let rec execute (prog: List<(string * string * int)>) (point: int) (acc: int) =
    if point > (List.length prog - 1) then
        (acc, true)
    else
        let instr, jmp, runs = prog.[point]
        if runs = 1 then
            (acc, false)
        else
            let newInst = (instr, jmp, (runs + 1))

            let newProg =
                prog
                |> Seq.mapi (fun i el -> if i = point then newInst else el)
                |> Seq.toList

            match instr with
            | "nop" -> execute newProg (point + 1) acc
            | "acc" -> execute newProg (point + 1) (getAcc jmp acc)
            | "jmp" -> execute newProg (getAcc jmp point) acc
            | _ -> (acc, false)

let rec findNextJMP p i =
    match i with
    | x when x > List.length p -> i
    | _ ->
        let instr, _, _ = p.[i]
        if instr = "jmp" then i else findNextJMP p (i + 1)

let rec findNextNOP p i =
    match i with
    | x when x > List.length p -> i
    | _ ->
        let instr, _, _ = p.[i]
        if instr = "nop" then i else findNextJMP p (i + 1)

let switchInstrJMP prog indx =
    let next = findNextJMP prog indx
    let _, jmp, rns = prog.[next]
    let newInstr = ("nop", jmp, rns)

    let newProg =
        prog
        |> Seq.mapi (fun i el -> if i = next then newInstr else el)
        |> Seq.toList

    newProg

let switchInstrNOP prog indx =
    let next = findNextNOP prog indx
    let _, jmp, rns = prog.[next]
    let newInstr = ("jmp", jmp, rns)

    let newProg =
        prog
        |> Seq.mapi (fun i el -> if i = next then newInstr else el)
        |> Seq.toList

    newProg

let rec findError test orig indx j =
    let res = execute test 0 0
    if snd (res) then
        fst (res)
    else if j then
        let nextIndex = indx + 1
        if nextIndex > List.length test then
            0
        else
            let nextTest = switchInstrNOP orig nextIndex
            findError nextTest orig nextIndex true
    else
        let nextIndex = indx + 1
        if nextIndex > List.length test then
            findError orig orig 0 true
        else
            let nextTest = switchInstrJMP orig nextIndex
            findError nextTest orig nextIndex false

let testRegister = createRegister testProgram
let register = createRegister program
let testResult = execute testRegister 0 0

printfn "Test part1: %A" testResult
let result = execute register 0 0
printfn "Result part1: %A" result


let testResult2 =
    findError testRegister testRegister 0 false

printfn "Test part2: %A" testResult2

let result2 = findError register register 0 false

printfn "Result part2: %A" result2

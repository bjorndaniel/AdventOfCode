open System

let readLines filePath = System.IO.File.ReadAllText(filePath)

let testLines =
    readLines @"C:\Projects\bjorndaniel\adventofcode\2020\day14-test.txt"

let testProgram =
    testLines.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.toList

let lines =
    readLines @"C:\Projects\bjorndaniel\adventofcode\2020\day14.txt"

let program =
    lines.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.toList

let maskValue (value: string) (mask: string) =
    // printfn "To mask %A" value
    // printfn "Mask %A" mask

    let mutable newValue = ""
    for i = 0 to (mask.Length - 1) do
        if mask.[i] <> 'X'
        then newValue <- newValue + ((mask.[i] |> string))
        else newValue <- newValue + ((value.[i] |> string))

    // printfn "%A" newValue
    newValue

let rec execute (prog: List<string>) mask reg =
    match prog with
    | [] -> List.fold (fun acc a -> acc + snd (a)) (0 |> int64) reg
    | h :: t ->
        if h.IndexOf "mask" > -1 then
            let newMask = h.[7..]
            execute t newMask reg
        else
            let memPos =
                h.[(h.IndexOf "[") + 1..(h.IndexOf "]") - 1]
                |> int

            let value = h.[((h.IndexOf "= ") + 2)..] |> int

            match reg |> Seq.tryFind (fun a -> fst (a) = memPos) with
            | None ->
                let unmasked = Convert.ToString(value, 2)

                let masked =
                    maskValue (unmasked.PadLeft(mask.Length, '0')) mask

                let newValue = Convert.ToInt64(masked, 2)
                execute t mask ((memPos, newValue) :: reg)
            | Some m ->
                let unmasked = Convert.ToString(value, 2)

                let masked =
                    maskValue (unmasked.PadLeft(mask.Length, '0')) mask

                let newValue = Convert.ToInt64(masked, 2)

                let newReg =
                    reg
                    |> Seq.map (fun a -> if fst (a) = fst (m) then (fst (m), newValue) else a)
                    |> Seq.toList

                execute t mask newReg

let testResult = execute testProgram "" []
printfn "%A" testResult

let result = execute program "" []
printfn "Result %A" result

//Part 2
let testLinesP2 =
    readLines @"C:\Projects\bjorndaniel\AdventOfCode\2020\day14-testP2.txt"


let testProgramP2 =
    testLinesP2.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.toList

let maskValue2 (value: string) (mask: string) =
    let mutable newValue = ""
    for i = 0 to (mask.Length - 1) do
        if mask.[i] = '0'
        then newValue <- newValue + ((value.[i] |> string))
        else newValue <- newValue + ((mask.[i] |> string))

    newValue

//Adapted from https://www.geeksforgeeks.org/generate-all-binary-strings-from-given-pattern/
let combos s =
    let mutable list = []

    let rec parse (s: string) =
        let index = s.IndexOf('X')

        match index with
        | -1 -> list <- s :: list
        | _ ->
            let newS =
                s
                |> Seq.mapi (fun i a -> if i = index then '0' else a)
                |> String.Concat

            let newS1 =
                s
                |> Seq.mapi (fun i a -> if i = index then '1' else a)
                |> String.Concat

            parse newS
            parse newS1

    parse s
    list |> Seq.map (fun a -> Convert.ToInt64(a, 2))


let rec execute2 (prog: string list) (mask: string) reg =
    match prog with
    | [] -> List.fold (fun acc a -> acc + snd (a)) (0 |> int64) reg
    | h :: t ->
        if h.IndexOf "mask" > -1 then
            let newMask = h.[7..]
            execute2 t newMask reg
        else
            let memPos =
                h.[(h.IndexOf "[") + 1..(h.IndexOf "]") - 1]
                |> int64

            let value = h.[((h.IndexOf "= ") + 2)..] |> int64
            let unmasked = Convert.ToString(memPos, 2)

            let masked =
                maskValue2 (unmasked.PadLeft(mask.Length, '0')) mask

            let low =
                Convert.ToInt64(
                    (masked
                     |> Seq.map (fun a -> if a = 'X' then '0' else a)
                     |> String.Concat),
                    2
                )

            let high =
                Convert.ToInt64(
                    (masked
                     |> Seq.map (fun a -> if a = 'X' then '1' else a)
                     |> String.Concat),
                    2
                )

            let combos = combos masked
            let vals = combos |> Seq.map (fun a -> (a, value))
            let mutable newReg = reg

            for c in vals do
                let x =
                    newReg |> Seq.tryFind (fun a -> fst (a) = fst (c))

                match x with
                | None -> newReg <- c :: newReg
                | Some a ->
                    newReg <-
                        newReg
                        |> List.map (fun a -> if fst (a) = fst (c) then c else a)

            execute2 t mask newReg

let testresultP2 = execute2 testProgramP2 "" []
printfn "Testresult P2 %A" testresultP2

let resultP2 = execute2 program "" []
printfn "Result P2 %A" resultP2

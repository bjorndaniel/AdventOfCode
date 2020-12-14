open System

let readLines filePath = System.IO.File.ReadAllText(filePath)

let testLines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day14-test.txt"

let testProgram =
    testLines.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.toList

let lines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day14.txt"

let program =
    lines.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.toList

let maskValue (value: string) (mask: string) =
    printfn "To mask %A" value
    printfn "Mask %A" mask
    let mutable newValue = ""
    for i = 0 to (mask.Length - 1) do
        if mask.[i] <> 'X'
        then newValue <- newValue + ((mask.[i] |> string))
        else newValue <- newValue + ((value.[i] |> string))
    printfn "%A" newValue
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
printfn "%A" result

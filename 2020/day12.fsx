open System

let readLines filePath = System.IO.File.ReadAllText(filePath)

let testLines =
    readLines @"c:\Projects\bjorndaniel\adventofcode\2020\day12-test.txt"

let testInstructions =
    testLines.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.map (fun a -> (a.[0], a.[1..] |> int))
    |> Seq.toList

let lines =
    readLines @"c:\Projects\bjorndaniel\adventofcode\2020\day12.txt"

let instructions =
    lines.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.map (fun a -> (a.[0], a.[1..] |> int))
    |> Seq.toList

type Ship =
    { Direction: char
      North: int
      East: int
      South: int
      West: int }
    member this.Manhattan =
        abs ((abs (this.North)) - (abs (this.South)))
        + (abs ((abs this.West) - (abs this.East)))


let testShip =
    { Direction = 'E'
      North = 0
      East = 0
      South = 0
      West = 0 }

let getDirection dir degr inDir =
    match degr with
    | 90 ->
        match inDir with
        | 'N' -> if dir = 'L' then 'W' else 'E'
        | 'S' -> if dir = 'L' then 'E' else 'W'
        | 'E' -> if dir = 'L' then 'N' else 'S'
        | 'W' -> if dir = 'L' then 'S' else 'N'
        | _ -> inDir
    | 180 ->
        match inDir with
        | 'N' -> 'S'
        | 'S' -> 'N'
        | 'E' -> 'W'
        | 'W' -> 'E'
        | _ -> inDir
    | 270 ->
        match inDir with
        | 'N' -> if dir = 'L' then 'E' else 'W'
        | 'S' -> if dir = 'L' then 'W' else 'E'
        | 'E' -> if dir = 'L' then 'S' else 'N'
        | 'W' -> if dir = 'L' then 'N' else 'S'
        | _ -> inDir
    | _ -> inDir

let rec execute (instr: List<(char * int)>) (ship: Ship) =
    match instr with
    | [] -> ship
    | h :: t ->
        match fst (h) with
        | 'N' ->
            execute
                t
                { ship with
                      North = (ship.North + snd (h) - ship.South)
                      South = 0 }
        | 'S' ->
            execute
                t
                { ship with
                      South = (ship.South + snd (h) - ship.North)
                      North = 0 }
        | 'E' ->
            execute
                t
                { ship with
                      East = (ship.East + snd (h) - ship.West)
                      West = 0 }
        | 'W' ->
            execute
                t
                { ship with
                      West = (ship.West + snd (h) - ship.East)
                      East = 0 }
        | 'L' ->
            execute
                t
                { ship with
                      Direction = (getDirection 'L' (snd (h)) ship.Direction) }
        | 'R' ->
            execute
                t
                { ship with
                      Direction = (getDirection 'R' (snd (h)) ship.Direction) }
        | 'F' ->
            match ship.Direction with
            | 'N' ->
                execute
                    t
                    { ship with
                          North = (ship.North + snd (h) - ship.South)
                          South = 0 }
            | 'S' ->
                execute
                    t
                    { ship with
                          South = (ship.South + snd (h) - ship.North)
                          North = 0 }
            | 'E' ->
                execute
                    t
                    { ship with
                          East = (ship.East + snd (h) - ship.West)
                          West = 0 }
            | 'W' ->
                execute
                    t
                    { ship with
                          West = (ship.West + snd (h) - ship.East)
                          East = 0 }
            | _ -> execute t ship
        | _ -> execute t ship


let testResult = execute testInstructions testShip
printfn "Testresult part 1: %A" testResult.Manhattan
printfn "%A" testResult

let result = execute instructions testShip
printfn "%A" result
printfn "Result part 1: %A" result.Manhattan
10

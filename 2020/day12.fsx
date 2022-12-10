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

type Waypoint =
    { North: int
      East: int
      South: int
      West: int }

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

// let testResult = execute testInstructions testShip
// printfn "Testresult part 1: %A" testResult.Manhattan

// let result = execute instructions testShip
// printfn "Result part 1: %A" result.Manhattan
//Part 2


let rec execute2 (instr: List<(char * int)>) (sw: (Ship * Waypoint)) =
    // printfn "Ship %A" (fst (sw))
    // printfn "Waypoint %A" (snd (sw))
    match instr with
    | [] -> sw
    | h :: t ->
        match fst (h) with
        | 'N' ->
            let wayp = snd (sw)

            let wp =
                { wayp with
                      North = (wayp.North + snd (h) - wayp.South)
                      South = 0 }

            execute2 t (fst (sw), wp)
        | 'S' ->
            let wayp = snd (sw)

            let wp =
                { wayp with
                      South = (wayp.South + snd (h) - wayp.North)
                      North = 0 }

            execute2 t (fst (sw), wp)
        | 'E' ->
            let wayp = snd (sw)

            let wp =
                { wayp with
                      East = (wayp.East + snd (h) - wayp.West)
                      West = 0 }

            execute2 t (fst (sw), wp)
        | 'W' ->
            let wayp = snd (sw)

            let wp =
                { wayp with
                      West = (wayp.West + snd (h) - wayp.East)
                      East = 0 }

            execute2 t (fst (sw), wp)
        | 'F' ->
            let wayp = snd (sw)
            let sh = fst (sw)
            let newNorth = wayp.North * snd (h)
            let newSouth = wayp.South * snd (h)
            let newEast = wayp.East * snd (h)
            let newWest = wayp.West * snd (h)

            let newShip =
                { sh with
                      North = (newNorth + sh.North) - (newSouth + sh.South)
                      South = 0
                      East = (newEast + sh.East) - (newWest + sh.West)
                      West = 0 }

            execute2 t (newShip, snd (sw))
        | 'L'
        | 'R' ->
            let wayp = snd (sw)
            let indir1 = if wayp.North = 0 then 'S' else 'N'

            let inpoint1 =
                if wayp.North = 0 then wayp.South else wayp.North

            let indir2 = if wayp.East = 0 then 'W' else 'E'

            let inpoint2 =
                if wayp.East = 0 then wayp.West else wayp.East

            let dir1 = getDirection (fst (h)) (snd (h)) indir1
            let dir2 = getDirection (fst (h)) (snd (h)) indir2

            let wp =
                { wayp with
                      North = (if dir1 = 'N' then inpoint1 else (if dir2 = 'N' then inpoint2 else 0))
                      South = (if dir1 = 'S' then inpoint1 else (if dir2 = 'S' then inpoint2 else 0))
                      West = (if dir1 = 'W' then inpoint1 else (if dir2 = 'W' then inpoint2 else 0))
                      East = (if dir1 = 'E' then inpoint1 else (if dir2 = 'E' then inpoint2 else 0)) }

            execute2 t (fst (sw), wp)
        | _ -> execute2 t sw

let waypoint =
    { North = 1
      East = 10
      West = 0
      South = 0 }

let testResultp2 =
    execute2 testInstructions (testShip, waypoint)

printfn "%A" (fst (testResultp2))
printfn "TestResult part 2 %A" (fst(testResultp2).Manhattan)

let resultp2 =
    execute2 instructions (testShip, waypoint)

printfn "%A" (fst (resultp2))
printfn "Result part 2 %A" (fst(resultp2).Manhattan)

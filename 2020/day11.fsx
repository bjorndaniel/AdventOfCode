open System

let readLines filePath = System.IO.File.ReadAllText(filePath)

let testLines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day11-test.txt"

let lines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day11.txt"

let testSeats =
    testLines.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.map (fun a -> Seq.toArray a)
    |> Seq.toArray

let seats =
    lines.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.map (fun a -> Seq.toArray a)
    |> Seq.toArray

let testRows = Seq.length testSeats
let rows = Seq.length seats

let testColumns = Seq.length (Seq.head testSeats)
let columns = Seq.length (Seq.head seats)

let testGrid =
    Array2D.init testRows testColumns (fun i j -> testSeats.[i].[j])

let grid =
    Array2D.init rows columns (fun i j -> seats.[i].[j])

let findAdjacent (grid: char [,]) row col =
    match row with
    | 0 ->
        match col with
        | 0 ->
            [ grid.[0, 1]
              grid.[1, 0]
              grid.[1, 1] ]
        | x when (col + 1) = (Array2D.length2 grid) ->
            [ grid.[0, col - 1]
              grid.[1, col - 1]
              grid.[1, col] ]
        | _ ->
            [ grid.[0, col - 1]
              grid.[0, col + 1]
              grid.[1, col - 1]
              grid.[1, col]
              grid.[1, col + 1] ]
    | x when (row + 1) = (Array2D.length1 grid) ->
        match col with
        | 0 ->
            [ grid.[row - 1, 0]
              grid.[row - 1, 1]
              grid.[row, 1] ]
        | x when (col + 1) = (Array2D.length2 grid) ->
            [ grid.[row - 1, col - 1]
              grid.[row - 1, col]
              grid.[row, col - 1] ]
        | _ ->
            [ grid.[row - 1, col - 1]
              grid.[row - 1, col]
              grid.[row - 1, col + 1]
              grid.[row, col - 1]
              grid.[row, col + 1] ]
    | _ ->
        match col with
        | 0 ->
            [ grid.[row - 1, 0]
              grid.[row - 1, 1]
              grid.[row, 1]
              grid.[row + 1, 0]
              grid.[row + 1, 1] ]
        | x when (col + 1) = (Array2D.length2 grid) ->
            [ grid.[row - 1, col - 1]
              grid.[row - 1, col]
              grid.[row, col - 1]
              grid.[row + 1, col - 1]
              grid.[row + 1, col] ]
        | _ ->
            [ grid.[row, col - 1]
              grid.[row, col + 1]
              grid.[row - 1, col - 1]
              grid.[row - 1, col]
              grid.[row - 1, col + 1]
              grid.[row + 1, col - 1]
              grid.[row + 1, col + 1]
              grid.[row + 1, col] ]

let rec matchSeat l =
    match l with
    | [] -> ' '
    | h :: t -> if h = 'L' || h = '#' then h else matchSeat t

let siteRow (grid: char [,]) row col =
    match col with
    | 0 ->
        matchSeat (grid.[row, col + 1..] |> Seq.toList)
        :: []
    | x when (x + 1) = Array2D.length2 grid ->
        matchSeat (grid.[row, ..col - 1] |> Seq.rev |> Seq.toList)
        :: []
    | _ ->
        [ (matchSeat (grid.[row, ..col - 1] |> Seq.rev |> Seq.toList))
          (matchSeat (grid.[row, col + 1..] |> Seq.toList)) ]

let siteCol (grid: char [,]) row col =
    match row with
    | 0 ->
        matchSeat (grid.[row + 1.., col] |> Seq.toList)
        :: []
    | x when (x + 1) = Array2D.length1 grid ->
        matchSeat (grid.[..row - 1, col] |> Seq.rev |> Seq.toList)
        :: []
    | _ ->
        [ (matchSeat (grid.[..row - 1, col] |> Seq.rev |> Seq.toList))
          (matchSeat (grid.[row + 1.., col] |> Seq.toList)) ]

let diagonals (grid: char [,]) row col =
    let mutable upLeft = []
    let mutable upRight = []
    let mutable downLeft = []
    let mutable downRight = []
    let rows = Array2D.length1 grid
    let cols = Array2D.length2 grid
    for gr = 0 to (rows - 1) do
        for gc = 0 to (cols - 1) do
            // if gr <> row && gc <> col then
            if (abs (gc - col)) = (abs (gr - row)) then
                if gc < col && gr < row then upLeft <- (grid.[gr, gc]) :: upLeft
                else if gc > col && gr < row then upRight <- grid.[gr, gc] :: upRight
                else if gc < col && gr > row then downLeft <- grid.[gr, gc] :: downLeft
                else if gc > col && gr > row then downRight <- grid.[gr, gc] :: downRight

    downLeft <- downLeft |> Seq.rev |> Seq.toList
    downRight <- downRight |> Seq.rev |> Seq.toList
    [ upLeft; upRight; downLeft; downRight ]

let findAdjacentP2 (grid: char [,]) row col =
    let mutable adjacent = []
    adjacent <- (siteRow grid row col) @ adjacent //row
    adjacent <- (siteCol grid row col) @ adjacent //col
    let diags = diagonals grid row col
    for d in diags do
        adjacent <- (matchSeat d) :: adjacent
    adjacent
//From: https://gist.github.com/kristopherjohnson/0d8f9d29f0894bbb51af
/// Count number of elements in array that satisfy filter
let countFilteredBy (f: ('a -> bool)) (array: 'a []): int =
    Array.fold (fun acc item -> if f item then acc + 1 else acc) 0 array

let checkSeat row col (grid: char [,]) p2 =
    let seat = grid.[row, col]

    let adjacent =
        if p2
        then (findAdjacentP2 grid row col) |> Seq.toArray
        else (findAdjacent grid row col) |> Seq.toArray

    match seat with
    | 'L' ->
        let countOccupied =
            countFilteredBy (fun s -> s = '#') adjacent

        if countOccupied = 0 then (true, '#') else (false, 'L')
    | '#' ->
        let countOccupied =
            countFilteredBy (fun s -> s = '#') adjacent

        if p2
        then (if countOccupied >= 5 then (true, 'L') else (false, '#'))
        else (if countOccupied >= 4 then (true, 'L') else (false, '#'))

    | _ -> (false, seat)

let rec seating (grid: char [,]) (c: bool) (p2: bool): char [,] =
    if not c then
        grid
    else
        let mutable changed = false
        let rows = Array2D.length1 grid
        let cols = Array2D.length2 grid
        let mutable newGrid = Array2D.init rows cols (fun x y -> '.')
        for i = 0 to (rows - 1) do
            for j = 0 to (cols - 1) do
                let colChange = checkSeat i j grid p2
                if fst (colChange) then changed <- true
                newGrid.[i, j] <- snd (colChange)

        if changed then seating newGrid true p2 else seating newGrid false p2

// let testResult =
//     countFilteredBy (fun x -> x = '#')
//         ((seating testGrid true false)
//          |> Seq.cast<Char>
//          |> Seq.toArray)

// printfn "Testresult part 1: %A" testResult

// let result =
//     countFilteredBy (fun x -> x = '#')
//         ((seating grid true false)
//          |> Seq.cast<Char>
//          |> Seq.toArray)

// printfn "Result part 1: %A" result
//Part 2
let testResultP2 =
    countFilteredBy (fun x -> x = '#')
        ((seating testGrid true true)
         |> Seq.cast<Char>
         |> Seq.toArray)

printfn "Testresult part 2: %A" testResultP2
let resultP2 =
    countFilteredBy (fun x -> x = '#')
        ((seating grid true true)
         |> Seq.cast<Char>
         |> Seq.toArray)
printfn "Result part 2: %A" resultP2

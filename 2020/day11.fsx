open System

let readLines filePath = System.IO.File.ReadAllText(filePath)

let testLines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day11-test.txt"

let testSeats =
    testLines.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.map (fun a -> Seq.toArray a)
    |> Seq.toArray


let testRows = Seq.length testSeats

let testColumns = Seq.length (Seq.head testSeats)

let testGrid =
    Array2D.init testRows testColumns (fun i j -> testSeats.[i].[j])

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
        | x when (col + 1) <= (Array2D.length2 grid) ->
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

//From: https://gist.github.com/kristopherjohnson/0d8f9d29f0894bbb51af
/// Count number of elements in array that satisfy filter
let countFilteredBy (f: ('a -> bool)) (array: 'a []): int =
    Array.fold (fun acc item -> if f item then acc + 1 else acc) 0 array

let checkSeat row col (grid: char [,]) =
    let seat = grid.[row, col]

    let adjacent =
        (findAdjacent grid row col) |> Seq.toArray

    // printfn "%A %A %A" adjacent row col
    match seat with
    | 'L' ->
        let countOccupied =
            countFilteredBy (fun s -> s = '#') adjacent

        if countOccupied = 0 then (true, '#') else (false, 'L')
    | '#' ->
        let countOccupied =
            countFilteredBy (fun s -> s = '#') adjacent

        // printfn "%A" countOccupied
        if countOccupied > 4 then (true, 'L') else (false, '#')
    | _ -> (false, seat)


let rec seating (grid: char [,]) (c: bool): char [,] =
    if not c then
        grid
    else
        let mutable changed = false

        let newGrid =
            Array2D.init (Array2D.length1 grid) (Array2D.length2 grid) (fun r c ->
                let change = checkSeat r c grid
                // printfn "%A" change
                if fst (change) then changed <- true
                snd (change))

        printfn "%A %A" newGrid changed
        if changed then seating newGrid true else seating newGrid false
// match changed with
// | true -> seating newGrid true
// | false -> seating newGrid false
// printfn "%A" testGrid

let grid = (seating testGrid true)

printfn "%A" grid

let result =
    countFilteredBy (fun x -> x = '#')
        ((seating testGrid true)
         |> Seq.cast<Char>
         |> Seq.toArray)

printfn "%A" result
//          |> Seq.cast<Char>
//          |> Seq.toArray)
// printfn "%A" result

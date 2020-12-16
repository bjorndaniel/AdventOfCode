let testInput = [ 0; 3; 6 ]
let input = [ 6; 3; 15; 13; 1; 0 ]

let countFilteredBy (f: ('a -> bool)) (array: 'a []): int =
    Array.fold (fun acc item -> if f item then acc + 1 else acc) 0 array

let rec find l i n m =
    match l with
    | [] -> m
    | x when (Seq.length m) = 2 -> m
    | h :: t -> if h = n then find t (i - 1) n (i :: m) else find t (i - 1) n m

let rec play turn (numbers: List<int>) =
    if turn % 10000 = 0 then printfn "%A" turn
    match turn with
    | x when x < (Seq.length numbers) -> play (turn + 1) numbers
    | 30000000 -> numbers |> Seq.rev |> Seq.head
    | _ ->
        let mutable occurs = []
        let previous = numbers |> Seq.rev |> Seq.head
        // printfn "Previous %A" previous
        // printfn "%A" (numbers.[0..((Seq.length numbers) - 2)])
        if ((countFilteredBy (fun x -> x = previous) (numbers |> Seq.toArray)) > 1) then
            let toSearch =
                numbers.[0..((Seq.length numbers) - 2)]
                |> List.rev

            occurs <- find (toSearch) ((List.length toSearch) - 1) previous []
        //find numbers 0 (numbers |> Seq.rev |> Seq.head) []
        // printfn "%A" occurs
        match occurs with
        | [] -> play (turn + 1) (numbers @ [ 0 ])
        | _ ->
            let pr = occurs |> Seq.max
            let nr = turn - (pr + 1)
            play (turn + 1) (numbers @ [ (if nr = 0 then 1 else nr) ])

// printfn "%A" (play 1 testInput)
printfn "%A" (play 1 input)

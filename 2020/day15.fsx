let testInput = [ 0; 3; 6 ]
let input = [ 6; 3; 15; 13; 1; 0 ]

let rec find l i n m =
    match l with
    | [] -> m
    | h :: t -> if h = n then find t (i + 1) n (i :: m) else find t (i + 1) n m

let rec play turn (numbers: List<int>) =
    match turn with
    | x when x < (Seq.length numbers) -> play (turn + 1) numbers
    | 2020 ->
        // printfn "%A" numbers
        numbers |> Seq.rev |> Seq.head
    | _ ->
        let previous = numbers |> Seq.rev |> Seq.head
        // printfn "Previous %A" previous
        // printfn "%A" (numbers.[0..((Seq.length numbers) - 2)])

        let occurs =
            find (numbers.[0..((Seq.length numbers) - 2)]) 0 previous []
        //find numbers 0 (numbers |> Seq.rev |> Seq.head) []
        // printfn "%A" occurs
        match occurs with
        | [] -> play (turn + 1) (numbers @ [ 0 ])
        | _ ->
            let pr = occurs |> Seq.max
            let nr = turn - (pr + 1)
            play (turn + 1) (numbers @ [ (if nr = 0 then 1 else nr) ])

printfn "%A" (play 1 testInput)
printfn "%A" (play 1 input)

open System
let testInput = "1 + (2 * 3) + (4 * (5 + 6))"

let evaluate (exp: string) =
    let terms =
        exp.Split(" ", System.StringSplitOptions.RemoveEmptyEntries)

    let mutable sum = 0
    let mutable operator = ""

    for t in terms do
        match t with
        | x when (Seq.forall Char.IsDigit x) ->
            match operator with
            | "+" -> sum <- sum + (x |> int)
            | "*" -> sum <- sum * (x |> int)
            | _ -> sum <- (x |> int)
        | _ -> operator <- t

    sum


let rec group (x: char list) (g: string list) =
    match x with
    | [] ->
        g
        |> Seq.rev
        |> Seq.filter (fun x -> x <> "")
        |> Seq.toList
    | h :: t ->
        match h with
        | ' ' -> group t g
        // | x when Char.IsDigit x -> group t [ (x |> string) ] @ g
        | '(' -> group t ("" :: g)
        | ')' -> group t ("" :: g)
        | _ ->
            if Seq.isEmpty g then
                group t [ (h |> string) ]
            else
                let current = Seq.head g
                let theRest = Seq.tail g |> Seq.toList
                let newG = current + " " + (h |> string)
                group t (newG :: theRest)

let rec sumGroups g (s: int) (o: string) =
    // printfn "%A" g
    match g with
    | [] -> s
    | h :: t ->
        let mutable tSum = evaluate h

        let tops =
            h.Split(" ", StringSplitOptions.RemoveEmptyEntries)

        if tops.[0] = "+" then
            tSum <- tSum + s
        else if tops.[0] = "*" then
            tSum <- tSum * s
        else
            tSum <- s

        if h.EndsWith("*") then
            sumGroups t tSum "*"
        else if h.EndsWith("+") then
            sumGroups t tSum "+"
        else
            sumGroups t tSum ""

// printfn "%A %A" s tSum





let g =
    group
        ("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))"
         |> Seq.toList)
        []

printfn "%A" g

printfn "%A" (sumGroups g 0 "")
// printfn
//     "%A"
//     (group
//         ("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2"
//          |> Seq.toList)
//         [])
// printfn
//     "%A"
//     (group
//         ("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))"
//          |> Seq.toList)
//         [])
// printfn "%A" (evaluate " 4 * 5")
// printfn "%A" (evaluate "8 * 3 + 9 + 3 * 4 * 3")

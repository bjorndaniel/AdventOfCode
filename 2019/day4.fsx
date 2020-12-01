open System.Text.RegularExpressions

let values = seq { 272091 .. 815432 }
let hasDuplicates input = Regex.IsMatch(input, "(.)(\\1)+")
let hasDuplicatesPart2 input = Regex.IsMatch(input, "(?:^|(.)(?!\\1))(\\d)\\2(?!\\2)")
let rec increasing result prev (input: string) =
    let number = int input.[0]
    if input.Length = 1 then result && (prev <= number)
    else if prev > number then false
    else increasing (prev <= number) number input.[1..]

let ispassword input fn =
    let inputString = string input
    (fn inputString) && (increasing true 0 inputString)

let validPasswords = 
    Seq.choose(fun x ->
        match x with
        | x when (ispassword x hasDuplicates) -> Some(x)
        | _ -> None) values
printfn("Number of valid %A") (Seq.length validPasswords)


let validPasswordsPart2 = 
    Seq.choose(fun x ->
        match x with
        | x when (ispassword x hasDuplicatesPart2) -> Some(x)
        | _ -> None) values
printfn("Number of valid %A") (Seq.length validPasswordsPart2)
            

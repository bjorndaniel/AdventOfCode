#r "nuget: FSharp.Data"
open System
open System.Text.RegularExpressions
open FSharp.Data

type Policies =
    CsvProvider<"C:\\Projects\\bjorndaniel\\AdventOfCode\\2020\\day2-test.csv", Schema="Occurs (string),Char(string), Password(string)">

let testPolicies =
    Policies.Load("C:\\Projects\\bjorndaniel\\AdventOfCode\\2020\\day2-test.csv")

let policies =
    Policies.Load("C:\\Projects\\bjorndaniel\\AdventOfCode\\2020\\day2.csv")



let countMatches charToMatch (input: string) =
    Regex.Matches(input, Regex.Escape charToMatch).Count

let rec passwordChecker (l: List<Policies.Row>) c =
    match l with
    | [] -> c
    | h :: t ->
        let occurs = h.Occurs.Split '-'
        let low = occurs.[0] |> int
        let high = occurs.[1] |> int
        let count = countMatches h.Char h.Password
        if count >= low && count <= high then (passwordChecker t (c + 1)) else (passwordChecker t c)

//FROM: https://stackoverflow.com/questions/7656054/how-to-do-boolean-exclusive-or
let xor a b = (a || b) && not (a && b)

let rec passwordChecker2 (l: List<Policies.Row>) c =
    match l with
    | [] -> c
    | h :: t ->
        let occurs = h.Occurs.Split '-'
        let firstLetter = occurs.[0] |> int
        let secondLetter = occurs.[1] |> int

        let firstMatch =
            h.Password.[firstLetter - 1] = h.Char.[0]

        let secondMatch =
            h.Password.[secondLetter - 1] = h.Char.[0]

        if (xor firstMatch secondMatch) then (passwordChecker2 t (c + 1)) else (passwordChecker2 t c)

let result =
    passwordChecker (Seq.toList policies.Rows) 0

let resultPart2 =
    passwordChecker2 (Seq.toList policies.Rows) 0

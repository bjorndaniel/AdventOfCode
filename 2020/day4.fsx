open System
open System.Linq
open System.Text.RegularExpressions

let readLines filePath = System.IO.File.ReadAllText(filePath)

let testLines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day4-test.txt"

let lines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day4.txt"

let testLinesP2 =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day4-part2-test.txt"

let requiredFields =
    [ "byr"
      "iyr"
      "eyr"
      "hgt"
      "hcl"
      "ecl"
      "pid" ]

let validEyColours =
    [ "amb"
      "blu"
      "brn"
      "gry"
      "grn"
      "hzl"
      "oth" ]

let testPassports =
    testLines.Split([| "\r\n\r\n" |], StringSplitOptions.RemoveEmptyEntries)

let testPassports2 =
    testLinesP2.Split([| "\r\n\r\n" |], StringSplitOptions.RemoveEmptyEntries)

let passports =
    lines.Split([| "\r\n\r\n" |], StringSplitOptions.RemoveEmptyEntries)

let rec containsAll (l: List<string>) (s: string) (b: bool) =
    match l with
    | [] -> b
    | h :: t -> containsAll t s (b && (s.IndexOf(h) >= 0))

let rec checkValid l c: List<string> =
    match l with
    | [] -> c
    | h :: t ->
        let isValid = containsAll requiredFields h true
        if isValid then checkValid t (h :: c) else checkValid t c

let trimField (s: string) =
    if s.IndexOf(" ") > -1 then s.Substring(0, s.IndexOf(" "))
    else if s.IndexOf("\r\n") > -1 then s.Substring(0, s.IndexOf("\r\n"))
    else s

let isFieldValid (f: string) (v: string) =
    // printfn "Checking %A with %A" f v
    let mutable result = false
    match f with
    | "byr" ->
        let regex = "^(19[2-9][0-9])|200[0-2]$"
        result <- Regex.IsMatch(v, regex)
    | "iyr" ->
        let regex = "^(201[0-9])|2020$"
        result <- Regex.IsMatch(v, regex)
    | "eyr" ->
        let regex = "^(202[0-9])|2030$"
        result <- Regex.IsMatch(v, regex)
    | "hgt" ->
        let regexCm = "^(1[5-8][0-9]|19[0-3])cm$"
        let regexIn = "(59|6[0-9]|7[0-6])in"
        result <- (Regex.IsMatch(v, regexCm) || Regex.IsMatch(v, regexIn))
    | "hcl" ->
        let regex = "^#(?:[0-9a-fA-F]{6}){1}$"
        result <- Regex.IsMatch(v, regex)
    | "ecl" -> result <- validEyColours.Contains(v)
    | "pid" ->
        let regex = "^\d{9}$"
        result <- Regex.IsMatch(v, regex)
    | "cid" -> result <- true
    | _ -> result <- false
    // printfn "Result %A" result
    result

let validateFields (s: string) =
    let mutable res = true
    for f in requiredFields do
        let sval =
            s.Split([| f |], StringSplitOptions.RemoveEmptyEntries)

        if sval.Length = 1 then
            let value = sval.[0].Split ':'
            res <- res && (isFieldValid f (trimField value.[1]))
        else
            let value = sval.[1].Split ':'
            res <- res && (isFieldValid f (trimField value.[1]))
    res

let rec checkValidValues l c =
    match l with
    | [] -> c
    | h :: t -> if validateFields h then checkValidValues t (c + 1) else checkValidValues t c

let testResult = checkValid (Seq.toList testPassports) []
printfn "Part 1 testresult: %A" testResult.Length

let result = checkValid (Seq.toList passports) []

printfn "Part 1 result: %A" result.Length
//PART 2
let validFields =
    checkValid (Seq.toList testPassports2) []

printfn "Part 2 valid fields %A" validFields.Length

let validValues = checkValidValues validFields 0

printfn "Part 2 valid values %A" validValues

let validFields2 = checkValid (Seq.toList passports) []

printfn "Part 2 valid fields %A" validFields2.Length

let validValues2 = checkValidValues validFields2 0

printfn "Part 2 valid values %A" validValues2

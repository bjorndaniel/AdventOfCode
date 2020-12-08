open System
open System.Linq
open System.Text.RegularExpressions

let readLines filePath = System.IO.File.ReadAllText(filePath)

let testLines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day7-test.txt"


let lines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day7.txt"

let testRules =
    testLines.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.toList

let rules =
    lines.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.toList

let getRule (s: string) =
    let bags =
        s.Replace("bags", "").Replace("bag", "").Replace(".", "").Split("contain")

    let outer = bags.[0].[..bags.[0].Length - 3]

    let inner =
        bags.[1].Split(",")
        |> Seq.map (fun x -> x.Trim())
        |> Seq.toList

    (outer, inner)

let rec getRules (rules: List<string>) r =
    match rules with
    | [] -> r
    | h :: t ->
        let b = getRule h
        getRules t (b :: r)

let canContain (r: List<string>) (b: List<string>) =
    let mutable contains = false
    for bag in b do
        contains <- contains || r.Any(fun x -> x.Contains(bag))
    contains

let rec findBags (r: List<(string * List<string>)>) (o: List<(string * List<string>)>) b c =
    match r with
    | [] -> c
    | h :: t ->
        if canContain (snd (h)) b then
            let newB =
                if b.Contains(fst (h)) then b else ((fst (h) :: b))

            let mutable newRuleList = o.Where(fun a -> fst (a) <> fst (h))
            for bag in newB do
                newRuleList <- newRuleList.Where(fun a -> fst (a) <> bag)
            findBags (Seq.toList newRuleList) o newB (c + 1)
        else
            findBags t o b c

let bagRules = getRules rules []

let result =
    findBags bagRules bagRules [ "shiny gold" ] 0

printfn "%A" result
//Part 2

let rec findStartRule (r: List<(string * List<string>)>) (b: string) =
    match r with
    | [] -> ("", [])
    | h :: t -> if fst(h).Contains(b) then h else findStartRule t b

let rec countBagsWithin a (b: (string * List<string>)) c =
    let mutable count = c
    match snd (b) with
    | [] -> count
    | [ "no other" ] -> 1
    | _ ->
        for child in snd (b) do
            let multiply = child.[..0] |> int
            let bag = child.[2..]
            let rule = findStartRule a bag
            count <- count + (multiply * (countBagsWithin a rule 1))
        count

printfn "%A" (countBagsWithin bagRules (findStartRule bagRules "shiny gold") 0)

open System
open System.Linq
open System.Text.RegularExpressions

let readLines filePath = System.IO.File.ReadAllText(filePath)

let testLines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day6-test.txt"

let lines =
    readLines @"D:\Code\bjorndaniel\adventofcode\2020\day6.txt"


let testAnswerGroups =
    testLines.Split([| "\r\n\r\n" |], StringSplitOptions.RemoveEmptyEntries)

let answerGroups =
    lines.Split([| "\r\n\r\n" |], StringSplitOptions.RemoveEmptyEntries)

let getGroupSum (h: string): int =
    let s = h.Replace("\r\n", "")
    s.Distinct().Count()

let rec countGroupAnswers l s =
    match l with
    | [] -> s
    | h :: t -> countGroupAnswers t (s + (getGroupSum h))

let result =
    countGroupAnswers (Seq.toList answerGroups) 0

//Part2
let getAllYesCount (h: string): int =
    let groups =
        h.Split([| "\r\n" |], StringSplitOptions.RemoveEmptyEntries)

    let ppl = groups.Count()
    let x = String.Concat(groups)
    let charGroups = x.GroupBy(fun a -> a)

    let sum =
        charGroups.Where(fun a -> a.Count() = ppl).Count()

    printfn "%A" sum
    sum

let rec countGroupAnswersP2 l s =
    match l with
    | [] -> s
    | h :: t -> countGroupAnswersP2 t (s + (getAllYesCount h))


let resultPart2 = countGroupAnswersP2 (Seq.toList answerGroups) 0

printfn "%A" resultPart2

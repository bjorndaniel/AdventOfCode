open System.Collections.Generic
//let testInput = [ 0; 3; 6 ]
let testInput = [ 2; 3; 1 ]
let input = [ 6; 3; 15; 13; 1; 0 ]

let countFilteredBy (f: ('a -> bool)) (array: 'a []): int =
    Array.fold (fun acc item -> if f item then acc + 1 else acc) 0 array

let rec find l i n m =
    match l with
    | [] -> m
    | x when (Seq.length m) = 2 -> m
    | h :: t ->
        if h = n then
            if (Seq.length m) = 1 then
                (i :: m)
            else
                find t (i - 1) n (i :: m)
        else
            find t (i - 1) n m

let rec play turn startNumbers (numbers: byref<int []>) turns (occur: Dictionary<int, int list>) =
    if turn % 10000000 = 0 then
        printfn "%A %A" turn System.DateTime.Now

    match turn with
    | x when x <= startNumbers -> play (turn + 1) startNumbers &numbers turns occur
    | x when x = turns ->
        //printfn "%A" numbers.[(Seq.length numbers - 10)..]
        //printfn "%A %A" turn (Seq.length numbers)
        numbers.[turn - 2]
    | _ ->
        let previous = numbers.[turn - 2]
        let prevOccurs = occur.[previous]
        let mutable number = 0

        if prevOccurs.[0] = -1 then
            occur.[previous] <- [ turn - 1; -1 ]
            number <- 0
        else if prevOccurs.[1] = -1 then
            if prevOccurs.[0] = turn - 1 then
                number <- 0
            else
                number <- (turn - 1) - prevOccurs.[0]
                occur.[previous] <- [ turn - 1; prevOccurs.[0] ]
        else
            number <- prevOccurs.[0] - prevOccurs.[1]

        numbers.[turn - 1] <- number
        occur.[number] <- [ turn; occur.[number].[0] ]
        play (turn + 1) startNumbers &numbers turns occur

let numberCount = 2021
let testOccurs = Dictionary<int, int list>()

for i = 0 to (Seq.length testInput) - 1 do
    testOccurs.Add(testInput.[i], [ i + 1; -1 ])

for i = 0 to numberCount do
    if testOccurs.ContainsKey(i) = false then
        testOccurs.Add(i, [ -1; -1 ])


let mutable testResultNumbers = Array.zeroCreate numberCount

for i = 0 to (Seq.length testInput) - 1 do
    testResultNumbers.[i] <- testInput.[i]

printfn "%A" System.DateTime.Now
printfn "TestResult part 1: %A" (play 1 3 &testResultNumbers numberCount testOccurs)
printfn "%A" System.DateTime.Now

let occurs = Dictionary<int, int list>()

for i = 0 to (Seq.length input) - 1 do
    occurs.Add(input.[i], [ i + 1; -1 ])

for i = 0 to numberCount do
    if occurs.ContainsKey(i) = false then
        occurs.Add(i, [ -1; -1 ])


let mutable resultNumbers = Array.zeroCreate numberCount

for i = 0 to (Seq.length input) - 1 do
    resultNumbers.[i] <- input.[i]

printfn "%A" System.DateTime.Now
printfn "Result part 1: %A" (play 1 6 &resultNumbers numberCount occurs)
printfn "%A" System.DateTime.Now


//Part 2

let numberCountP2 = 30000001
let testOccursP2 = Dictionary<int, int list>()

for i = 0 to (Seq.length testInput) - 1 do
    testOccursP2.Add(testInput.[i], [ i + 1; -1 ])

for i = 0 to numberCountP2 do
    if not (testOccursP2.ContainsKey(i)) then
        testOccursP2.Add(i, [ -1; -1 ])


let mutable testResultNumbersP2 = Array.zeroCreate numberCountP2

for i = 0 to (Seq.length testInput) - 1 do
    testResultNumbersP2.[i] <- testInput.[i]

printfn "%A" System.DateTime.Now
printfn "TestResult part 2: %A" (play 1 3 &testResultNumbersP2 numberCountP2 testOccursP2)
printfn "%A" System.DateTime.Now

let occursP2 = Dictionary<int, int list>()

for i = 0 to (Seq.length input) - 1 do
    occursP2.Add(input.[i], [ i + 1; -1 ])

for i = 0 to numberCountP2 do
    if occursP2.ContainsKey(i) = false then
        occursP2.Add(i, [ -1; -1 ])


let mutable resultNumbersP2 = Array.zeroCreate numberCountP2

for i = 0 to (Seq.length input) - 1 do
    resultNumbersP2.[i] <- input.[i]

printfn "%A" System.DateTime.Now
printfn "Result part 2: %A" (play 1 6 &resultNumbersP2 numberCountP2 occursP2)
printfn "%A" System.DateTime.Now

//Module 1
let program =
    [ 1
      12
      2
      3
      1
      1
      2
      3
      1
      3
      4
      3
      1
      5
      0
      3
      2
      1
      9
      19
      1
      13
      19
      23
      2
      23
      9
      27
      1
      6
      27
      31
      2
      10
      31
      35
      1
      6
      35
      39
      2
      9
      39
      43
      1
      5
      43
      47
      2
      47
      13
      51
      2
      51
      10
      55
      1
      55
      5
      59
      1
      59
      9
      63
      1
      63
      9
      67
      2
      6
      67
      71
      1
      5
      71
      75
      1
      75
      6
      79
      1
      6
      79
      83
      1
      83
      9
      87
      2
      87
      10
      91
      2
      91
      10
      95
      1
      95
      5
      99
      1
      99
      13
      103
      2
      103
      9
      107
      1
      6
      107
      111
      1
      111
      5
      115
      1
      115
      2
      119
      1
      5
      119
      0
      99
      2
      0
      14
      0 ]
let testProgram1 = [ 1; 0; 0; 0; 99 ]
let testProgram2 = [ 2; 3; 0; 3; 99 ]
let testProgram3 = [ 2; 4; 4; 5; 99; 0 ]
let testProgram4 = [ 1; 1; 1; 4; 99; 5; 6; 0; 99 ]

let replaceAt (i: int) (v: int) (l: list<int>): list<int> =
    if i = 0 then
        v :: List.tail l
    else
        let s, e = List.splitAt i l
        let newS = v :: List.tail e
        List.append s newS

let rec execute (p: list<int>) (i: int) : list<int> =
    let opCode = List.item i p
    let itemsLeft = List.skip i p
    let count = List.length itemsLeft
    if opCode = 99 || count < 4 then
        p
    else
        let f = List.skip i p |> List.truncate 4
        let operand1 = List.item (List.item 1 f) p
        let operand2 = List.item (List.item 2 f) p
        let position = List.item 3 f
        let operation = List.head f
        let nextPosition = i + 4
        if operation = 1 then
            let pModified = replaceAt position (operand1 + operand2) p
            execute pModified (i + 4)
        else
            let pModified = replaceAt position (operand1 * operand2) p
            execute pModified (nextPosition)

let x = execute program 0

//Module 2
//This function from https://stackoverflow.com/questions/49190739/a-more-functional-way-to-create-tuples-from-two-arrays
let allTuplesUntil x =
    let primary = seq { 0 .. x }
    let secondary = seq { 0 .. x }
    [for x in primary do
     for y in secondary do
     yield (x,y)]
let inputs = allTuplesUntil 99
let computeResult (a:int,b:int) = 
    let modProgram1 = replaceAt 1 a program
    let modProgram2 = replaceAt 2 b modProgram1
    let result = execute modProgram2 0
    List.item 0 result
let rec getDesiredValue desired program index = 
    let tuple = List.item index inputs
    let result = computeResult tuple
    if result = desired then [fst(tuple), snd(tuple), (100 * fst(tuple) + snd(tuple))]
    else
        getDesiredValue desired program (index+1)

let result = getDesiredValue 19690720 program 0
    

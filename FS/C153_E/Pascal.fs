let factorials = new System.Collections.Generic.Dictionary<int, bigint>();

let factorial (i: int) =
    let success, value = factorials.TryGetValue(i)
    if success then value else
        let fact = Seq.fold (*) 1I [1I .. bigint(i)] 
        factorials.Add(i, fact)
        fact

let calc n x y z = 
    factorial(n) / (factorial(x) * factorial(y) * factorial(z))

let calcCell n row col =
    calc n (n - row - col) row col

let calcRow n len =
    let y = (n + 1) - len
    List.init len (calcCell n y)

let calcLayer n =
    List.map (calcRow n) [1 .. (n + 1)]

let calcHighest n =
    let x, y, z =
        let y = n / 3
        let x = (n - y) / 2
        let z = (n - y) - x
        (x, y, z)
    calc n x y z

let printCell width value =
    let str = value.ToString()
    printf "%*s" (width * 2) str

let printRow n width row =
    let precount = (n + 1) - List.length row
    printf "%*s" (precount * width) ""
    List.iter (printCell width) row
    printfn ""

let printLayer n =
    let layer = calcLayer n
    let width = (calcHighest n).ToString().Length
    List.iter (printRow n width) layer

let rec promptInput() = 
    printf "Input layer of Pascal's Pyramid to solve for: "
    let success, n = System.Int32.TryParse(System.Console.ReadLine())
    if success then
        printfn ""; printLayer n; printfn "";
        printfn "Solution complete, press enter to exit."
        System.Console.ReadLine() |> ignore
    else
        printfn "Invalid input. Try again."
        promptInput()

[<EntryPoint>]
let main argv = 
    promptInput()
    0 // return an integer exit code

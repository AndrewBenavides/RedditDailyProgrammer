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

let printLayer n =
    let layer = calcLayer n
    let highestValue =
        let x, y, z =
            let y = n / 3
            let x = (n - y) / 2
            let z = (n - y) - x
            (x, y, z)
        calc n x y z
    let width = highestValue.ToString().Length
    List.iter (fun layer ->
        let precount = (n + 1) - List.length layer
        printf "%*s" (precount * width) ""
        List.iter (fun cell -> printf "%*s" (width * 2) (cell.ToString())) layer
        printfn ""
    ) layer

[<EntryPoint>]
let main argv = 
    printLayer 11
    System.Console.ReadLine() |> ignore
    0 // return an integer exit code

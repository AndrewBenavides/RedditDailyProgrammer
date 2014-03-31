open System.Collections.Generic
open Microsoft.FSharp.Math

let factorials = new Dictionary<int, bigint>();

let factorial (i: int) =
    let factorial n = Seq.fold (*) 1I [1I .. n]
    if factorials.ContainsKey i then factorials.[i]
    else
        let fact = factorial (bigint i)
        factorials.Add(i, fact)
        fact

let calc n x y z = 
    let fn = factorial n
    let fx = factorial x
    let fy = factorial y
    let fz = factorial z
    fn / (fx * fy * fz)

let calcCell n x y =
    calc n (n - x - y) y x

let calcRow n len =
    List.init len (fun i -> calcCell n i (n - (len - 1)))

let calcLayer n =
    List.map (fun l -> 
        calcRow n l
    ) [1 .. (n + 1)]

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
    ) (calcLayer n)

[<EntryPoint>]
let main argv = 
    printLayer 11
    System.Console.ReadLine() |> ignore
    0 // return an integer exit code

open System.Collections.Generic
open Microsoft.FSharp.Math

let factorials = new Dictionary<int, bigint>();

let factorial (i: int) =
    //Seq.fold (*) 1I [1I .. (bigint i)]
    let factorial n = Seq.fold (*) 1I [1I .. n]
    if factorials.ContainsKey i then factorials.[i]
    else
        let fact = factorial (bigint i)
        factorials.Add(i, fact)
        fact

let calcCell n x y =
    let fn = factorial n
    let fx = factorial (n - x - y)
    let fy = factorial (y)
    let fz = factorial (x)
    fn / (fx * fy * fz)
    //(factorial n) / ((factorial x) * (factorial y) * (factorial (n - x)))

let calcRow n len =
    List.init len (fun i -> calcCell n i (n - (len - 1)))

let calcLayer n =
    //let layers = [(n + 1) .. -1 .. 1]
    List.map (fun l -> 
        calcRow n l
    ) [1 .. (n + 1)]

[<EntryPoint>]
let main argv = 
    Seq.iter (fun layer -> 
        Seq.iter (printf "%A ") layer
        printfn ""
    ) (calcLayer 5)
    0 // return an integer exit code

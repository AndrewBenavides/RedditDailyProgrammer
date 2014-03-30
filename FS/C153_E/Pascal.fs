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

let printlayer n =
    let layer = calcLayer n
    
    let findLongest =
        calcCell n (n / 2 / 2) (n / 2 / 2)
//        let row = (List.length layer) - (int (ceil (float (n + 1) / 2.0))) + 1
//        let rowLen = float (List.length layer.[row])
//        //let cell = int (System.Math.Round((rowLen / 2.0),0, System.MidpointRounding.))
//        let cell = int (ceil (rowLen / 2.0))
//        layer.[row].[cell]
    
    findLongest

[<EntryPoint>]
let main argv = 
//    Seq.iter (fun layer -> 
//        Seq.iter (printf "%A ") layer
//        printfn ""
//    ) (calcLayer 5)
    //let a = printlayer 1
    //let b = printlayer 2
    let c = printlayer 3
    let d = printlayer 4
    let e = printlayer 5
    0 // return an integer exit code

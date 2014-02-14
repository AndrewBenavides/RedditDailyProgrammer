// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

type TreeBuilder(maxWidth: int, trunkChar:char, leafChar: char) =
    let rand = System.Random()
    let branchWidths = {1..2..maxWidth}

    let leafBuilder i =
        let index = i + 1
        let r = rand.Next(2, maxWidth)
        let leafChar = if index % r = 0 then 'o' else leafChar
        sprintf "%c" leafChar

    let padString width func =
        let padding = (maxWidth - width) / 2
        String.init maxWidth (fun initIndex ->
            let index = initIndex + 1
            match index with
            | index when index <= padding -> " "
            | index when index > padding + width -> " "
            | index -> func index
        )

    let branchBuilder width =
        padString width leafBuilder

    let baseBuilder =
        padString 3 (fun i -> sprintf "%c" trunkChar)

    let branches = Seq.map branchBuilder branchWidths

    member this.Print =
        for branch in branches do printfn "%s" branch
        printfn "%s" baseBuilder


[<EntryPoint>]
let main argv = 
    let tree = new TreeBuilder(15, '#', '*')
    tree.Print
    System.Console.ReadLine() |> ignore
    0 // return an integer exit code

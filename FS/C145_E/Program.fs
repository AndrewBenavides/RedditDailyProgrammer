open System

type TreeBuilder(maxWidth: int, trunkChar:char, leafChar: char) =
    let rand = System.Random()
    let branchWidths = {1..2..maxWidth}

    let leafBuilder index =
        let leafChar = 
            let r = rand.Next(2, maxWidth)
            if index % r = 0 then 'o' else leafChar

        sprintf "%c" leafChar

    let layerBuilder leafCount func =
        let padding = (maxWidth - leafCount) / 2
        String.init maxWidth (fun initIndex ->
            let index = initIndex + 1
            match index with
            | index when index <= padding -> " "
            | index when index > padding + leafCount -> " "
            | index -> func index
        )

    let branchBuilder width =
        layerBuilder width leafBuilder

    let trunkBuilder =
        layerBuilder 3 (fun i -> sprintf "%c" trunkChar)

    let branches = Seq.map branchBuilder branchWidths

    member this.Print() =
        for branch in branches do printfn "%s" branch
        printfn "%s" trunkBuilder

type InputParser(input: string) =
    let args = input.Split(' ')
    
    member this.MakeTree() =
        try
            let maxWidth = Int32.Parse(args.[0])
            let trunkChar = Char.Parse(args.[1])
            let leafChar = Char.Parse(args.[2])
            let tree = new TreeBuilder(maxWidth, trunkChar, leafChar)
            tree.Print()
        with
            | _ -> printfn "Input was not valid."

[<EntryPoint>]
let main argv = 
    InputParser("3 # *").MakeTree()
    InputParser("13 = +").MakeTree()
    System.Console.ReadLine() |> ignore
    0 // return an integer exit code

open System

let rand = Random()

let cprintf color format =
    Printf.kprintf (fun str ->
        let originalColor = Console.ForegroundColor
        try
            Console.ForegroundColor <- color
            Console.Write str
        finally
            Console.ForegroundColor <- originalColor
    ) format

type TreeBuilder(maxWidth, trunkChar, leafChar) =
    let PrintReturn() =
        printfn ""

    let PrintSpace() =
        printf " "

    let PrintLeaf() =
        if rand.Next(4) = 0 then //20% chance to print bauble?
            cprintf (enum<ConsoleColor> (rand.Next(1,15))) "%c" 'o'
        else
            cprintf ConsoleColor.Green "%c" leafChar

    let PrintTrunk() =
        cprintf ConsoleColor.Yellow "%c" trunkChar

    member this.PrintTree() =
        for level in (maxWidth / 2) .. -1 .. -1 do
            let width = 
                match level with
                | -1 -> 3
                | _ -> maxWidth - 2 * level
            let padding = (maxWidth - width) / 2
            for index in 0 .. maxWidth do
                match index with
                | _ when index = maxWidth -> PrintReturn()
                | _ when index < padding -> PrintSpace()
                | _ when index >= padding + width -> PrintSpace()
                | _ when level = -1 -> PrintTrunk()
                | _ -> PrintLeaf()

    
type TreePrinter(input: string) =
    let args = input.Split(' ')
    
    member this.Print() =
        try
            let maxWidth = Int32.Parse(args.[0])
            let trunkChar = Char.Parse(args.[1])
            let leafChar = Char.Parse(args.[2])

            if maxWidth < 3 || maxWidth > 21 then 
                raise(NotSupportedException("Tree size is out of range."))
            if maxWidth % 2 = 0 then
                raise(NotSupportedException("Tree size must be odd."))

            let tree = new TreeBuilder(maxWidth, trunkChar, leafChar)
            printfn "%d character wide tree printed with '%c' trunk and '%c' leaves:" 
                maxWidth trunkChar leafChar
            tree.PrintTree()
            printfn ""
        with
            | ex -> printfn "Input was not valid: %s" ex.Message

[<EntryPoint>]
let main argv = 
    TreePrinter("3 # *").Print()
    TreePrinter("13 = +").Print()
    TreePrinter("21 | _").Print()
    //System.Console.ReadLine() |> ignore
    0 // return an integer exit code

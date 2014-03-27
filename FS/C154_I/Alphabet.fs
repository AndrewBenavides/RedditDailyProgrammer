open System

type Sorter(alphabetString: string, words: List<string>) =
    let alphabet = //List.ofSeq alphabetString
        List.mapi (fun i c -> 
            (c, i)
        ) (List.ofSeq (alphabetString.ToUpper()))

    let CheckMissing =
        let alphabet = List.ofSeq (alphabetString.ToUpper())
        let standard = List.ofSeq "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        let rec check remaining duplicate missing =
            if remaining = [] then (List.sort duplicate), (List.sort missing)
            else
                let current = List.head remaining
                let cur, rem = List.partition (fun c -> c = current) alphabet
                match cur.Length with
                | 0 -> check remaining.Tail duplicate (current::missing)
                | 1 -> check remaining.Tail duplicate missing
                | _ -> check remaining.Tail (current::duplicate) missing
        let duplicate, missing = check standard [] []
        if not (List.isEmpty duplicate) then
            printf "Duplicate: "
            List.iter (printf "%A ") duplicate; printfn ""
        if not (List.isEmpty missing) then
            printf "Missing: "
            List.iter (printf "%A ") missing; printfn ""

    let Weigh (c: char) (alphabet: List<char * int>) =
        List.pick (fun pairs -> 
            match pairs with
            | (chr, i) when chr = c ->  Some i
            | _ -> None
        ) alphabet

    let rec Sorter a b alphabet =
        let weightOf c = Weigh c alphabet
        if a = [] then -1
        elif b = [] then 1
        else
            let wa = weightOf (List.head a)
            let wb = weightOf (List.head b)
            if wa > wb then 1
            elif wa < wb then -1
            else Sorter a.Tail b.Tail alphabet

    let Compare (a: string) (b: string) alphabet =
        let a = a.ToUpper()
        let b = b.ToUpper()
        Sorter (List.ofSeq a) (List.ofSeq b) alphabet

    let SortWords alphabet words =
        let compare a b =  Compare a b alphabet
        List.sortWith compare words

    member this.SortedWords =
        SortWords alphabet words

type Parser() =
    let WordCollector length =
        let rec collect words =
            if List.length words = length then List.rev words
            else
                let word = Console.ReadLine()
                collect (word::words)
        printfn "Input %i words:" length
        collect []
    
    let alphabet, words =
        printfn "Input number of words, a single space, and alphabet as a contiguous string:"
        let input = Console.ReadLine()
        let numberOfWords, alphabet = 
            let args = input.Split(' ')
            (Int32.Parse(args.[0]), args.[1])
        let words = WordCollector numberOfWords
        (alphabet, words)

    member this.Alphabet = alphabet
    member this.Words = words

type Processor() =
    let parser = Parser()
    let sorter = Sorter(parser.Alphabet, parser.Words)

    member this.Process() =
        printfn "\nSorted words:"
        List.iter (fun word -> printfn "%s" word) sorter.SortedWords
        printfn "\nProcess complete. Press enter to exit."
        Console.ReadLine() |> ignore

[<EntryPoint>]
let main argv = 
    Processor().Process()
    0 // return an integer exit code

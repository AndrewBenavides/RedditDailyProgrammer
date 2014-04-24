open System

type Alphabet(alphabetString: string) =
    let alphabet = //List.ofSeq alphabetString
        List.mapi (fun i c -> 
            (c, i)
        ) (List.ofSeq (alphabetString.ToUpper()))

    let duplicate, missing =
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
        duplicate, missing

    member this.DuplicateLetters = duplicate
    member this.IsValid =
        if (List.isEmpty this.DuplicateLetters && List.isEmpty this.MissingLetters) then
            true
        else
            false
    member this.Letters = alphabet
    member this.MissingLetters = missing
    
    member this.InvalidationMessage =
        let stringify (lst: List<char>) =
            List.fold (fun acc elem -> sprintf "%s, %c" acc elem) (string lst.Head) lst.Tail
        
        let build typeString lst =
            if not (List.isEmpty lst) then
                Some(sprintf "%s Letters: %s" typeString (stringify lst))
            else 
                None

        let messages = 
            [
                build "Duplicates" this.DuplicateLetters
                build "Missing" this.MissingLetters
            ] |> 
            List.filter (fun m -> m.IsSome) |>
            List.map (fun m -> m.Value )

        messages
               
type Sorter() =
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

    member this.Sort(alphabet: Alphabet, words: List<string>) =
        if alphabet.IsValid then
            SortWords alphabet.Letters words
        else
            words

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
            (Int32.Parse(args.[0]), Alphabet(args.[1]))
        let words = 
            if alphabet.IsValid then
                WordCollector numberOfWords
            else
                List.Empty
        (alphabet, words)

    member this.Alphabet = alphabet
    member this.Words = words

type Processor() =
    let parser = Parser()
    let sorter = Sorter()

    member this.Process() =
        let words = 
            if parser.Alphabet.IsValid then
                printfn "\nSorted words:"
                sorter.Sort(parser.Alphabet, parser.Words)
            else
                printfn "\nAlphabet parsing errors:"
                parser.Alphabet.InvalidationMessage
        List.iter (printfn "%s") words
        printfn "\nProcess complete. Press enter to exit."
        Console.ReadLine() |> ignore

[<EntryPoint>]
let main argv = 
    Processor().Process()
    0 // return an integer exit code

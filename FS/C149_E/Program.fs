let GetVowels c =
    match c with
    | 'a' | 'e' | 'i' | 'o' | 'u' -> c.ToString()
    | _ -> ""

let GetConsonants c =
    match c with
    | ' ' -> ""
    | _ when GetVowels c = "" -> c.ToString()
    | _ -> ""

let Disemvowel str = 
    let FindConsonants = String.collect GetConsonants
    let FindVowels = String.collect GetVowels

    (FindConsonants str, FindVowels str)

let PrintDisemvoweledCollections (input: string) =
    let input = input.ToLower()
    let OutputCollection title collection =
        printfn "%-10s: %s" title collection

    let consonants, vowels = Disemvowel input

    OutputCollection "Input" input
    OutputCollection "Consonants" consonants
    OutputCollection "Vowels" vowels
    printfn ""

[<EntryPoint>]
let main argv = 
    PrintDisemvoweledCollections "two drums and a cymbal fall off a cliff"
    PrintDisemvoweledCollections "all those who believe in psychokinesis raise my hand"
    PrintDisemvoweledCollections "did you hear about the excellent farmer who was outstanding in his field"
    //System.Console.ReadLine() |> ignore
    0 // return an integer exit code

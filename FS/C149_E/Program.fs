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
    let CollectConsonants = String.collect GetConsonants
    let CollectVowels = String.collect GetVowels

    (CollectConsonants str, CollectVowels str)

let PrintDisemvoweledCollections (input: string) =
    let input = input.ToLower()
    let PrintCollection title collection =
        printfn "%-10s: %s" title collection

    let consonants, vowels = Disemvowel input

    PrintCollection "Input" input
    PrintCollection "Consonants" consonants
    PrintCollection "Vowels" vowels
    printfn ""

[<EntryPoint>]
let main argv = 
    PrintDisemvoweledCollections "two drums and a cymbal fall off a cliff"
    PrintDisemvoweledCollections "all those who believe in psychokinesis raise my hand"
    PrintDisemvoweledCollections "did you hear about the excellent farmer who was outstanding in his field"
    //System.Console.ReadLine() |> ignore
    0 // return an integer exit code

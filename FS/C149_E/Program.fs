
let IsVowel c =
    match c with
    | 'a' | 'e' | 'i' | 'o' | 'u' -> true
    | _ -> false

let IsConsonant c =
    match c with
    | ' ' -> false
    | _ when IsVowel c -> false
    | _ -> true

let Disemvowel str = 
    let FindVowels = Seq.filter IsVowel
    let FindConsonants = Seq.filter IsConsonant

    (FindConsonants str, FindVowels str)


let OutputSequence title (charSeq: seq<char>) =
    printfn "%10s: %s" title (String.concat "" charSeq)

let PrintDisemvoweledSequences input =
    let consonants, vowels = Disemvowel input
    OutputSequence "Consonants" consonants
    OutputSequence "Vowels" vowels
    printfn ""

let Print

[<EntryPoint>]
let main argv = 
    //let test = "two drums and a cymbal fall off a cliff"
    printfn "%A" argv
    0 // return an integer exit code

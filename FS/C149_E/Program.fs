let vowels = "aeiou".ToCharArray()
let IsVowel letter = Array.exists (fun vowel -> letter = vowel) vowels
let ListToString lst = 
    let sb = new System.Text.StringBuilder()
    sb.Append((Array.ofList lst)).ToString()

let Disemvoweler (input: string) =
    let rec disemvoweler characters consonants vowels =
        match characters with
        | [] -> List.rev consonants, List.rev vowels
        | head::tail when IsVowel head -> disemvoweler tail consonants (head::vowels)
        | head::tail when head <> ' ' -> disemvoweler tail (head::consonants) vowels
        | _::tail -> disemvoweler tail consonants vowels

    let characters = List.ofArray (input.ToLower().ToCharArray())
    disemvoweler characters [] []

let Disemvowel (input: string) = 
    let consonants, vowels = Disemvoweler input
    printfn "%s" input
    printfn "%s" (ListToString consonants)
    printfn "%s" (ListToString vowels)
    printfn ""

[<EntryPoint>]
let main argv = 
    Disemvowel "two drums and a cymbal fall off a cliff"
    Disemvowel "all those who believe in psychokinesis raise my hand"
    Disemvowel "did you hear about the excellent farmer who was outstanding in his field"
    //System.Console.ReadLine() |> ignore
    0 // return an integer exit code

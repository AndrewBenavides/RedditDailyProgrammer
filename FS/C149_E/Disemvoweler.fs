let (|Vowel|Consonant|Space|) c =
    if Seq.exists ((=) c) "aeiouAEIOU" then Vowel
    elif c = ' ' then Space
    else Consonant

let Stringify charlist = Seq.fold (fun s c -> s + string c) "" charlist

let Disemvoweler (input: string) =
    let vowels, consonants = 
        Seq.fold (fun (vow, con) c ->
            match c with
            | Vowel -> c::vow, con
            | Consonant -> vow, c::con
            | Space -> vow, con
        ) ([],[]) input
        |> fun (vow, con) -> List.rev vow, List.rev con //pattern matching on pipelined values
    Stringify consonants, Stringify vowels

let Disemvowel (input: string) = 
    let consonants, vowels = Disemvoweler input
    printf "%s\n%s\n%s\n\n" input consonants vowels

[<EntryPoint>]
let main argv = 
    Disemvowel "two drums and a cymbal fall off a cliff"
    Disemvowel "all those who believe in psychokinesis raise my hand"
    Disemvowel "did you hear about the excellent farmer who was outstanding in his field"
    System.Console.ReadLine() |> ignore
    0 // return an integer exit code

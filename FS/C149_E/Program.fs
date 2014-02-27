let IsVowel letter = Seq.exists ((=) letter) "aeiou"

let Stringify charlist = Seq.fold (fun s c -> s + string c) "" charlist

let Disemvoweler (input: string) =
    let chars = List.ofSeq (input.ToLower().Replace(" ",""))
    let vowels, consonants = List.partition (fun e -> IsVowel e) chars
    Stringify consonants, Stringify vowels

let Disemvowel (input: string) = 
    let consonants, vowels = Disemvoweler input
    printf "%s\n%s\n%s\n\n" input consonants vowels

[<EntryPoint>]
let main argv = 
    Disemvowel "two drums and a cymbal fall off a cliff"
    Disemvowel "all those who believe in psychokinesis raise my hand"
    Disemvowel "did you hear about the excellent farmer who was outstanding in his field"
    //System.Console.ReadLine() |> ignore
    0 // return an integer exit code

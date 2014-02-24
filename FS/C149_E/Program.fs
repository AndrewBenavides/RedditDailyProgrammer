let vowels = "aeiou".ToCharArray()
let IsVowel letter = Array.exists (fun vowel -> letter = vowel) vowels
let IsConsonant letter = not (IsVowel letter)
let WhereLetter func letter = if func letter then letter.ToString() else ""

let Disemvowel (input: string) = 
    let fromInput = input.Replace(" ","").ToLower()
    printfn "%s" input
    printfn "%s" (String.collect (WhereLetter IsConsonant) fromInput)
    printfn "%s" (String.collect (WhereLetter IsVowel) fromInput)
    printfn ""

[<EntryPoint>]
let main argv = 
    Disemvowel "two drums and a cymbal fall off a cliff"
    Disemvowel "all those who believe in psychokinesis raise my hand"
    Disemvowel "did you hear about the excellent farmer who was outstanding in his field"
    //System.Console.ReadLine() |> ignore
    0 // return an integer exit code

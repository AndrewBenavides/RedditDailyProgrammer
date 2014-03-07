// A short experiment with long function names...
let EnabledWords = seq {
    use streamReader = new System.IO.StreamReader(".\enable1.txt")
    while not streamReader.EndOfStream do yield streamReader.ReadLine().ToLower()
}

let IsVowel c = Seq.exists ((=) c) "aeiouy"

let VowelsOnlyFrom c =  if IsVowel c then string c else ""

let VowelsAreSequentialIn word =
    let vowels = String.collect VowelsOnlyFrom word
    vowels = "aeiouy"

let WordsWithSequentialVowels =
    Seq.where VowelsAreSequentialIn EnabledWords

let HighlightVowelsIn word =
    String.map (fun c -> if IsVowel c then char (int c - 32) else c) word

let PrintWordsIn word =
    printfn "%s" (HighlightVowelsIn word)

[<EntryPoint>]
let main argv = 
    printfn "Words with one of each vowel in sequential order:"
    Seq.iter PrintWordsIn WordsWithSequentialVowels
    printfn "\nSequence complete. Press enter to exit."
    System.Console.ReadLine() |> ignore
    0 // return an integer exit code


let ReadLines (filePath: string) = seq {
    use streamReader = new System.IO.StreamReader(filePath)
    while not streamReader.EndOfStream do yield streamReader.ReadLine()
}

let Stringify charlist = Seq.fold (fun s c -> s + string c) "" charlist

let consonants = "wwllfndffthstrds"
let vowels = "eieoeaeoi"
let enableWords = ReadLines(".\enable1.txt")

let IsInEnabledWords (word: string) =
    Seq.exists (fun (w: string) -> w.Contains(word)) enableWords


//Read lists backwards
//Look for the bigest word possible, if not seq.exists for a segment then stop checking and take the current sample
//prepend a consonant and check next vowels
//if no matches then insert space
//Instead of contains, check right len of word
//If no match for current vowel, discard and append next consonant

let rc = List.rev(List.ofSeq consonants)
let rv = List.rev(List.ofSeq vowels)

let rec find (c: List<char>) (v: List<char>) currentString currentWord=
    let atEnd = if c.Tail = [] then true else false
    
    let state, word, usedVowels = 
        Seq.fold (fun (continueSeek, current, vowels) elem -> 
            if continueSeek then
                let next = elem.ToString() + current
                if IsInEnabledWords(next) then (true, next, elem.ToString() + vowels)
                else (false, current, vowels)
            else (false, current, vowels)
        ) (true, c.Head.ToString(), currentWord) v
    let next = 
        if not atEnd then
            c.Tail.Head.ToString() + word
        else
            word
    let vowels = Stringify v
    let remainingVowels = 
        if usedVowels.Length > 0 then
            vowels.Substring(usedVowels.Length)
        else
            vowels
    let newVowelList = List.ofSeq remainingVowels

    if (not atEnd) && IsInEnabledWords(next) then 
        find c.Tail newVowelList currentString word
    elif (not atEnd) then
        let str = sprintf "%s %s" word currentString
        find c.Tail newVowelList str ""
    else
        currentString



[<EntryPoint>]
let main argv = 
    printfn "%s" (find rc rv "" "")
    System.Console.ReadLine() |> ignore
    0 // return an integer exit code

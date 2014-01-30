type CaesarCipher(characterShift: int, ?numberShift: int, ?punctuationShift: int) = 
    let GetShiftedHead lst shift  = 
        let len = List.length lst
        let rec MapShiftWithinLength s = 
            match s with
            | s when s < 0 -> MapShiftWithinLength (s + len)
            | s when s > len - 1 -> MapShiftWithinLength (s - len)
            | s -> lst.[s]
        MapShiftWithinLength shift
    
    let ShiftList lst shift =
        let a, b = List.partition (fun c -> c >= (GetShiftedHead lst shift)) lst
        List.append a b

    let validCharacters = ['A'..'Z']
    let validNumbers = ['0'..'9']
    let validPunctuation = ['!'..'~'] |> List.filter(fun c-> (System.Char.IsPunctuation(c)))

    let shiftedCharacters = ShiftList validCharacters characterShift
    let shiftedNumbers = ShiftList validNumbers (defaultArg numberShift 0)
    let shiftedPunctuation = ShiftList validPunctuation (defaultArg punctuationShift 0)

    member this.EncryptMessage (message: string) =
        let upperMessage = message.ToUpper()

        let GetShiftedValue validList shiftedList c =
            match List.tryFindIndex(fun e -> e = c) validList with
            | Some index -> List.nth shiftedList index
            | None -> c

        let GetShiftedCharacter c = GetShiftedValue validCharacters shiftedCharacters c
        let GetShiftedNumber c = GetShiftedValue validNumbers shiftedNumbers c
        let GetShiftedPunctuation c = GetShiftedValue validPunctuation shiftedPunctuation c
        
        String.map GetShiftedCharacter upperMessage 
        |> String.map GetShiftedNumber 
        |> String.map GetShiftedPunctuation

[<EntryPoint>]
let main argv = 
    let cipher = new CaesarCipher(2,-2,3)
    printfn "%s" (cipher.EncryptMessage "test! 1234?")
    System.Console.ReadLine() |> ignore
    printfn "%A" argv
    0 // return an integer exit code

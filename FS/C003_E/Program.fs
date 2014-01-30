type charList = {original: List<char>; shifted: List<char>;}

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
    
    let buildCharList lst shift = 
        { original = lst; shifted = ShiftList lst shift; }

    let characters = buildCharList ['A'..'Z'] characterShift
    let numbers = buildCharList ['0'..'9'] (defaultArg numberShift 0)
    let punctuation = buildCharList (['!'..'~'] |> List.filter(fun c-> (System.Char.IsPunctuation(c)))) (defaultArg punctuationShift 0)

    let GetShiftedValue sourceList destinationList c =
        match List.tryFindIndex(fun e -> e = c) sourceList with
        | Some index -> List.nth destinationList index
        | None -> c

    member this.EncryptMessage (message: string) =
        let upperMessage = message.ToUpper()

        let Encrypt (lst: charList) c =
            GetShiftedValue lst.original lst.shifted c
        
        String.map (Encrypt characters) upperMessage 
        |> String.map (Encrypt numbers) 
        |> String.map (Encrypt punctuation)

    member this.DecryptMessage (message: string) =
        let upperMessage = message.ToUpper()

        let Decrypt (lst: charList) c =
            GetShiftedValue lst.shifted lst.original c

        String.map (Decrypt characters) upperMessage 
        |> String.map (Decrypt numbers) 
        |> String.map (Decrypt punctuation)

[<EntryPoint>]
let main argv = 
    let cipher = new CaesarCipher(2,-2,3)
    printfn "%s" (cipher.EncryptMessage "test! 1234?")
    printfn "%s" (cipher.DecryptMessage "YJGP YKNN VJG DCPPGTU CPF XKEVQTA RCTCFGU EGNGDTCVG VJG FCA C DGVVGT YQTNF YCU YQP\\")
    System.Console.ReadLine() |> ignore
    printfn "%A" argv
    0 // return an integer exit code

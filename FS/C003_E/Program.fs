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

    let GetShiftedValue sourceList destinationList c =
        match List.tryFindIndex(fun e -> e = c) sourceList with
        | Some index -> List.nth destinationList index
        | None -> c
    
    let BuildCharList lst shift = 
        { original = lst; shifted = ShiftList lst shift; }

    let upperCase = BuildCharList ['A'..'Z'] characterShift
    let lowerCase = BuildCharList ['a'..'z'] characterShift
    let numbers = BuildCharList ['0'..'9'] (defaultArg numberShift 0)
    let punctuation = BuildCharList (['!'..'~'] |> List.filter(fun c-> (System.Char.IsPunctuation(c)))) (defaultArg punctuationShift 0)

    let Cipher cipher (message: string) =
        let Map charlist str =
            String.map (cipher charlist) str
            (*
                String.map has two parameters: 
                    1) a function that takes a character and returns a character
                    2) a string, which will be broken into characters that will be fed into the provided function
                Here, the function is cipher, which is inferred as a function that takes a charList and a char and returns a char.
                Below, the Encrypt and Decrypt functions are functions that take a charList and return a char.
                Providing the Encrypt and Decrypt functions with a charList satifies the first parameter and String.map provides it
                  with the second parameter, the char, from the str value that it is deconstructing.
                Effectively, String.map runs the cipher, with the given charList, on every character in the string and then returns
                  the reconstructed string that has been ciphered.

                This might be basic someday, but it took a bit of thought to understand all the basics being brought together.
            *)

        Map upperCase message
        |> Map lowerCase
        |> Map numbers
        |> Map punctuation

    member this.EncryptMessage (message: string) =
        let Encrypt (lst: charList) c =
            GetShiftedValue lst.original lst.shifted c

        Cipher Encrypt message

    member this.DecryptMessage (message: string) =
        let Decrypt (lst: charList) c =
            GetShiftedValue lst.shifted lst.original c

        Cipher Decrypt message

[<EntryPoint>]
let main argv = 
    let cipher = new CaesarCipher(2,-2,3)
    printfn "%s" (cipher.EncryptMessage "test! TEST! 1234?")
    printfn "%s" (cipher.DecryptMessage "Yjgp yknn vjg dcppgtu cpf xkevqta rctcfgu egngdtcvg vjg fca c dgvvgt yqtnf ycu yqp\\")
    System.Console.ReadLine() |> ignore
    printfn "%A" argv
    0 // return an integer exit code

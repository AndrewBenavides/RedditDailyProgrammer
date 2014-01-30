open System.Text

type CaesarCipher(shift: int) = 
    let validCharacters = ['A'..'Z']

    let GetCircularIndex(lst: List<char>, index: int) = 
        let rec value i =
            match i with
            | i when i < 0 -> value (i + lst.Length)
            | i when i > lst.Length - 1 -> value (i - lst.Length)
            | i -> lst.[i]
        value index

    let shiftedCharacters =
        let lst = validCharacters
        let a, b = List.partition (fun c -> c >= GetCircularIndex(lst, shift)) lst
        List.append a b

    member this.EncryptMessage (message: string) =
        let ContainsCharacter (e: char) (c: char) =
            e = c
                
        let GetShiftedCharacter (c: char) =
            match List.tryFindIndex(ContainsCharacter c) validCharacters with
            | Some index -> shiftedCharacters.[index]
            | None -> c
        
        let BuildEncryptedMessage (stringBuilder: StringBuilder) (c: char) =
            stringBuilder.Append(GetShiftedCharacter(c))

        let enc = (Array.fold BuildEncryptedMessage (new StringBuilder()) (message.ToUpper().ToCharArray())).ToString()
        enc   


[<EntryPoint>]
let main argv = 
    let cipher = new CaesarCipher(2)
    printfn "%s" (cipher.EncryptMessage("test!"))
    System.Console.ReadLine() |> ignore
    printfn "%A" argv
    0 // return an integer exit code

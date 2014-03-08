let random = System.Random()

let GenerateChars lst =
    List.map (fun i -> char(i)) lst

let special = GenerateChars ([32..47] @ [58..64] @ [91..96] @ [123..126])
let numbers = GenerateChars [48..57] 
let uppercase = GenerateChars [65..90]
let lowercase = GenerateChars [97..122]

let GetRandomChar n = 
    let getChar lst = List.nth lst (random.Next(0, List.length lst - 1))
    let c = 
        match random.Next(1,10) with
        | i when i <= 1 -> getChar special
        | i when i <= 3 -> getChar numbers
        | i when i <= 5 -> getChar uppercase
        | _ -> getChar lowercase
    string c

let GenerateRandomPassword length =
    String.init length GetRandomChar

let PrintPasswords amount length =
    for i in [0..amount - 1] do
        printfn "%s" (GenerateRandomPassword length)

let ParseInput message =
    printf "%s " message
    let success, number = System.Int32.TryParse(System.Console.ReadLine())
    if success then 
        number
    else
        printfn "Input was not valid. Using 10 as default."
        10

[<EntryPoint>]
let main argv = 
    let length = ParseInput "How long should the passwords be?"
    let amount = ParseInput "How many passwords should be generated?"
    printfn ""
    PrintPasswords amount length
    ParseInput "\nPress enter to exit." |> ignore
    0 // return an integer exit code

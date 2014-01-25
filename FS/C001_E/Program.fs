open System
open System.IO


let log(message) =
    use logger = new StreamWriter("C:\\Temp\\rdp_fs_c001_e_console.log", true)
    let timestamp = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH\:mm\:ss.fffffffzzz")
    do logger.WriteLine(sprintf "[%s] %s" timestamp message)

let prompt(message) =
    printf "%s " message
    let input = Console.ReadLine()
    do log(sprintf "%s %s" message input)
    input

let name = prompt "What's your name?"
let age = prompt "How old are you?"
let username = prompt "What's your reddit user name?"

let output =
    let message = sprintf "Your name is %s, you are %s years old, and your username is %s." name age username
    do log(message)
    message

let Pause() = 
    Console.ReadLine() |> ignore

[<EntryPoint>]
let main argv = 
    printfn "%s" output
    Pause()
    0 // return an integer exit code

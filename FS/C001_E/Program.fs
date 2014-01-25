open System

let prompt message =
    printf "%s " message
    Console.ReadLine()

let name = prompt "What's your name?"
let age = prompt "How old are you?"
let username = prompt "What's your reddit user name?"

let output =
    sprintf "Your name is %s, you are %s years old, and your username is %s." name age username

let Pause() = 
    Console.ReadLine() |> ignore

[<EntryPoint>]
let main argv = 
    printfn "%s" output
    Pause()
    0 // return an integer exit code

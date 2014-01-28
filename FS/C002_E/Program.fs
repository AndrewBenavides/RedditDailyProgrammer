open System

let Pause() =
    Console.ReadLine() |> ignore

type fma = { force: float; mass: float; accel: float; }

type FmaSolver() =
    let prompt(message) =
        printf "%s: " message
        let success, value = Double.TryParse(Console.ReadLine())
        value

    let input = {mass = prompt "Mass "; accel = prompt "Accel"; force = prompt "Force"; }

    let solvingForce (x: fma) = (x.force = 0.0 && x.mass >= 0.0 && x.accel >= 0.0)
    let solvingMass (x: fma) = (x.force >= 0.0 && x.mass = 0.0 && x.accel >= 0.0)
    let solvingAccel (x: fma) = (x.force >= 0.0 && x.mass >= 0.0 && x.accel = 0.0)

    let solveForce (x: fma) = x.mass * x.accel
    let solveMass (x: fma) = x.force / x.accel
    let solveAccel (x: fma) = x.force / x.mass

    member this.answer =
        match input with
        | _ when solvingForce input -> sprintf "Force = %.2f N" (solveForce input)
        | _ when solvingMass input -> sprintf "Mass = %.2f kg" (solveMass input)
        | _ when solvingAccel input -> sprintf "Acceleration = %.2f m/s" (solveAccel input)
        | _ -> sprintf "One value must be blank (or zero), the remaining values may not be blank."
        
[<EntryPoint>]
let main argv = 
    printfn "Input two known values and one unknown/blank value for F=M*A"
    let solver = new FmaSolver()
    printfn "\r%s" solver.answer
    Pause()
    0 // return an integer exit code

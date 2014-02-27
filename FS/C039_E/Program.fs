let FizzBuzz1(max) =
    let fizzbuzz i =
        match i with
        | i when i % 15 = 0 -> printfn "FizzBuzz"
        | i when i % 5 = 0 -> printfn "Buzz"
        | i when i % 3 = 0 -> printfn "Fizz"
        | i -> printfn "%i" i
    Seq.iter fizzbuzz {1..max}

let FizzBuzz2(max) =
    let fizzbuzz i =
        let fizz = if i % 3 = 0 then "Fizz" else ""
        let buzz = if i % 5 = 0 then "Buzz" else ""
        let fizzbuzz = fizz + buzz
        let output = if fizzbuzz <> "" then fizzbuzz else i.ToString()
        printfn "%s" output
    Seq.iter fizzbuzz {1..max}
           
let FizzBuzz5(max) =
    let messages i = [i.ToString(); "Fizz"; "Buzz"; "FizzBuzz"]
    let bitmask = 0b110000010010010000011000010000
    let rec fizzbuzz i bitmask =
        if i > max then 0
        else
            let bit = bitmask &&& 0b11
            printfn "%s" ((messages i).[bit]) //bit is always 0, 1, 2, or 3
            let nextmask = (bitmask >>> 2) ||| (bit <<< 28)
            fizzbuzz (i + 1) nextmask
    fizzbuzz 1 bitmask

let FizzBuzz7(max) =
    let messages i = [i.ToString(); "Fizz"; "Buzz"; "FizzBuzz"]
    let bitmask = 0b110000010010010000011000010000
    let fizzbuzz =
        Seq.unfold (fun (i, bitmask) ->
            if i > max then None
            else
                let bit = bitmask &&& 0b11
                let message = sprintf "%s" ((messages i).[bit])
                let nextmask = (bitmask >>> 2) ||| (bit <<< 28)
                let next = i + 1
                Some(message, (next, nextmask)) 
            ) ((1, bitmask))
    for v in fizzbuzz do printfn "%s" v

[<EntryPoint>]
let main argv = 
    let fizzbuzzer message func =
        System.Console.Clear()
        printfn "%s" message

        (func(20)) |> ignore
        printfn "Press any key to proceed..."
        System.Console.ReadLine() |> ignore

    fizzbuzzer "Simple function, checks 3, 5, 15 by modulus:" FizzBuzz1
    fizzbuzzer "Simple function, checks 3 and 5 combines text for 15:" FizzBuzz2
    fizzbuzzer "Advanced (in comparison) function, uses bitmask and recursive function:" FizzBuzz5
    fizzbuzzer "Advanced (in comparison) function, generates infinite fizzbuzz seq by bitmask:" FizzBuzz7
    0 // return an integer exit code

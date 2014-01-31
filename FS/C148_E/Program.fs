type InputParser(str: string) =
    static member Parse (str:string) =
        let inputs = 
            List.ofArray (str.Split(' '))
            |> List.map (fun i -> 
                match System.Int32.TryParse(i) with
                | (true, int) -> int
                | (false, int) -> invalidArg "inputs" "Input was not an integer.")
        let ticks = inputs.Head
        let movements = inputs.Tail
        ticks, movements

type CombinationLock(str: string) =
    let ticks, movements = InputParser.Parse(str)
    
    let Clockwise dst (acc, src) =
        if src > dst then (acc + (ticks + dst - src), dst)
        elif src = dst then (acc + ticks, dst)
        elif dst > src then (acc + (dst - src), dst)
        else (acc, dst)

    let CounterClockwise dst (acc, src) =
        if src > dst then (acc + (src - dst), dst)
        elif src = dst then (acc + ticks, dst)
        elif dst > src then (acc + (ticks + src - dst), dst)
        else (acc, dst)
    
    let Spin (acc, src: int) = (acc + ticks, src)

    let SpinClockwise (acc: int * int) (elem: int * int) =
        Spin acc
        |> Clockwise (snd elem)

    let SpinCounterClockwise (acc: int * int) (elem: int * int) =
        Spin acc
        |> CounterClockwise (snd elem)

    member this.Counter =
        let ApplyMovementIndex index elem = (index, elem)
        let HasEvenIndex elem = (fst elem % 2 = 0)
        
        let indexedMovements = Seq.mapi ApplyMovementIndex ([0] @ movements)
        
        let TurnDialAccumulateClicks acc elem =
            match elem with
            | (index, _) when index = 0 -> Clockwise (snd elem) acc
            | (index, _) when index = List.length movements && HasEvenIndex elem -> CounterClockwise (snd elem) acc
            | (index, _) when index = List.length movements -> Clockwise (snd elem) acc
            | _ when HasEvenIndex elem -> SpinCounterClockwise acc elem
            | _ -> SpinClockwise acc elem

        (*  The first movement is executed as goto 0 from 0, or a full spin. 
            
            Before any intermediate movements are made, a preliminary full spin is executed.
            The movement direction is determined by whether or not the movement index is even or odd. 
            i.e. The 0th movement is even and counter-clockwise, the 1st movement is odd and clockwise.
            
            The final movement is executed as a direct spin to the final destination without a preliminary full spin.
            The movement direction is determined by whether or not the movement index is even or odd.
        *)

        let clicks = fst (Seq.fold TurnDialAccumulateClicks (0,0) indexedMovements)
        clicks
                
type CombinationLock2() =
    let f n x y z = 
        let g a b = (n + b - a - 1) % n + 1 //(n + b - a - 1) gets the remainder, like Haskell's mod function appears to do
        3 * n + x + g y x + g y z

    let a = f 5 1 2 3

type CombinationLock3() =
    let ticks = 5
    let move src dst = (ticks + dst - src - 1) % ticks + 1
    let spin = move 0 0
    let cw src dst = move src dst
    let ccw src dst = move dst src

    let a = spin + spin + cw 0 1 + spin + ccw 1 2 + cw 2 3

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    let a = new CombinationLock("5 1 2 3")
    let b = a.Counter
    //Console.ReadLine() |> ignore
    0 // return an integer exit code

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

type ICombinationLock =
    interface
    abstract member Input: string
    abstract member Clicks: int
    end

type CombinationLock1(str: string) = 
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

    interface ICombinationLock with
        member this.Input = str
        member this.Clicks =
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
                
type CombinationLock2(str: string) =
    let ticks, movements = InputParser.Parse(str)

    let f n x y z = 
        let g a b = (n + b - a - 1) % n + 1 //(n + b - a - 1) gets the remainder, like Haskell's mod function appears to do
        3 * n + x + g y x + g y z

    interface ICombinationLock with
        member this.Input = str
        member this.Clicks =
            f ticks movements.[0] movements.[1] movements.[2]

type CombinationLock3(str: string) =
    let ticks, movements = InputParser.Parse(str)

    let Move src dst = (ticks + dst - src - 1) % ticks + 1
    //  g a b = (n + b - a - 1) % n + 1
    let Spin = Move 0 0
    // 3 * n, as above in CombinationLock2, if executed 3 times will return 3 * ticks
    let Clockwise src dst = Move src dst
    // g y z, as above in CombinationLock2, a clockwise movement
    let CounterClockwise src dst = Move dst src
    // g y x, as above in CombinationLock2, a counter-clockwise movement
    // if ticks was set to 5 then we would find that 21 = spin + spin + cw 0 1 + spin + ccw 1 2 + cw 2 3
    let SpinClockwise src dst = Spin + Clockwise src dst
    let SpinCounterClockwise src dst = Spin + CounterClockwise src dst
    
    interface ICombinationLock with
        member this.Input = str
        member this.Clicks =
            let ApplyMovementIndex index elem = (index, elem)
            let HasEvenIndex elem = (fst elem % 2 = 0)
        
            let indexedMovements = List.mapi ApplyMovementIndex ([0] @ movements)
        
            let TurnDialAccumulateClicks acc elem =
                let src = if fst elem > 0 then snd (indexedMovements.[fst elem - 1]) else 0
                let dst = snd elem
                let clicks =
                    match elem with
                    | (index, _) when index = 0 -> Clockwise src dst
                    | (index, _) when index = List.length movements && HasEvenIndex elem -> CounterClockwise src dst
                    | (index, _) when index = List.length movements -> Clockwise src dst
                    | _ when HasEvenIndex elem -> SpinCounterClockwise src dst
                    | _ -> SpinClockwise src dst
                acc + clicks

            let clicks = List.fold TurnDialAccumulateClicks 0 indexedMovements
            clicks
    

[<EntryPoint>]
let main argv = 
    let PrintLockInfo (lock: ICombinationLock) lockType =
        printfn "Number of clicks for input \"%s\" using %s was %d" lock.Input lockType lock.Clicks |> ignore

    PrintLockInfo (new CombinationLock1("5 1 2 3")) "CombinationLock1"
    PrintLockInfo (new CombinationLock2("5 1 2 3")) "CombinationLock2"
    PrintLockInfo (new CombinationLock3("5 1 2 3")) "CombinationLock3"
    PrintLockInfo (new CombinationLock3("5 1 2 3 2")) "CombinationLock3"
    PrintLockInfo (new CombinationLock3("60 12 32 48 1")) "CombinationLock3"
    0 // return an integer exit code

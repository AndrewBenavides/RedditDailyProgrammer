open System

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
        let AllButLast = List.length movements - 1
        let ApplyMovementIndex index elem = (index + 1, elem)
        let HasEvenIndex elem = (fst elem % 2 = 0)

        let indexedMovements = Seq.mapi ApplyMovementIndex movements
        let lastDst = Seq.nth (List.length movements - 1) indexedMovements
        let midMovements = Seq.take AllButLast indexedMovements

        let TurnDialAccumulateClicks acc elem =
            if HasEvenIndex elem then SpinCounterClockwise acc elem
            else SpinClockwise acc elem

        let LastTurn acc elem =
            if HasEvenIndex elem then CounterClockwise (snd elem) acc
            else Clockwise (snd elem) acc

        
        (*  The fold is seeded with the state after the first move (one full turn, 0 position) is executed.
            The fold then decides to spin clockwise or counterclockwise based on the movement (odd: CW, even: CCW)
            and executes a full spin before moving to the destination.  *)
        let firstMoveState = (ticks, 0)
        let midMovesState = Seq.fold TurnDialAccumulateClicks firstMoveState midMovements 
        let lastMoveState = LastTurn midMovesState lastDst

        fst lastMoveState //snd lastMoveState is the final position that the process stopped at
                
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

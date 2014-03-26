let checkMissing alphabet =
    let complete = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
//    let rec check alphabet missing extra =
//        match alphabet with
//        | [] -> missing, extra
//        | hd :: tl when List.exists(fun c -> c = hd) tl -> check tl missing c::extra
//        | hd :: tl when not List.exists(fun c -> c = hd) complete
    let extra =
        let alphabet = "UUVWXYZNOPQRSTHIJKLMABCDEFG"
        let complete = List.ofSeq "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        let aseq = 
            Seq.groupBy (fun c-> 
                List.findIndex(fun a -> a = c) complete
            ) alphabet
        for s in aseq do 
            printf "%i " (fst s)
            for c in (snd s) do
                printf "%c " c
            printfn ""

let alphabet (str: string) =
    List.mapi (fun i c -> 
        (c, i)
    ) (List.ofSeq (str.ToUpper()))

let weight (c: char) (alphabet: List<char * int>) =
    List.pick (fun pairs -> 
        match pairs with
        | (chr, i) when chr = c ->  Some i
        | _ -> None
    ) alphabet

let rec sorter a b alphabet =
    let weightOf c = weight c alphabet
    if a = [] then -1
    elif b = [] then 1
    else
        let wa = weightOf (List.head a)
        let wb = weightOf (List.head b)
        if wa > wb then 1
        elif wa < wb then -1
        else sorter a.Tail b.Tail alphabet

let sort (a: string) (b: string) alphabet =
    let a = a.ToUpper()
    let b = b.ToUpper()
    sorter (List.ofSeq a) (List.ofSeq b) alphabet

let sortwords words alphabet =
    let isort a b =  sort a b alphabet
    List.sortWith isort words

[<EntryPoint>]
let main argv = 
    let a = alphabet "UVWXYZNOPQRSTHIJKLMABCDEFG"
    let words = [
        "ANTLER";
        "ANY";
        "COW";
        "HILL";
        "HOW";
        "HOWEVER";
        "WHATEVER";
        "ZONE";
        ]
    let sorted = sortwords words a
    
    let a2 = alphabet "ZYXWVuTSRQpONMLkJIHGFEDCBa"
    let words2 = [
        "go";
        "aLL";
        "ACM";
        "teamS";
        "Go";
    ]
    let sorted2 = sortwords words2 a2
    0 // return an integer exit code

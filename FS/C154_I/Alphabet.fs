let alphabet (str: string) =
    List.mapi (fun i c -> 
        (c, i)
    ) (List.ofSeq str)

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

let sort a b alphabet =
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
    0 // return an integer exit code

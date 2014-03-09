let scores = [8;7;6;3;] //No safeties.

let IsInvalidScore score =
    (score >= 0) && (List.exists ((=) score) [1;2;4;5;])

let FindCombinations score = 
    let rec find remaining allowed (accumulated: List<int>)=
        if List.isEmpty allowed then
            []
        elif remaining > 0 then
            let invalid, valid = List.partition (fun i -> i > remaining) allowed
            if (List.length valid = 0 && remaining > 0) then
                let highest = List.max invalid
                find (remaining + highest) allowed.Tail accumulated.Tail
            else
                let amount = List.max valid
                let next = amount :: accumulated
                find (remaining - amount) allowed next
        else
            List.rev accumulated
    let rec reduce lst acc =
        match lst with
        | _ :: tl -> reduce tl ((find score lst []) :: acc)
        | [] -> List.filter (fun (lst : List<int>) -> lst.Length > 0) (List.rev acc)

    reduce scores []

let PrintCombinations score =
    List.iteri (fun index scores -> 
        printf "%i. " (index + 1)
        List.iter (fun point -> 
            printf "%d " point
        ) scores
        printfn ""
    ) (FindCombinations score)

let Process score =
    if IsInvalidScore score then
        printfn "Score %i is invalid." score
    else
        printfn "Score %i is valid. Possible combinations:" score
        PrintCombinations score
        printfn ""

[<EntryPoint>]
let main argv = 
    printf "Input a score to validate: "
    let success, score = System.Int32.TryParse(System.Console.ReadLine())
    Process score
    printfn "Press enter to exit."
    System.Console.ReadLine() |> ignore
    0 // return an integer exit code

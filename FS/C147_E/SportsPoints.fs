let legalScores = [8;7;6;3;] //No safeties.

let IsInvalidScore score =
    (score >= 0) && (List.exists ((=) score) [1;2;4;5;])

let FindCombinations score =
    let rec findPoints remainingPoints (currentChain: List<int>) (combinations: List<List<int>>) =
        match remainingPoints with
        | _ when remainingPoints < 0 -> []
        | _ when remainingPoints = 0 -> currentChain :: combinations
        | _ ->
            List.collect (fun score -> 
                findPoints (remainingPoints - score) (score :: currentChain) (combinations)
                ) legalScores
            |> List.filter (fun lst -> not (List.isEmpty lst))
    findPoints score [] []
 
let PrintCombinations score =
    List.iteri (fun index points -> 
        printf "%i. " (index + 1)
        List.iter (fun point -> 
            printf "%d " point
        ) points
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

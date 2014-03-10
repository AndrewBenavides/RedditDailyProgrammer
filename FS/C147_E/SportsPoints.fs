let legalScores = [8;7;6;3;] //No safeties.

let IsInvalidScore score =
    (score >= 0) && (List.exists ((=) score) [1;2;4;5;])

let FindCombinations score =
    let rec findPoints remainingPoints scoreSet (combinations: List<int>) =
        match scoreSet with
        | [] -> 
            //All available legalScores have been removed the scoreSet in an attempt to
            //"make change": there is no solution.
            None
        | _ when remainingPoints > 0 ->
            let invalid, valid = List.partition (fun i -> i > remainingPoints) scoreSet
            if List.isEmpty valid && remainingPoints > 0 then
                //"Rollback" last iteration and remove highest score to prevent re-running it
                let highest = List.max invalid
                findPoints (remainingPoints + highest) scoreSet.Tail combinations.Tail
            else
                let highest = List.max valid
                findPoints (remainingPoints - highest) scoreSet (highest :: combinations)
        | _ -> Some(List.rev combinations)
        
    let rec collectionCombinations points scoreSet combinations =
        match scoreSet with
        | _ :: remainingScoreSet -> 
            let collected = 
                match (findPoints points scoreSet []) with
                | Some combo -> combo :: combinations
                | None -> combinations
            collectionCombinations points remainingScoreSet collected
        | [] -> List.rev combinations

    collectionCombinations score legalScores []

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

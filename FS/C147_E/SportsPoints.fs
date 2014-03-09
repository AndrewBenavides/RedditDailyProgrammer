let scores = [8;7;6;3;] //No safeties.

let IsInvalidScore score =
    (score >= 0) && (List.exists ((=) score) [1;2;4;5;])

let FindCombinations score = 
    let rec find remaining allowed accumulated =
        if remaining > 0 then
            let invalid, valid = List.partition (fun i -> i > remaining) allowed
            if (List.length valid = 0 && remaining > 0) then 
                []
            else
                let amount = List.max valid
                let next = amount :: accumulated
                find (remaining - amount) allowed next
        else
            List.rev accumulated
    let rec reduce lst acc =
        match lst with
        | _ :: tl -> reduce tl ((find score lst []) :: acc)
        | [] -> acc

    reduce scores []
        
    
[<EntryPoint>]
let main argv = 
    let a = FindCombinations 18
    0 // return an integer exit code

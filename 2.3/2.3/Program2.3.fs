module Logic
    let rec sortList list =
        match list with
        | [ ] -> [ ] 
        | x::xs -> sortList(List.filter(fun n -> n < x) xs) @ [x] @ sortList(List.filter (fun n -> n >= x) xs) 

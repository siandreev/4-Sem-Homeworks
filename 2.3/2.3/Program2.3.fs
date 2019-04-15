module logic
    let rec sort list1 =
        match list1 with
        | [ ] -> [ ] 
        | x::xs -> sort(List.filter(fun n -> n < x) xs) @ [x] @ sort(List.filter (fun n -> n >= x) xs) 

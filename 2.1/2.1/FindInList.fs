module Logic
    
    let getFirstPosition list template =
        let rec recSearch list template count =
            match list with
            | x::xs -> 
                if not (x = template) then recSearch xs template (count + 1) else Some(count)
            | [] -> None
        recSearch list template 1
  
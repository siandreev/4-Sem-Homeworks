module logic
    let reverse list1 = 
        let rec rev tail AccList = 
            match tail with
            | (x::xs)  -> rev xs (x::AccList)
            | [] -> AccList
        rev list1 []


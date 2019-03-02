let currentList = [1;2;3;4;5;6;7;8;9;10]

let reverse list1 = 
    let rec rev tail AccList = 
        match tail with
        | (x::xs)  -> rev xs (x::AccList)
        | [] -> AccList
    rev list1 []
printf "%A" (reverse currentList)

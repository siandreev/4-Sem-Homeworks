let CurrentList = [1;2;3;4;5;6;7;8;9;10]

let reverse list1 = 
    let rec rev Hvost AccList = 
        match Hvost with
        | (x::xs)  -> rev xs (x::AccList)
        | [] -> AccList
    rev list1 []
printf "%A" (reverse CurrentList)

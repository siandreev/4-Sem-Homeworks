let CurrentList = [1;2;3;4;5;6;7;8;9;10]

let rec reverse Hvost AccList = 
    match Hvost with
    | (x::xs)  -> reverse xs (x::AccList)
    | [] -> AccList
    
let rev list1 = reverse list1 []
printf "%A" (rev CurrentList)
